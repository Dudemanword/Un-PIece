using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Web.Hosting;
using OnePieceAbridged.App_Start;
using Quartz;

namespace OnePieceAbridged.Controllers.Videos
{
    public class YoutubeOperations
    {
        public void GetVideoList(YouTubeService youtubeService, List<Video> videos)
        {
            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;

            var channelsListResponse = channelsListRequest.Execute();
            var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
            playlistItemsListRequest.MaxResults = 10;
            playlistItemsListRequest.PageToken = "";
            playlistItemsListRequest.PlaylistId = channelsListResponse.Items[0].ContentDetails.RelatedPlaylists.Uploads;
            var playlistItems = playlistItemsListRequest.Execute();
            var video = playlistItems.Items.Select(x => new Video
            {
                Description = x.Snippet.Description,
                Name = x.Snippet.Title,
                ThumbnailUrl = x.Snippet.Thumbnails.High.Url,
                VideoUrl = @"http://www.youtube.com/embed/" + x.Snippet.ResourceId.VideoId
            });

            videos.AddRange(video);
        }
    }
}