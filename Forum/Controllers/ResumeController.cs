using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Documents;
using Forum.Helpers;
using Forum.Models;
using Forum.Services;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MvcPaging;

namespace Forum.Controllers
{
    [HandleError]
    public class ResumeController : AuthenticationController
    {
        private const int DefaultPageSize = 10;
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddResume(ResumeModel model)
        {
            if (ModelState.IsValid)
            {
                var resumeDocument = MapResumeDocument(model);
                _resumeService.SaveResume(resumeDocument);

                return new JsonResult
                {
                    Data = new
                    {
                        Result = 1,
                        //resumeHtml = RenderPartialHelper.RenderPartialViewToString("UploadForm", new ResumeModel(), this),
                    }
                };
            }

            // if model state is invalid, then previously uploaded file must be deleted
            DeleteResumeFile(model.FileId);

            return new JsonResult
            {
                Data = new
                {
                    Result = 0,
                    ResumeHtml = RenderPartialHelper.RenderPartialViewToString("UploadForm", model, this),
                }
            };
        }
        

        public JsonResult AjaxUpload()
        {
            var error = "File upload failed";

            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0] as HttpPostedFileBase;
                error = ValidateResume(file, new List<string> {".txt", ".doc", ".docx", ".pdf"}, 10*1024*1024);
                
                if (String.IsNullOrEmpty(error))
                {
                    var fileId = _resumeService.SaveResumeFile(file);
                    if (!String.IsNullOrEmpty(fileId))
                    {
                        return new JsonResult
                                   {
                                       Data = new
                                                  {
                                                      Result = 1,
                                                      FileId = fileId,
                                                  }
                                   };
                    }
                    else
                    {
                        error = "File save to database failed";
                    }
                }
            }

            return new JsonResult
                       {
                           Data = new
                                      {
                                          Result = 0,
                                          ErrorMessage = error,
                                      }
                       };
        }


        [NonAction]
        private string ValidateResume(HttpPostedFileBase file, ICollection<string> allowedFileExtensions, int maxContentLength)
        {
            var errorMessage = String.Empty;

            if (!file.HasFile())
            {
                errorMessage = String.Format("File is not specified");
                return errorMessage;
            }

            if (file.ContentLength > maxContentLength)
            {
                errorMessage = String.Format("File is too large, maximum allowed is: {0} MB", (maxContentLength / 1024 / 1024));
                return errorMessage;
            }

            if (allowedFileExtensions != null  &&  !allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                errorMessage = String.Format("Please upload file of type: {0}", String.Join(", ", allowedFileExtensions));
                return errorMessage;
            }

            return errorMessage;
        }

        public void DeleteResumeFile(string fileId)
        {
            if (!String.IsNullOrEmpty(fileId))
                _resumeService.DeleteResumeFile(fileId);
        }


        public ActionResult ResumeList(int? page)
        {
            var currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var resumeList = new List<ResumeListItemModel>();
            var resumes = _resumeService.GetResumes(currentPageIndex, DefaultPageSize);

            foreach (var resumeDocument in resumes)
            {
                var resumeFileDocument = _resumeService.GetResumeFile(resumeDocument.FileId, false);
                resumeList.Add(MapResumeListItemModel(resumeDocument, resumeFileDocument));
            }

            var model = new ResumeListModel
                            {
                                Resumes = resumeList.ToPagedList(currentPageIndex, DefaultPageSize, _resumeService.GetResumesCount()),
                            };

            return View(model);
        }


        public ActionResult DownloadResume(string id)
        {
            var resumeDocument = _resumeService.GetResumeFile(id);
            if (resumeDocument != null)
                return File(resumeDocument.Content, resumeDocument.ContentType, resumeDocument.Name);
            else
            {
                throw new MongoGridFSException("Oops :(. File not found in the database.");
            }
        }


        #region Mappers
        public ResumeDocument MapResumeDocument(ResumeModel resumeModel)
        {
            return new ResumeDocument
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FileId = resumeModel.FileId,
                Email = resumeModel.Email,
                Description = resumeModel.Description,
            };
        }

        public ResumeListItemModel MapResumeListItemModel(ResumeDocument resumeDocument, ResumeFileDocument resumeFileDocument)
        {
            return new ResumeListItemModel
            {
                FileId = resumeDocument.FileId,
                FileName = resumeFileDocument.Name,
                Email = resumeDocument.Email,
                UploadDate = resumeFileDocument.UploadDate,
                Description = resumeDocument.Description,
            };
        }
        #endregion
    }
}