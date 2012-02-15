using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;

namespace Forum.Models
{
    public class ThemeListModel
    {
        public IPagedList<ThemeListItemModel> Themes { get; set; }
    }
}