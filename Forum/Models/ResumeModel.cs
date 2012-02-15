using System.Web;
using Forum.CustomAttributes;

namespace Forum.Models
{
    public class ResumeModel
    {
        [ValidateFile(AllowedFileExtensions = new string[] { ".txt", ".doc", ".docx", ".pdf" }, 
            MaxContentLength = 10 * 1024 * 1024, // max size = 10 MB 
            ErrorMessage = "Invalid File")]
        public HttpPostedFileBase ResumeFile { get; set; }
    }
}