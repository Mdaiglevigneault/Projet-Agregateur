using System.Collections.Generic;
using System.Windows;
using agregateurWPF;

namespace agregateurMetier
{
    public class YoutubeTabControlleur
    {
        private YoutubeTabItem m_Vue;
        private int m_Column = 3;

        public YoutubeTabControlleur(YoutubeTabItem a_Vue, int a_index)
        {
            m_Vue = a_Vue;
            GenerateVideosButtons(MainWindowControlleur.Instance.YoutubeChannels[a_index].ChannelVideos);
        }

        private void GenerateVideosButtons(List<YoutubeVideo> a_Vids) {
            int t_NbRow = (float)a_Vids.Count/m_Column > (a_Vids.Count/m_Column) ? (a_Vids.Count/m_Column) + 1 : (a_Vids.Count/m_Column);
            for (int i = 0; i < t_NbRow; i++)
                m_Vue.AjouterRow();

            YoutubeVideo[] t_VideosArray = a_Vids.ToArray();
            int t_currentIndex = 0;
            for (int i = 0; i < t_NbRow; i++) {
                for (int j = 0; j < m_Column; j++) {
                    if (t_currentIndex < t_VideosArray.Length) {
                        if (t_VideosArray[t_currentIndex] != null) {
                            m_Vue.AfficherVideo(new YT_MediaBtn(t_VideosArray[t_currentIndex], i, j));
                            t_currentIndex++;
                        }
                    }
                }
            }
        }
    }
}
