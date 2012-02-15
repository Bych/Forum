using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ThemeModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }
    }
}