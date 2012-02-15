using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Forum.Documents;

namespace Forum.Services
{
    public interface IResumeService
    {
        void SaveResume(ResumeDocument resume);

        void DownloadResume(string fileId);

        IEnumerable<ResumeDocument> GetResumes(int pageIndex, int pageSize);
    }
}
