using System;
using System.Collections.Generic;
using System.Web;
using Forum.Documents;


namespace Forum.Services
{
    public class ResumeService : IResumeService
    {
        public int MaxContentLength = 10*1024*1024; // 10 MB
        public string[] AllowedFileExtensions = new[] { ".txt", ".doc", ".docx", ".pdf" };
        public string[] AllowedContentTypes;

        public void SaveResumeDocument(ResumeDocument resume)
        {
        }

        public string SaveResumeFile(HttpPostedFileBase file)
        {
            return "";
        }

        public void DownloadResume(string fileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Documents.ResumeDocument> GetResumes(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}