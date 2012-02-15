using System.Web;
using System.Web.Mvc;

namespace Forum.Helpers
{
    public static class FileUploadHelper
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }
    }

    public class FileUploadJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            this.ContentType = "text/html";
            context.HttpContext.Response.Write("<textarea>");
            base.ExecuteResult(context);
            context.HttpContext.Response.Write("</textarea>");
        }
    }
    
}