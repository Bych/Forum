using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;

namespace Forum.Models
{
    public class PostListModel
    {
        public IPagedList<PostListItemModel> Posts { get; set; }
    }
}