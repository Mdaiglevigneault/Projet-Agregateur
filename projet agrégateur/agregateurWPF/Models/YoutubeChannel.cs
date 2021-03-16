using System.Collections.Generic;

namespace agregateurMetier
{
    public class YoutubeChannel
    {
        public int id { get; set; }

        public string ChannelName { get; set; }

        public string ChannelLink { get; set; }

        public string ChannelImageLink { get; set; }

        public List<YoutubeVideo> ChannelVideos { get; set; }
    }
}
