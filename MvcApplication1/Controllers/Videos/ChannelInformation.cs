using System.Collections.Generic;

namespace OnePieceAbridged.Controllers.Videos
{
    public class ChannelInformation
    {
        public string id { get; set; }
        public string kind { get; set; }
        public string etag { get; set; }
        public ContentDetails contentDetails { get; set; }
        public string googlePlusUserId { get; set; }
    }

    public class ChannelInformations
    {
        public List<ChannelInformation> items { get; set; }
    }
}