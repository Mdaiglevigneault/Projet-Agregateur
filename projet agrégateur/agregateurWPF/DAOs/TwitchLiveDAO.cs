using System.Xml;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace agregateurMetier
{
    public class TwitchLiveDAO
    {
        private char[] m_Delimiters = { ' ', ':'};
        public List<TwitchLive> GetLivesFromNodeList(XmlNodeList a_twitchChannelsNodes)
        {
            List<TwitchLive> t_Lives = new List<TwitchLive>();

            foreach (XmlNode t_Node in a_twitchChannelsNodes) {
                try {
                    TwitchLive t_live = new TwitchLive();

                    t_live.ChannelLink = t_Node.SelectSingleNode("./Link").InnerText;

                    XmlDocument t_doc = new RssReader().GetXMLDocumentFromUrl(t_live.ChannelLink);

                    string t_channel = t_doc.DocumentElement.FirstChild.FirstChild.InnerText;
                    t_live.ChannelName = t_channel.Substring(0, t_channel.IndexOf("'s Twitch video RSS"));

                    XmlNode t_lastLive = t_doc.DocumentElement.FirstChild.ChildNodes[4];

                    t_live.Title = t_lastLive.ChildNodes[0].InnerText;
                    t_live.Link = t_lastLive.ChildNodes[1].InnerText;
                    t_live.ChannelImageLink = t_Node.SelectSingleNode("./Image").InnerText;
                    if (t_Node.SelectSingleNode("./Image").InnerText != string.Empty)
                        t_live.ChannelImage = new BitmapImage(new Uri(t_live.ChannelImageLink));
                    string t_desc = t_lastLive.ChildNodes[2].InnerText;
                    t_desc = t_desc.Substring(t_desc.IndexOf("img src=\"")+9, t_desc.Length-(t_desc.IndexOf("img src=\"") + 9));
                    t_live.Thumbnail = new BitmapImage(new Uri(t_desc.Substring(0, t_desc.IndexOf("\" /></a><br/>")), UriKind.Absolute));

                    string t_published = t_lastLive.ChildNodes[4].InnerText;
                    t_published = t_published.Substring(t_published.IndexOf(", ") + 2, t_published.Length-(t_published.IndexOf(", ") + 2));
                    t_published = t_published.Substring(0, t_published.IndexOf(" UT"));
                    string[] t_Date = t_published.Split(m_Delimiters);
                    DateTime t_ReleaseDate = new DateTime(Int32.Parse(t_Date[2]), StringMonthToInt(t_Date[1]), Int32.Parse(t_Date[0]), Int32.Parse(t_Date[3]), Int32.Parse(t_Date[4]), Int32.Parse(t_Date[5]));
                    t_live.PublicationDate = t_ReleaseDate;
                    TimeSpan t_DeltaRelease = DateTime.UtcNow - t_ReleaseDate;

                    t_live.PublicatedSince = t_DeltaRelease.Days > 365 ? String.Format("{0} Ans, {1} Mois", (t_DeltaRelease.Days / 365), (t_DeltaRelease.Days / 31) - (t_DeltaRelease.Days / 365) * 12) :
                                        t_DeltaRelease.Days > 31 ? String.Format("{0} Mois, {1} Jours", (t_DeltaRelease.Days / 31), t_DeltaRelease.Days - (t_DeltaRelease.Days / 31) * 31) :
                                        t_DeltaRelease.Days > 0 ? String.Format("{0} Jours, {1} Heures", t_DeltaRelease.Days, t_DeltaRelease.Hours) :
                                        t_DeltaRelease.Hours > 0 ? String.Format("{0} Heures, {1} Minutes", t_DeltaRelease.Hours, t_DeltaRelease.Minutes) :
                                        t_DeltaRelease.Minutes > 0 ? String.Format("{0} Minutes, {1} Secondes", t_DeltaRelease.Minutes, t_DeltaRelease.Seconds) :
                                        String.Format("{0} Secondes", t_DeltaRelease.Seconds);

                    t_live.IsLive = t_lastLive.ChildNodes[5].InnerText.Equals("live");

                    t_Lives.Add(t_live);
                } catch (Exception e) { }
            }

            return t_Lives;
        }


        private int StringMonthToInt(string a_month)
        {
            int t_int = 0;
            switch (a_month)
            {
                case "Jan" :
                    t_int = 1;
                    break;
                case "Feb":
                    t_int = 2;
                    break;
                case "Mar":
                    t_int = 3;
                    break;
                case "Apr":
                    t_int = 4;
                    break;
                case "May":
                    t_int = 5;
                    break;
                case "Jun":
                    t_int = 6;
                    break;
                case "Jul":
                    t_int = 7;
                    break;
                case "Aug":
                    t_int = 8;
                    break;
                case "Sep":
                    t_int = 9;
                    break;
                case "Oct":
                    t_int = 10;
                    break;
                case "Nov":
                    t_int = 11;
                    break;
                case "Dec":
                    t_int = 12;
                    break;
            }
            return t_int;
        }
    }
}
