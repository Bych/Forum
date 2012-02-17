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
}