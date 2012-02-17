using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace Forum.Documents
{
    public class ResumeDocument
    {
        [BsonId]
        public string ResumeId { get; set; }

        public string FileId { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }
    }
}