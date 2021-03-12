using System;
using System.Collections.Generic;
using agregateurWPF;

namespace agregateurMetier
{
    public class SettingsPageControlleur
    {
        private MainWindowControlleur m_MainWindowControlleur;
        private SettingsPage m_Vue;
        private List<YTSetting> m_YTBuffer;
        private List<TwitchSetting> m_TwitchBuffer;
        private List<RedditSetting> m_SubRedditBuffer;

        public SettingsPageControlleur(SettingsPage a_Vue) {
            m_Vue = a_Vue;
            m_YTBuffer = new List<YTSetting>();
            m_TwitchBuffer = new List<TwitchSetting>();
            m_SubRedditBuffer = new List<RedditSetting>();
            m_MainWindowControlleur = MainWindowControlleur.Instance;
        }
        
        public void LoadCurrentConfig() {
            m_Vue.SetProfileName(m_MainWindowControlleur.CurrentProfile.SelectSingleNode("./Name").InnerText);

            foreach (YoutubeChannel t_chan in m_MainWindowControlleur.YoutubeChannels){
                m_Vue.AddThisSetting("YoutubeSettingBtn", t_chan);
            }
            foreach (TwitchLive t_twitch in m_MainWindowControlleur.TwitchLives){
                m_Vue.AddThisSetting("TwitchSettingBtn", t_twitch);
            }
            foreach (SubReddit t_SubRed in m_MainWindowControlleur.SubReddits) {
                m_Vue.AddThisSetting("RedditSettingBtn", t_SubRed);
            }
        }

        public void AddtoBuffer(Object a_obj) {
            if (a_obj is YTSetting)
                m_YTBuffer.Add((YTSetting)a_obj);

            if (a_obj is TwitchSetting)
                m_TwitchBuffer.Add((TwitchSetting)a_obj);

            if (a_obj is RedditSetting)
                m_SubRedditBuffer.Add((RedditSetting)a_obj);
        }

        public void RemoveFromBuffer(Object a_obj) {
            if (a_obj is YTSetting) 
                m_YTBuffer.Remove((YTSetting)a_obj);

            if (a_obj is TwitchSetting)
                m_TwitchBuffer.Remove((TwitchSetting)a_obj);

            if (a_obj is RedditSetting)
                m_SubRedditBuffer.Remove((RedditSetting)a_obj);

            ReloadVueWithBuffer();
        }

        private void ReloadVueWithBuffer() {
            List<YTSetting> t_YTreloadBuffer = m_YTBuffer;
            List<TwitchSetting> t_TwitchreloadBuffer = m_TwitchBuffer;
            List<RedditSetting> t_SubredditReloadBuffer = m_SubRedditBuffer;
            m_YTBuffer = new List<YTSetting>();
            m_TwitchBuffer = new List<TwitchSetting>();
            m_SubRedditBuffer = new List<RedditSetting>();

            m_Vue.ClearVue();

            foreach (YTSetting t_ytset in t_YTreloadBuffer) {
                m_Vue.AddThisSetting("YoutubeSettingBtn", t_ytset.Setting);
            }

            foreach (TwitchSetting t_twitchset in t_TwitchreloadBuffer) {
                m_Vue.AddThisSetting("TwitchSettingBtn", t_twitchset.Setting);
            }

            foreach (RedditSetting t_redditset in t_SubredditReloadBuffer) {
                m_Vue.AddThisSetting("RedditSettingBtn", t_redditset.Setting);
            }
        }

        public void SaveSettings(string profileName) {
            List<YoutubeChannel> t_yts = new List<YoutubeChannel>();
            foreach (YTSetting t_ytset in m_YTBuffer)
                t_yts.Add(t_ytset.Setting);

            List<TwitchLive> t_lives = new List<TwitchLive>();
            foreach (TwitchSetting t_twitchset in m_TwitchBuffer)
                t_lives.Add(t_twitchset.Setting);

            List<SubReddit> t_Sred = new List<SubReddit>();
            foreach (RedditSetting t_redditset in m_SubRedditBuffer)
                t_Sred.Add(t_redditset.Setting);

            m_MainWindowControlleur.SaveBufferConfig(t_yts, t_lives, t_Sred, profileName);
        }
    }
}
