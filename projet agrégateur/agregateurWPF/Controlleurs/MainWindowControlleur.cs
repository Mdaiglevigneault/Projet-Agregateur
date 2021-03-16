using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml;
using agregateurMetier;
using agregateurWPF.DAOs;
using agregateurWPF.Models;

namespace agregateurWPF
{
    public class MainWindowControlleur
    {
        public Profile ProfileActu;
        private MainWindow m_vue;
        public string CurrentTab = "Home";
        public List<SubReddit> SubReddits;
        public List<YoutubeChannel> YoutubeChannels;
        public List<GaieteFilm> Representations;
        public List<TwitchLive> TwitchLives;
        public List<CsGoUpdate> CsGoUpdades;

        private static MainWindowControlleur m_Instance;
        private static string OldDataDirectory = "./Resources/ProfilesData.xml";

        public static MainWindowControlleur Instance {
            get { return m_Instance;}
        }

        public MainWindowControlleur(MainWindow a_vue) {
            if (MainWindowControlleur.Instance == null)
                m_Instance = this;
            else
                throw new Exception("Singleton Object Cannot be instantiated twice!");

            m_vue = a_vue;
            SubReddits = new List<SubReddit>();
            YoutubeChannels = new List<YoutubeChannel>();

            if (File.Exists(OldDataDirectory)) {
                try {
                    XmlDocument t_Doc = new XmlDocument();
                    t_Doc.Load(OldDataDirectory);
                    if (t_Doc.DocumentElement.SelectSingleNode("/Data/Upgrade").InnerText == "1") {
                        SQLiteDAO.upgrade(t_Doc);
                        File.Delete(OldDataDirectory);
                    }
                } catch(Exception e) { }
            }

            ReloadProfileButtons();
            LoadProfile(SQLiteDAO.LastConnectedProfile());
        }

        private void ReloadProfileButtons() {
            m_vue.ClearProfilButtons();
            foreach (Profile t_profil in SQLiteDAO.loadAllProfile()) {
                m_vue.AfficherProfilButton(t_profil.Name, t_profil.Id.ToString());
            }
            /*foreach (XmlNode t_profil in m_Doc.DocumentElement.SelectNodes("/Data/Profil")) {
                m_vue.AfficherProfilButton(t_profil.SelectSingleNode("./Name").InnerText, t_profil.SelectSingleNode("./Id").InnerText);
            }*/
        }

        public void LoadProfile(string a_ID) {
            Profile profile = SQLiteDAO.loadProfile(Int32.Parse(a_ID));
            ProfileActu = profile;
            m_vue.AfficherNomProfilActuel(profile.Name);
            //Vieux fontionnement
            /*XmlNodeList t_Ids = m_Doc.DocumentElement.SelectNodes("/Data/Profil/Id");
            foreach (XmlNode t_Id in t_Ids) {
                if (t_Id.InnerText.Equals(a_ID))
                    CurrentProfile = t_Id.ParentNode;
            }
            m_vue.AfficherNomProfilActuel(CurrentProfile.SelectSingleNode("./Name").InnerText);
            m_Doc.DocumentElement.SelectSingleNode("/Data/LastIDConnected").InnerText = a_ID;
            m_Doc.Save("./Resources/ProfilesData.xml");*/

            RefreshDAO();

            m_vue.DisplayMenu("Home");
        }

        public void DeleteCurrentProfile() {
            if (SQLiteDAO.loadAllProfile().Count > 1) {
                SQLiteDAO.removeProfile(ProfileActu);
                LoadProfile(SQLiteDAO.loadAllProfile()[0].Id.ToString());
            }

            /*XmlNodeList t_profileList = m_Doc.SelectNodes("/Data/Profil");
            if (t_profileList.Count > 1) {
                m_Doc.DocumentElement.RemoveChild(CurrentProfile);
                m_vue.RetirerProfilButton(CurrentProfile.SelectSingleNode("./Name").InnerText);
                LoadProfile(m_Doc.DocumentElement.LastChild.SelectSingleNode("./Id").InnerText);
            }*/
        }

