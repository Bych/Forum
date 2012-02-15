using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Web.Mvc;

namespace Forum.Documents
{
    public class ThemeDocument
    {
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId ThemeId { get; set; }

        [ScaffoldColumn(false)]
        public string Author { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        [Required]
        public string Title { get; set; }

        [ScaffoldColumn(false)]
        public string Url { get; set; }

        [Required]
        public string Details { get; set; }

        [ScaffoldColumn(false)]
        public int TotalPosts { get; set; }

        [ScaffoldColumn(false)]
        public string LastPostInfo { get; set; }

        [ScaffoldColumn(false)]
        public IList<PostDocument> Posts { get; set; }
    }
}