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
    public class PostService : IPostService
    {
        private MongoCollection<ThemeDocument> themes;

        public PostService(IMongoHelper mongoHelper)
        {
            themes = mongoHelper.GetCollection<ThemeDocument>("theme");
        }

        public void AddPost(ObjectId themeId, PostDocument post)
        {
            var lastPostInfo = String.Format("{0}, {1}", post.Date, post.Author);
            themes.Update(Query.EQ("_id", themeId), Update.PushWrapped("Posts", post).Inc("TotalPosts", 1).Set("LastPostInfo", lastPostInfo));
        }

        public int GetPostsCount(ObjectId themeId)
        {
            return themes.FindOne(Query.EQ("_id", themeId)).Posts.Count;
        }

        public IEnumerable<PostDocument> GetPosts(ObjectId themeId, int pageIndex, int pageSize)
        {
            //TODO: ask Bugi how to select Posts as mongoCursor to apply SetSkip&SetLimit methods
            IEnumerable<PostDocument> test = themes.FindOne(Query.EQ("_id", themeId)).Posts.OrderBy(p => p.Date).Skip(pageIndex * pageSize).Take(pageSize);
            int selected = test.Count();

            return themes.FindOne(Query.EQ("_id", themeId)).Posts
                .OrderBy(p => p.Date)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }

        public void DeletePost(ObjectId themeId, ObjectId postId)
        {
            themes.Update(Query.EQ("_id", themeId), Update.Pull("Posts", Query.EQ("_id", postId)).Inc("TotalPosts", -1));

            List<PostDocument> posts = (List<PostDocument>)themes.FindOne(Query.EQ("_id", themeId)).Posts;
            posts.Sort(delegate(PostDocument p1, PostDocument p2) { return p1.Date.CompareTo(p2.Date); });
            var lastPostInfo = String.Format("{0}, {1}", posts.Last().Date, posts.Last().Author);
            themes.Update(Query.EQ("_id", themeId), Update.Set("LastPostInfo", lastPostInfo));
        }
    }
}