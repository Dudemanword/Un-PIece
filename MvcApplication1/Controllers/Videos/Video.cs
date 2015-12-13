using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnePieceAbridged.Controllers.Videos
{
    public class Video
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VideoUrl { get; set; }
    }

    public class Videos
    {
        public List<Video> VideoList { get; set; }
        public ObjectId _id { get; set; }
    }
}
