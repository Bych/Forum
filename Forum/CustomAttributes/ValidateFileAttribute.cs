using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Forum.Helpers;

namespace Forum.CustomAttributes
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public int MaxContentLength = int.MaxValue;
        public string[] AllowedFileExtensions;
        public string[] AllowedContentTypes;

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (!file.HasFile())
            {
                ErrorMessage = String.Format("File is not specified");
                return false;
            }
                
            if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = String.Format("File is too large, maximum allowed is: {0} MB", (MaxContentLength / 1024 / 1024));
                return false;
            }

            if (AllowedFileExtensions != null)
            {
                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    ErrorMessage = String.Format("Please upload file of type: {0}", String.Join(", ", AllowedFileExtensions));
                    return false;
                }
            }

            if (AllowedContentTypes != null)
            {
                if (!AllowedContentTypes.Contains(file.ContentType))
                {
                    ErrorMessage = String.Format("Please upload file of content type: " + String.Join(", ", AllowedContentTypes));
                    return false;
                }
            }

            return true;
        }
    }
}