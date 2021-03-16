using agregateurMetier;
using agregateurWPF.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace agregateurWPF.DAOs
{
    public class SQLiteDAO
    {
        public static List<Profile> loadAllProfile() {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                var output = cnn.Query<Profile>("select * from Profile", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void removeProfile(Profile profile) {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                cnn.Execute("delete from Subreddit where ProfileID="+ profile.Id);
                cnn.Execute("delete from YoutubeChannel where ProfileID="+ profile.Id);
                cnn.Execute("delete from TwitchChannel where ProfileID="+ profile.Id);
                cnn.Execute("delete from Profile where Id=" + profile.Id);
            }
        }

        public static Profile CreateProfile(string name) {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                int NewProfileId = cnn.QuerySingle<Profile>("INSERT INTO Profile (Name) VALUES ('" + name + "'); select Last_Insert_Rowid() as Id;", new DynamicParameters()).Id;
                return cnn.QuerySingle<Profile>("select * from Profile where Id=" + NewProfileId, new DynamicParameters());
            }
        }

        public static string LastConnectedProfile() {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                return cnn.QuerySingle<Profile>("select ProfileID as Id from Connected where Id = 1", new DynamicParameters()).Id.ToString();
            }
        }

            public static Profile loadProfile(int profileid) {
            Profile profile = new Profile();
            profile.Id = profileid;
            return loadProfile(profile);
        }

        public static Profile loadProfile(Profile profile) {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                profile.clean();
                profile.Name = cnn.QuerySingle<Profile>("select Name from Profile where Id=" + profile.Id, new DynamicParameters()).Name;
                profile.SubReddits = cnn.Query<RedditEntry>("select * from Subreddit where ProfileID="+ profile.Id, new DynamicParameters()).ToList();
                profile.YoutubeChannels = cnn.Query<YoutubeEntry>("select * from YoutubeChannel where ProfileID=" + profile.Id, new DynamicParameters()).ToList();
                profile.TwitchLives = cnn.Query<TwitchEntry>("select * from TwitchChannel where ProfileID=" + profile.Id, new DynamicParameters()).ToList();
                cnn.Execute("update Connected set ProfileID = " + profile.Id + " where Id = 1");
                return profile;
            }
        }

        public static void upgrade(XmlDocument a_Doc) {
            foreach (XmlNode t_profil in a_Doc.DocumentElement.SelectNodes("/Data/Profil")) {
                Profile t_profileObj = CreateProfile(t_profil.SelectSingleNode("./Name").InnerText);
                t_profileObj = upgradeXmlProfile(t_profil, t_profileObj);
                saveProfile(t_profileObj, null);
            }
        }

        private static Profile upgradeXmlProfile(XmlNode a_profil, Profile a_profileObj) {

            XmlNodeList t_YTchannelsNodes = a_profil.SelectNodes("./YoutubeSetting/Channel");
            foreach (XmlNode t_channelNode in t_YTchannelsNodes) {
                try {
                    YoutubeEntry t_YTEntry = new YoutubeEntry();
                    t_YTEntry.Name = (t_channelNode.SelectSingleNode("./Name")).InnerText;
                    t_YTEntry.Link = (t_channelNode.SelectSingleNode("./Link")).InnerText;
                    t_YTEntry.Image = t_channelNode.SelectSingleNode("./Image").InnerText;
                    t_YTEntry.ProfileID = a_profileObj.Id;

                    a_profileObj.YoutubeChannels.Add(t_YTEntry);
                } catch (Exception e) { }
            }

            XmlNodeList t_subredditsNodes = a_profil.SelectNodes("./RedditSetting/SubReddit");
            foreach (XmlNode t_SubRedditNode in t_subredditsNodes) {
                try {
                    RedditEntry t_RedditEntry = new RedditEntry();
                    t_RedditEntry.Name = (t_SubRedditNode.SelectSingleNode("./Name")).InnerText;
                    t_RedditEntry.Link = (t_SubRedditNode.SelectSingleNode("./Link")).InnerText;
                    t_RedditEntry.ProfileID = a_profileObj.Id;

                    a_profileObj.SubReddits.Add(t_RedditEntry);
                } catch (Exception e) { }
            }

            XmlNodeList t_TwitchChannelNodes = a_profil.SelectNodes("./TwitchSetting/Channel");
            foreach (XmlNode t_Node in t_TwitchChannelNodes) {
                try {
                    TwitchEntry t_TwitchEntry = new TwitchEntry();
                    t_TwitchEntry.Link = t_Node.SelectSingleNode("./Link").InnerText;
                    t_TwitchEntry.Image = t_Node.SelectSingleNode("./Image").InnerText;
                    t_TwitchEntry.ProfileID = a_profileObj.Id;

                    a_profileObj.TwitchLives.Add(t_TwitchEntry);
                } catch (Exception e) { }
            }

            return a_profileObj;
        }

        public static void saveProfile(List<YoutubeChannel> a_yts, List<TwitchLive> a_twitchs, List<SubReddit> a_reddits, string a_ProfileName, Profile a_ProfileActu) {
            Profile t_profile = new Profile();
            t_profile.Name = a_ProfileName;
            
            foreach (YoutubeChannel t_yts in a_yts) {
                try {
                    if (t_yts.ChannelLink == "https://www.youtube.com/feeds/videos.xml?user=NAME")
                        continue;
                    YoutubeEntry t_YTEntry = new YoutubeEntry();
                    t_YTEntry.Id = t_yts.id;
                    t_YTEntry.Name = t_yts.ChannelName;
                    t_YTEntry.Link = t_yts.ChannelLink;
                    t_YTEntry.Image = t_yts.ChannelImageLink;
                    t_YTEntry.ProfileID = a_ProfileActu.Id;

                    t_profile.YoutubeChannels.Add(t_YTEntry);
                } catch (Exception e) { }
            }


            foreach (SubReddit t_reddits in a_reddits) {
                try {
                    if (t_reddits.SubRedditLink == "https://www.reddit.com/r/NAME/.rss")
                        continue;
                    RedditEntry t_RedditEntry = new RedditEntry();
                    t_RedditEntry.Id = t_reddits.id;
                    t_RedditEntry.Name = t_reddits.SubRedditName;
                    t_RedditEntry.Link = t_reddits.SubRedditLink;
                    t_RedditEntry.ProfileID = a_ProfileActu.Id;

                    t_profile.SubReddits.Add(t_RedditEntry);
                } catch (Exception e) { }
            }

            foreach (TwitchLive t_twitchs in a_twitchs) {
                try {
                    if (t_twitchs.Link == null)
                        continue;
                    TwitchEntry t_TwitchEntry = new TwitchEntry();
                    t_TwitchEntry.Id = t_twitchs.id;
                    t_TwitchEntry.Link = t_twitchs.Link;
                    t_TwitchEntry.Image = t_twitchs.Thumbnail.UriSource.ToString();
                    t_TwitchEntry.ProfileID = a_ProfileActu.Id;

                    t_profile.TwitchLives.Add(t_TwitchEntry);
                } catch (Exception e) { }
            }

            saveProfile(t_profile, a_ProfileActu);
        }

        public static void saveProfile(Profile a_ProfileModifier, Profile a_ProfileInitial) {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                if (a_ProfileInitial != null) {
                    foreach (YoutubeEntry t_YTEntry in a_ProfileInitial.YoutubeChannels) {
                        bool present = false;
                        foreach (YoutubeEntry tt_YTEntry in a_ProfileModifier.YoutubeChannels) {
                            if (tt_YTEntry.Id == t_YTEntry.Id) {
                                present = true;
                                break;
                        }   }
                        if (!present) {
                            cnn.Execute("delete from YoutubeChannel where Id=" + t_YTEntry.Id);
                        }
                    }

                    foreach (RedditEntry t_RedditEntry in a_ProfileInitial.SubReddits) {
                        bool present = false;
                        foreach (RedditEntry tt_RedditEntry in a_ProfileModifier.SubReddits) {
                            if (tt_RedditEntry.Id == t_RedditEntry.Id) {
                                present = true;
                                break;
                        }   }
                        if (!present) {
                            cnn.Execute("delete from Subreddit where Id=" + t_RedditEntry.Id);
                        }
                    }

                    foreach (TwitchEntry t_TwitchEntry in a_ProfileInitial.TwitchLives) {
                        bool present = false;
                        foreach (TwitchEntry tt_TwitchEntry in a_ProfileModifier.TwitchLives) {
                            if (tt_TwitchEntry.Id == t_TwitchEntry.Id) {
                                present = true;
                                break;
                        }   }
                        if (!present) {
                            cnn.Execute("delete from TwitchChannel where Id=" + t_TwitchEntry.Id);
                        }
                    }
                }
                
                foreach (YoutubeEntry t_YTEntry in a_ProfileModifier.YoutubeChannels) {
                    if (t_YTEntry.Id != 0) {
                        cnn.Execute("update YoutubeChannel set Name = '" + t_YTEntry.Name + "', Link = '" + t_YTEntry.Link + "', Image = '" + t_YTEntry.Image + "' where Id="+ t_YTEntry.Id+ ";");
                    } else {
                        cnn.Execute("INSERT INTO YoutubeChannel (Name, Link, Image, ProfileID) VALUES ('" + t_YTEntry.Name + "', '" + t_YTEntry.Link + "', '" + t_YTEntry.Image + "', " + t_YTEntry.ProfileID + ");");
                    }
                }

                foreach (RedditEntry t_RedditEntry in a_ProfileModifier.SubReddits) {
                    if (t_RedditEntry.Id != 0) {
                        cnn.Execute("update Subreddit set Name = '" + t_RedditEntry.Name + "', Link = '" + t_RedditEntry.Link + "' where Id=" + t_RedditEntry.Id + ";");
                    } else {
                        cnn.Execute("INSERT INTO Subreddit (Name, Link, ProfileID) VALUES ('" + t_RedditEntry.Name + "', '" + t_RedditEntry.Link + "', " + t_RedditEntry.ProfileID + ");");
                    }
                }

                foreach (TwitchEntry t_TwitchEntry in a_ProfileModifier.TwitchLives) {
                    if (t_TwitchEntry.Id != 0) {
                        cnn.Execute("update TwitchChannel set Link = '" + t_TwitchEntry.Link + "', Image = '" + t_TwitchEntry.Image + "' where Id=" + t_TwitchEntry.Id + ";");
                    } else {
                        cnn.Execute("INSERT INTO TwitchChannel (Link, Image, ProfileID) VALUES ('" + t_TwitchEntry.Link + "', '" + t_TwitchEntry.Image + "', " + t_TwitchEntry.ProfileID + ");");
                    }
                }

            }
        }

        private static string LoadConnectionString(string id = "Default") {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
