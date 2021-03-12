using System.Collections.Generic;
using agregateurWPF;

namespace agregateurMetier
{
    public class TwitchPageControlleur
    {
        private TwitchPage m_Vue;
        private int m_Column = 3;

        public TwitchPageControlleur(TwitchPage a_Vue) {
            m_Vue = a_Vue;
            GenerateLivesButtons(MainWindowControlleur.Instance.TwitchLives);
        }

        private void GenerateLivesButtons(List<TwitchLive> a_Live) {
            int t_NbRow = (float)a_Live.Count / m_Column > (a_Live.Count / m_Column) ? (a_Live.Count/ m_Column) + 1 : (a_Live.Count / m_Column);
            for (int i = 0; i < t_NbRow; i++)
                m_Vue.AjouterRow();

            TwitchLive[] t_LiveArray = a_Live.ToArray();
            int t_currentIndex = 0;
            for (int i = 0; i < t_NbRow; i++) {
                for (int j = 0; j < m_Column; j++) {
                    if (t_currentIndex < t_LiveArray.Length) {
                        if (t_LiveArray[t_currentIndex] != null) {
                            YT_Twitch_MediaBtn t_LiveBtn = new YT_Twitch_MediaBtn(t_LiveArray[t_currentIndex], i, j);
                            m_Vue.AfficherLive(t_LiveBtn);
                            t_currentIndex++;
                        }
                    }
                }
            }
        }
    }
}
