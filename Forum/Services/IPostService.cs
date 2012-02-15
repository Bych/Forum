using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Forum.Documents;

namespace Forum.Services
{
    public interface IPostService
    {
        void AddPost(ObjectId themeId, PostDocument post);

        IEnumerable<PostDocument> GetPosts(ObjectId themeId, int pageIndex, int pageSize);

        int GetPostsCount(ObjectId themeId);

        void DeletePost(ObjectId themeId, ObjectId postId);
    }
}
