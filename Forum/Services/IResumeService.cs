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

        IEnumerable<ResumeDocument> GetResumes(int pageIndex, int pageSize);

        int GetResumesCount();

        string SaveResumeFile(HttpPostedFileBase file);

        ResumeFileDocument GetResumeFile(string fileId, bool initContent = true);

        void DeleteResumeFile(string fileId);
    }
}
