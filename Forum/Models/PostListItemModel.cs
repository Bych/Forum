using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Forum.Models
{
    public class PostListItemModel
    {
        public ObjectId PostId { get; set; }

        public string Author { get; set; }

        public string Details { get; set; }

        public DateTime Date { get; set; }

        public ObjectId ThemeId { get; set; }

        public string ThemeTitle { get; set; }
    }
}
