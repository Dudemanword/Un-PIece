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
using System.Net.Http;
using System.Net.Http.Headers;
using DatabaseInteractions;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace OnePieceAbridged.Controllers.Videos
{
    public class YoutubeOperations
    {
        public List<Video> GetVideoList(TokenInfo tokenInfo, List<Video> videos)
        {
            var httpClient = new HttpClient();
            var baseUrl = "https://www.googleapis.com/youtube/v3";

            
            var channelInformation = GetChannelInformation(baseUrl, tokenInfo, httpClient);
            var playlistInformation = GetPlayListInformation(channelInformation, baseUrl, tokenInfo, httpClient);
            
            var video = playlistInformation.items.Select(x => new Video
            {
                Description = x.snippet.description,
                Name = x.snippet.title,
                ThumbnailUrl = x.snippet.thumbnails.high.url,
                VideoUrl = @"http://www.youtube.com/embed/" + x.snippet.resourceId.VideoId
            });

            videos.AddRange(video);
            return videos;
        }

        private ChannelInformations GetChannelInformation(string baseUrl, TokenInfo googleAuthorization, HttpClient httpClient)
        {
            var getChannels = baseUrl + "/channels?part=contentDetails&mine=true";
            var channelResponse = MakeYoutubeGetRequest(googleAuthorization, httpClient, getChannels);
            var channelInformation = new JavaScriptSerializer().Deserialize<ChannelInformations>(channelResponse);

            return channelInformation;
        }

        public string MakeYoutubeAuthorizationRequest(NameValueCollection queryString, HttpClient httpClient)
        {
            var authUrl = "https://accounts.google.com/o/oauth2/token";

            var content = new StringContent(queryString.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            var exchangePostResponse = httpClient.PostAsync(authUrl, content).Result;
            var resp = exchangePostResponse.Content.ReadAsStringAsync().Result;

            return resp;
        }

        private PlayListInformations GetPlayListInformation(ChannelInformations channelInformation, string baseUrl, TokenInfo googleAuthorization, HttpClient httpClient)
        {
            PlayListInformations playlistInformation = new PlayListInformations();
            foreach (var item in channelInformation.items)
            {
                var playlistItemUrl = baseUrl + "/playlistItems?part=snippet&playlistId=" + item.contentDetails.relatedPlaylists.uploads;
                var playlistResponse = MakeYoutubeGetRequest(googleAuthorization, httpClient, playlistItemUrl);
                playlistInformation = new JavaScriptSerializer().Deserialize<PlayListInformations>(playlistResponse);
            }

            return playlistInformation;
        }

        private string MakeYoutubeGetRequest(TokenInfo googleAuthorization, HttpClient httpClient, string uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", googleAuthorization.access_token);
            
            var responseMessage = httpClient.SendAsync(httpRequestMessage)
                                            .Result.Content.ReadAsStringAsync().Result;
            return responseMessage;
        }
    }
}