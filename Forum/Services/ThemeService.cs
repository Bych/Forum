using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using Forum.Documents;
using Forum.Helpers;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Forum.Services
{
    public class ThemeService : IThemeService
    {
        private readonly MongoCollection<ThemeDocument> _themes;

        public ThemeService(IMongoHelper mongoHelper)
        {
            _themes = mongoHelper.GetCollection<ThemeDocument>("theme");
        }

        public void AddTheme(ThemeDocument theme)
        {
            if (theme != null)
            {
                var firstPost = new PostDocument();
                firstPost.PostId = ObjectId.GenerateNewId();
                firstPost.Author = theme.Author;
                firstPost.Date = theme.Date;
                firstPost.Details = theme.Details;

                theme.LastPostInfo = String.Format("{0}, {1}", firstPost.Date, firstPost.Author);
                theme.Posts = new List<PostDocument> { { firstPost } };
                theme.TotalPosts = theme.Posts.Count;
                theme.Url = theme.Title.GenerateSlug();

                _themes.Insert(theme);
            }
        }

        public void DeleteTheme(ObjectId themeId)
        {
            _themes.Remove(Query.EQ("_id", themeId));
        }

        public IEnumerable<ThemeDocument> GetThemes(int pageIndex, int pageSize)
        {
            return _themes.FindAll()
                .SetFields(Fields.Exclude("Posts"))
                .SetSortOrder(SortBy.Descending("Date"))
                .SetSkip(pageIndex * pageSize)
                .SetLimit(pageSize);
        }

        public int GetThemesCount()
        {
            return (int)_themes.FindAll().Count();
        }

        public ThemeDocument GetTheme(ObjectId themeId)
        {
            var theme = _themes.FindOne(Query.EQ("_id", themeId));
            theme.Posts.OrderByDescending(p => p.Date);

            return theme;
        }

        public ThemeDocument GetTheme(string url)
        {
            var theme = _themes.FindOne(Query.EQ("Url", url));
            theme.Posts.OrderByDescending(p => p.Date);

            return theme;
        }
    }
}