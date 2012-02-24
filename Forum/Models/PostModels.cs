using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using System.Web.Mvc;
using MvcPaging;

namespace Forum.Models
{
    public class PostModel
    {
        public string ThemeId { get; set; }

        [Required]
        [AllowHtml]
        public string Details { get; set; }
    }

    public class PostListItemModel
    {
        public ObjectId PostId { get; set; }

        public string Author { get; set; }

        public string Details { get; set; }

        public DateTime Date { get; set; }

        public ObjectId ThemeId { get; set; }

        public string ThemeTitle { get; set; }
    }

    public class PostListModel
    {
        public IPagedList<PostListItemModel> Posts { get; set; }
    }
}