        public void AddProfile() {
            //SQLite
            Profile profile = SQLiteDAO.CreateProfile("Enter Name");

            //Vieux fontionnement
            /*XmlNode t_ProfilNode = m_Doc.CreateElement("Profil");
            XmlNode t_IDNode = m_Doc.CreateElement("Id");
            t_IDNode.InnerText = (Int32.Parse(m_Doc.LastChild.LastChild.SelectSingleNode("./Id").InnerText) +1).ToString();
            XmlNode t_NameNode = m_Doc.CreateElement("Name");
            t_NameNode.InnerText = "Enter Name";
            t_ProfilNode.AppendChild(t_IDNode);
            t_ProfilNode.AppendChild(t_NameNode);
            t_ProfilNode.AppendChild(m_Doc.CreateElement("YoutubeSetting"));
            t_ProfilNode.AppendChild(m_Doc.CreateElement("TwitchSetting"));
            t_ProfilNode.AppendChild(m_Doc.CreateElement("RedditSetting"));
            m_Doc.DocumentElement.AppendChild(t_ProfilNode);*/
            ReloadProfileButtons();
            //LoadProfile(t_IDNode.InnerText);
            LoadProfile(profile.Id.ToString());
        }

        public void RefreshDAO() {
            List<YoutubeChannel> t_YTchannels = new List<YoutubeChannel>();
            foreach (YoutubeEntry t_YTEntry in ProfileActu.YoutubeChannels) {
                try {
                    YoutubeChannel t_YTchannel = new YoutubeChannel();
                    t_YTchannel.id = t_YTEntry.Id;
                    t_YTchannel.ChannelName = t_YTEntry.Name;
                    t_YTchannel.ChannelLink = t_YTEntry.Link;
                    t_YTchannel.ChannelImageLink = t_YTEntry.Image;
                    List<YoutubeVideo> t_vids = new YoutubeVideoDAO().GetChannelFeed(t_YTchannel.ChannelLink);
                    foreach (YoutubeVideo t_vid in t_vids) {
                            t_vid.ChannelImage = new BitmapImage(new Uri(t_YTchannel.ChannelImageLink));
                    }
                    t_YTchannel.ChannelVideos = t_vids;
                    t_YTchannels.Add(t_YTchannel);
                } catch (Exception e) { }
            }
            YoutubeChannels = t_YTchannels;

            List<SubReddit> t_SubReddits = new List<SubReddit>();
            foreach (RedditEntry t_RedditEntry in ProfileActu.SubReddits) {
                try {
                    SubReddit t_subReddit = new SubReddit();
                    t_subReddit.id = t_RedditEntry.Id;
                    t_subReddit.SubRedditName = t_RedditEntry.Name;
                    t_subReddit.SubRedditLink = t_RedditEntry.Link;
                    t_subReddit.SubRedditPosts = new RedditPostDAO().GetRedditPosts(t_subReddit.SubRedditLink);
                    t_SubReddits.Add(t_subReddit);
                } catch (Exception e) { }
            }
            SubReddits = t_SubReddits;

            //Refresh youtube
            /*XmlNodeList t_YTchannelsNodes = CurrentProfile.SelectNodes("./YoutubeSetting/Channel");
            List<YoutubeChannel> t_YTchannels = new List<YoutubeChannel>();

            foreach (XmlNode t_channelNode in t_YTchannelsNodes) {
                try {
                    YoutubeChannel t_YTchannel = new YoutubeChannel();
                    t_YTchannel.ChannelName = (t_channelNode.SelectSingleNode("./Name")).InnerText;
                    t_YTchannel.ChannelLink = (t_channelNode.SelectSingleNode("./Link")).InnerText;
                    List<YoutubeVideo> t_vids = new YoutubeVideoDAO().GetChannelFeed(t_YTchannel.ChannelLink);
                    t_YTchannel.ChannelImageLink = t_channelNode.SelectSingleNode("./Image").InnerText;
                    foreach (YoutubeVideo t_vid in t_vids) {
                        t_vid.ChannelImage = new BitmapImage(new Uri(t_YTchannel.ChannelImageLink));
                    }
                    t_YTchannel.ChannelVideos = t_vids;
                    t_YTchannels.Add(t_YTchannel);
                } catch (Exception e) { }
            }
            YoutubeChannels = t_YTchannels;*/

            /*XmlNodeList t_subredditsNodes = CurrentProfile.SelectNodes("./RedditSetting/SubReddit");
            List<SubReddit> t_SubReddits = new List<SubReddit>();

            foreach (XmlNode t_SubRedditNode in t_subredditsNodes) {
                try {
                    SubReddit t_subReddit = new SubReddit();
                    t_subReddit.SubRedditName = (t_SubRedditNode.SelectSingleNode("./Name")).InnerText;
                    t_subReddit.SubRedditLink = (t_SubRedditNode.SelectSingleNode("./Link")).InnerText;
                    t_subReddit.SubRedditPosts = new RedditPostDAO().GetRedditPosts(t_subReddit.SubRedditLink);
                    t_SubReddits.Add(t_subReddit);
                } catch (Exception e) { }
            }
            SubReddits = t_SubReddits;*/

            TwitchLives = new TwitchLiveDAO().GetLivesFromEntryList(ProfileActu.TwitchLives);

            Representations = new GaieteFilmDAO().GetRepresentations();
            CsGoUpdades = new CsGoUpdateDAO().GetUpdates();

            m_vue.DisplayMenu(CurrentTab);
        }

