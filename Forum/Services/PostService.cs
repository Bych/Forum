using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using Forum.Documents;
using Forum.Helpers;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using Forum.App_Code;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly MongoCollection<ThemeDocument> _themes;

        public PostService(IMongoHelper mongoHelper)
        {
            _themes = mongoHelper.GetCollection<ThemeDocument>("theme");
        }

        public void AddPost(ObjectId themeId, PostDocument post)
        {
            var lastPostInfo = String.Format("{0}, {1}", post.Date, post.Author);
            _themes.Update(Query.EQ("_id", themeId), Update.PushWrapped("Posts", post).Inc("TotalPosts", 1).Set("LastPostInfo", lastPostInfo));
        }

        public int GetPostsCount(ObjectId themeId)
        {
            return _themes.FindOne(Query.EQ("_id", themeId)).Posts.Count;
        }

        public IEnumerable<PostDocument> GetPosts(ObjectId themeId, int pageIndex, int pageSize)
        {
            var theme = _themes
                .FindAs<ThemeDocument>(Query.EQ("_id", themeId))
                .SetLimit(1)
                .SetFields(Fields.Include("_id").Slice("Posts", pageIndex*pageSize, pageSize))
                .FirstOrDefault();

            return theme == null ? Enumerable.Empty<PostDocument>() : theme.Posts;
        }

        public void DeletePost(ObjectId themeId, ObjectId postId)
        {
            _themes.Update(Query.EQ("_id", themeId), Update.Pull("Posts", Query.EQ("_id", postId)).Inc("TotalPosts", -1));

            List<PostDocument> posts = (List<PostDocument>)_themes.FindOne(Query.EQ("_id", themeId)).Posts;
            posts.Sort(delegate(PostDocument p1, PostDocument p2) { return p1.Date.CompareTo(p2.Date); });
            var lastPostInfo = String.Format("{0}, {1}", posts.Last().Date, posts.Last().Author);
            _themes.Update(Query.EQ("_id", themeId), Update.Set("LastPostInfo", lastPostInfo));
        }
    }
}