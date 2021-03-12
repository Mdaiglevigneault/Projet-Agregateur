using System.Collections.Generic;
using System.Windows.Controls;
using agregateurWPF;

namespace agregateurMetier
{
    public class YoutubePageControlleur
    {
        private MainWindowControlleur m_MainWindowControlleur;
        private YoutubePage m_Vue;

        public YoutubePageControlleur(YoutubePage a_Vue)
        {
            m_MainWindowControlleur = MainWindowControlleur.Instance;
            m_Vue = a_Vue;
            CreateAlltabs(m_MainWindowControlleur.YoutubeChannels);
        }

        private void CreateAlltabs(List<YoutubeChannel> a_YtbChannels) {
            int t_index = 0;
            foreach (YoutubeChannel t_YtChannel in a_YtbChannels) {
                TabItem t_tab = new TabItem();
                t_tab.Header = t_YtChannel.ChannelName;
                t_tab.Content = new YoutubeTabItem(t_index);
                m_Vue.AfficherTab(t_tab);
                t_index++;
            }
        }

    }
}
