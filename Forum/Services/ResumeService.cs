using System;
using System.Collections.Generic;
using System.Web;


namespace Forum.Services
{
    public class ResumeService : IResumeService
    {
        public void SaveResume(Documents.ResumeDocument resume)
        {
            throw new NotImplementedException();
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