        public void SaveBufferConfig(List<YoutubeChannel> a_yts, List<TwitchLive> a_twitchs, List<SubReddit> a_reddits, string a_ProfileName) {
            /*XmlNode t_Youtubeset = CurrentProfile.SelectSingleNode("./YoutubeSetting");
            XmlNode t_Redditset = CurrentProfile.SelectSingleNode("./RedditSetting");
            XmlNode t_Twitchset = CurrentProfile.SelectSingleNode("./TwitchSetting");
            t_Youtubeset.RemoveAll();
            t_Redditset.RemoveAll();
            t_Twitchset.RemoveAll();

            foreach (YoutubeChannel t_yt in t_yts) {
                if (t_yt.ChannelLink == "https://www.youtube.com/feeds/videos.xml?user=NAME")
                    continue;

                XmlNode t_channel = m_Doc.CreateElement("Channel");
                XmlNode t_channelName = m_Doc.CreateElement("Name");
                XmlNode t_channelLink = m_Doc.CreateElement("Link");
                XmlNode t_channelImage = m_Doc.CreateElement("Image");
                t_channelName.InnerText = t_yt.ChannelName;
                t_channelLink.InnerText = t_yt.ChannelLink;
                t_channelImage.InnerText = t_yt.ChannelImageLink;
                t_channel.AppendChild(t_channelName);
                t_channel.AppendChild(t_channelLink);
                t_channel.AppendChild(t_channelImage);
                t_Youtubeset.AppendChild(t_channel);
            }

            foreach (TwitchLive t_twitch in t_twitchs) {
                XmlNode t_channel = m_Doc.CreateElement("Channel");
                XmlNode t_channelLink = m_Doc.CreateElement("Link");
                XmlNode t_channelImage = m_Doc.CreateElement("Image");
                t_channelLink.InnerText = t_twitch.ChannelLink;
                t_channelImage.InnerText = t_twitch.ChannelImageLink;
                t_channel.AppendChild(t_channelLink);
                t_channel.AppendChild(t_channelImage);
                t_Twitchset.AppendChild(t_channel);
            }

            foreach (SubReddit t_Subreddit in t_reddits) {
                XmlNode t_SubredditNode = m_Doc.CreateElement("SubReddit");
                XmlNode t_SubredditNodeName = m_Doc.CreateElement("Name");
                XmlNode t_SubredditNodeLink = m_Doc.CreateElement("Link");
                t_SubredditNodeName.InnerText = t_Subreddit.SubRedditName;
                t_SubredditNodeLink.InnerText = t_Subreddit.SubRedditLink;
                t_SubredditNode.AppendChild(t_SubredditNodeName);
                t_SubredditNode.AppendChild(t_SubredditNodeLink);
                t_Redditset.AppendChild(t_SubredditNode);
            }

            CurrentProfile.SelectSingleNode("./Name").InnerText = ProfileName;
            */
            SQLiteDAO.saveProfile(a_yts, a_twitchs, a_reddits, a_ProfileName, ProfileActu);

            ReloadProfileButtons();
            
            LoadProfile(ProfileActu.Id.ToString());
        }
    }
}
