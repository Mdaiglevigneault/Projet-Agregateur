using System;
using System.Windows.Media.Imaging;

namespace agregateurMetier
{
    public class TwitchLive
    {
        public string ChannelName { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string ChannelLink { get; set; }
        public BitmapImage ChannelImage { get; set; }
        public string ChannelImageLink { get; set; }
        public BitmapImage Thumbnail { get; set; }
        public DateTime PublicationDate { get; set; }
        public string PublicatedSince { get; set; }
        public bool IsLive { get; set; }
    }
}
