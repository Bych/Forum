using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MvcPaging;

namespace Forum.Models
{
    public class ThemeModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }
    }

    public class ThemeListItemModel
    {
        public ObjectId ThemeId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Author { get; set; }

        public int TotalPosts { get; set; }

        public string LastPostInfo { get; set; }
    }

    public class ThemeListModel
    {
        public IPagedList<ThemeListItemModel> Themes { get; set; }
    }
}