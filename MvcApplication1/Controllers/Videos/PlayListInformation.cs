using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;

namespace OnePieceAbridged.Controllers.Videos
{
    public class PlayListInformations
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string prevPageToken { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<PlayListInformation> items { get; set; }
    }

    public class PageInfo
    {
        public string totalResults { get; set; }
        public string resultsPerPage { get; set; }
    }

    public class PlayListInformation
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public Snippet snippet { get; set; }
        public VideoContentDetails contentDetails { get; set; }
        public Status privacyStatus { get; set; }
    }

    public class Status
    {
        public string privacyStatus { get; set; }
    }

    public class VideoContentDetails
    {
        public string videoId { get; set; }
        public string startAt { get; set; }
        public string endAt { get; set; }
        public string note { get; set; }
    }

    public class Snippet
    {
        public DateTime publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnail thumbnails { get; set; }
        public string channelTitle { get; set; }
        public string playlistId { get; set; }
        public uint position { get; set; }
        public ResourceId resourceId { get; set; }
    }

    public class Thumbnail
    {
        public ThumbnailInfo medium { get; set; }
        public ThumbnailInfo high { get; set; }
    }

    public class ThumbnailInfo
    {
        public string url;
        public uint width;
        public uint height;
    }
}