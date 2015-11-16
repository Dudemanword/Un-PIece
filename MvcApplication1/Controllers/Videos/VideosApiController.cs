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

namespace OnePieceAbridged.Controllers.Videos
{
    public class VideosApiController : ApiController
    {
        public List<Video> Get()
        {
            var videos = new List<Video>();
            var youtubeOperations = new YoutubeOperations();
            var youtubeService = youtubeOperations.GenerateYoutubeRequest().Result;
            youtubeOperations.GetVideoList(youtubeService, videos);
            return videos;
        }

        
    }
}
