using System;
using agregateurWPF;

namespace agregateurMetier
{
    public class HomePageControlleur {
        private HomePage m_Vue;
        private MainWindowControlleur m_MainWindowControlleur;

        public HomePageControlleur(HomePage a_Vue) {
            m_Vue = a_Vue;

            m_MainWindowControlleur = MainWindowControlleur.Instance;
            GenererInfoYoutube();
            GenererInfoTwitch();
            GenererInfoReddit();
            GenererInfoFilms();
            GenererInfoCsGo();
        }

        private void GenererInfoYoutube() {
            int t_YTchannels = m_MainWindowControlleur.YoutubeChannels.Count;
            int t_Videos = 0;
            int t_todayVid = 0;
            foreach (YoutubeChannel t_channel in m_MainWindowControlleur.YoutubeChannels)
            {
                t_Videos += t_channel.ChannelVideos.Count;
                foreach (YoutubeVideo t_vid in t_channel.ChannelVideos) {
                    if (t_vid.PublicationDate.ToShortDateString() == DateTime.UtcNow.ToShortDateString())
                        t_todayVid++;
                }
            }
            m_Vue.AfficherYoutube(t_YTchannels, t_Videos, t_todayVid);
        }

        private void GenererInfoTwitch() {
            int t_Twitchchannels = m_MainWindowControlleur.TwitchLives.Count;
            int t_TwitchInLive = 0;
            foreach (TwitchLive t_twitch in m_MainWindowControlleur.TwitchLives) {
                if (t_twitch.IsLive)
                    t_TwitchInLive++;
            }
            m_Vue.AfficherTwitch(t_Twitchchannels, t_TwitchInLive);
        }

        private void GenererInfoFilms() {
            int t_Films = m_MainWindowControlleur.Representations.Count;
            m_Vue.AfficherFilms(t_Films);
        }

        private void GenererInfoReddit() {
            int t_Subreddits = m_MainWindowControlleur.SubReddits.Count;
            int t_Posts = 0;
            foreach (SubReddit t_Sred in m_MainWindowControlleur.SubReddits)
                t_Posts += t_Sred.SubRedditPosts.Count;
            m_Vue.AfficherReddit(t_Subreddits, t_Posts);
        }

        private void GenererInfoCsGo() {
            int t_CsUpdates = m_MainWindowControlleur.CsGoUpdades.Count;
            int t_CsNewUpdates = 0;
            foreach (CsGoUpdate t_update in m_MainWindowControlleur.CsGoUpdades) {
                if (t_update.UpdateDate.ToShortDateString() == DateTime.UtcNow.ToShortDateString())
                    t_CsNewUpdates++;
            }

            m_Vue.AfficherCsGo(t_CsUpdates, t_CsNewUpdates);
        }
    }
}
