using System;
using System.Windows.Media.Imaging;

namespace agregateurMetier
{
    public class YoutubeVideo
    {
        public string VideoID { get; set; } 

        public string Title { get; set; } 

        public BitmapImage Thumbnail { get; set; } 

        public BitmapImage ChannelImage { get; set; }

        public string Link { get; set; } 

        public float Views { get; set; } 

        public DateTime PublicationDate { get; set; }
    }
}
