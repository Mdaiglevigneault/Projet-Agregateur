using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace agregateurMetier
{
    public class YoutubeVideoDAO
    {
        private char[] m_Delimiters = { '-', 'T', ':' };
        public List<YoutubeVideo> GetChannelFeed(string url)
        {
            XmlDocument t_doc = new RssReader().GetXMLDocumentFromUrl(url);

            //liste pour youtube
            XmlNodeList t_XmlEntries = t_doc.DocumentElement.ChildNodes;
 
            List<YoutubeVideo> t_videos = new List<YoutubeVideo>();
            
            foreach (XmlNode t_XmlEntry in t_XmlEntries) {
                //pour chaque entry dans le XML fournit
                if (t_XmlEntry.Name.Equals("entry")) {
                    YoutubeVideo t_vid = new YoutubeVideo();

                    t_vid.VideoID = t_XmlEntry.ChildNodes[1].InnerText;
                    t_vid.Thumbnail = new BitmapImage(new Uri(String.Format("https://img.youtube.com/vi/{0}/hqdefault.jpg", t_vid.VideoID), UriKind.Absolute));
                    t_vid.Title = t_XmlEntry.ChildNodes[3].InnerText;
                    t_vid.Link = t_XmlEntry.ChildNodes[4].Attributes["href"]?.Value;
                    t_vid.Views = float.Parse(t_XmlEntry.ChildNodes[8].ChildNodes[4].ChildNodes[1].Attributes["views"].Value);

                    string t_published = t_XmlEntry.ChildNodes[6].InnerText;
                    t_published = t_published.Substring(0, t_published.IndexOf('+'));
                    string[] t_Date = t_published.Split(m_Delimiters);
                    DateTime t_ReleaseDate = new DateTime(Int32.Parse(t_Date[0]), Int32.Parse(t_Date[1]), Int32.Parse(t_Date[2]), Int32.Parse(t_Date[3]), Int32.Parse(t_Date[4]), Int32.Parse(t_Date[5]));
                    t_vid.PublicationDate = t_ReleaseDate;
                    t_videos.Add(t_vid);
                }
            }
            return t_videos;
        }
    }
}
