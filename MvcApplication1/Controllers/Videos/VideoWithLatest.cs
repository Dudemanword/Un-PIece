using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnePieceAbridged.Controllers.Videos
{
    public class VideoWithLatest
    {
        public List<Video> videos { get; set; }
        public Video latestVideo { get; set; }
    }
}