using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using System.Web.Mvc;

namespace Forum.Models
{
    public class PostModel
    {
        public string ThemeId { get; set; }

        [Required]
        [AllowHtml]
        public string Details { get; set; }
    }
}