using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseInteractions;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace OnePieceAbridged.Controllers.Videos
{
    public class BlogsController : ApiController
    {
        public List<BlogData> Get()
        {
            var databaseInteraction = new MongoDbInteraction(new MongoDbConnection { CollectionName = "blogs", DatabaseName = "StrawHatEntertainment" },
                                                             new MongoClient("mongodb://localhost"));
            var result = databaseInteraction.FindAllWithoutId<BsonDocument>().Result;
            var blogList = new List<BlogData>();

            result.ForEach(x => blogList.Add(BsonSerializer.Deserialize<BlogData>(x)));

            return blogList;
        }

        public void Post(BlogData blogData)
        {
            var databaseInteraction = new MongoDbInteraction(new MongoDbConnection { CollectionName = "blogs", DatabaseName = "StrawHatEntertainment" },
                                                 new MongoClient("mongodb://localhost"));
            var blogs = databaseInteraction.GetAscendingWithoutId(Builders<BsonDocument>.Sort.Ascending("BlogID")).Result;
            blogData.BlogId = blogs.Count > 0 ? BsonSerializer.Deserialize<BlogData>(blogs[0]).BlogId + 1 : 0;
            
            var result = databaseInteraction.Log(blogData);
        }

        public BlogData GetSpecificBlog(int blogId)
        {
            var databaseInteraction = new MongoDbInteraction(new MongoDbConnection { CollectionName = "blogs", DatabaseName = "StrawHatEntertainment" },
                                                 new MongoClient("mongodb://localhost"));
            var result = databaseInteraction.GetWithFilterWithoutId(Builders<BsonDocument>.Filter.Eq("BlogId", blogId)).Result;

            return BsonSerializer.Deserialize<BlogData>(result[0]);
        }
    }
}
