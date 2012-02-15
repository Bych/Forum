using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public FileUploadJsonResult AjaxUpload(ResumeModel model)
        {
            bool isSomethingUploaded = false;
            string resultMessage = String.Format("Failed to upload");

            if (ModelState.IsValid)
            {
                // map model to Document
                HttpPostedFileBase file = model.ResumeFile;

                //_resumeService.SaveResume(Document);

                resultMessage = String.Format("{0} uploaded successfully.", Path.GetFileName(file.FileName));

                isSomethingUploaded = true;
            }

            if (isSomethingUploaded)
            {
                return new FileUploadJsonResult
                           {
                               Data = new
                                          {
                                              screenMessage = resultMessage,
                                              uploadFormHtml = RenderPartialHelper.RenderPartialViewToString("UploadForm", new ResumeModel(), this),
                                          }
                           };
            }

            return new FileUploadJsonResult
                       {
                           Data = new
                                      {
                                          screenMessage = resultMessage,
                                          uploadFormHtml = RenderPartialHelper.RenderPartialViewToString("UploadForm", model, this),
                                      }
                       };
        }
    }
}