using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Forum.Documents
{
    public class PostDocument
    {
        [BsonId]
        public ObjectId PostId { get; set; }

        public DateTime Date { get; set; }

        public string Author { get; set; }

        [Required]
        public string Details { get; set; }
    }
}