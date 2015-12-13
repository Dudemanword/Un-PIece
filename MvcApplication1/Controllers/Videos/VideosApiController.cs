using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.IO;
using System.Threading;
using System.Web;
using DatabaseInteractions;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace OnePieceAbridged.Controllers.Videos
{
    public class VideosApiController : ApiController
    {
        public List<Video> Get()
        {
            //var videos = new List<Video>();
            var youtubeOperations = new YoutubeOperations();

            var databaseInteraction = new MongoDbInteraction(new MongoDbConnection { CollectionName = "videos", DatabaseName = "StrawHatEntertainment" },
                                                             new MongoClient("mongodb://localhost"));

            var videos = databaseInteraction.FindAll<Videos>().Result;
            //var tokenInfo = BsonSerializer.Deserialize<TokenInfo>(databaseInteraction.FindAll<BsonDocument>().Result[0]);

            //youtubeOperations.GetVideoList(tokenInfo, videos);
            //var videoList = videos.Where(x => x.VideoList.Any()).ToList();
            var videoList = new List<Video>();

            foreach (var video in videos)
            {
                videoList.AddRange(video.VideoList);
            }
            return videoList;
        }
        
    }
}
