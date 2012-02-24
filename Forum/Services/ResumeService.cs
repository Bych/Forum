using System;
using System.Collections.Generic;
using System.Web;
using Forum.Documents;
using Forum.Helpers;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;


namespace Forum.Services
{
    public class ResumeService : IResumeService
    {
        private readonly MongoDatabase _database;
        private readonly MongoCollection<ResumeDocument> _resumes;

        public int MaxContentLength = 10*1024*1024; // 10 MB
        public string[] AllowedFileExtensions = new[] { ".txt", ".doc", ".docx", ".pdf" };
        public string[] AllowedContentTypes;


        public ResumeService(IMongoHelper mongoHelper)
        {
            _database = mongoHelper.Database;
            _resumes = _database.GetCollection<ResumeDocument>("resume");
        }


        public void SaveResume(ResumeDocument resume)
        {
            var resumes = _database.GetCollection<ResumeDocument>("resume");
            resumes.Insert(resume);
        }


        public IEnumerable<ResumeDocument> GetResumes(int pageIndex, int pageSize)
        {
            return _resumes.FindAll()
                .SetSortOrder(SortBy.Descending("UploadDate"))
                .SetSkip(pageIndex * pageSize)
                .SetLimit(pageSize);
        }


        public int GetResumesCount()
        {
            return (int)_resumes.FindAll().Count();
        }


        public string SaveResumeFile(HttpPostedFileBase file)
        {
            var gridFsInfo = _database.GridFS.Upload(file.InputStream, file.FileName, new MongoGridFSCreateOptions{ContentType = file.ContentType});

            return gridFsInfo.Id.ToString();
        }


        public ResumeFileDocument GetResumeFile(string fileId, bool initContent = true)
        {
            ResumeFileDocument document = null;
            ObjectId id;

            if (ObjectId.TryParse(fileId, out id))
            {
                var file = _database.GridFS.FindOne(Query.EQ("_id", id));
                if (file != null)
                {
                    var fileBytes = new byte[0];

                    if (initContent)
                    {
                        using (var stream = file.OpenRead())
                        {
                            fileBytes = new byte[stream.Length];
                            stream.Read(fileBytes, 0, (int) stream.Length);
                        }
                    }

                    document = new ResumeFileDocument
                    {
                        Id = file.Id.ToString(),
                        Name = file.Name,
                        UploadDate = file.UploadDate,
                        Content = fileBytes,
                        ContentType = file.ContentType,
                    };
                }
            }

            return document;
        }


        public void DeleteResumeFile(string fileId)
        {
            ObjectId id;

            if (ObjectId.TryParse(fileId, out id))
                _database.GridFS.DeleteById(id);
        }


        


        
    }
}