using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Forum.Models
{
    public class ThemeListItemModel
    {
        public ObjectId ThemeId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Author { get; set; }
        
        public int TotalPosts { get; set; }

        public string LastPostInfo { get; set; }
    }
}