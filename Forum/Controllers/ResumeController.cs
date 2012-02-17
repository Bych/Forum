using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Forum.Helpers;
using Forum.Models;
using Forum.Services;

namespace Forum.Controllers
{
    public class ResumeController : AuthenticationController
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        //
        // GET: /Resume/

        public ActionResult Upload()
        {
            return View();
        }

        public JsonResult AjaxUpload()
        {
            //var isSomethingUploaded = false;
            //var resultMessage = String.Empty;
            var error = "File upload failed";

            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0] as HttpPostedFileBase;
                error = ValidateResume(file, new List<string> {".txt", ".doc", ".docx", ".pdf"}, 10*1024*1024);

                if (String.IsNullOrEmpty(error))
                {
                    // map model to Document

                    string path = AppDomain.CurrentDomain.BaseDirectory + @"Uploads\";
                    string filename = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(path, filename)); //TODO: rewrite this

                    var fileId = "1";
                    //var fileId = _resumeService.SaveResume(Document);

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
    }
}