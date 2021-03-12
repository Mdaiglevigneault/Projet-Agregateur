using System.Collections.Generic;
using System.Windows.Controls;
using agregateurWPF;

namespace agregateurMetier
{
    public class RedditPageControlleur
    {
        private MainWindowControlleur m_MainWindowControlleur;
        private RedditPage m_Vue;

        public RedditPageControlleur(RedditPage a_Vue)
        {
            m_MainWindowControlleur = MainWindowControlleur.Instance;
            m_Vue = a_Vue;
            CreateAlltabs(m_MainWindowControlleur.SubReddits);
        }

        private void CreateAlltabs(List<SubReddit> a_subReddits)
        {
            int t_index = 0;
            foreach (SubReddit t_subReddit in a_subReddits)
            {
                TabItem t_tab = new TabItem();
                t_tab.Header = t_subReddit.SubRedditName;
                t_tab.Content = new RedditTabItem(t_index);
                m_Vue.AfficherTab(t_tab);
                t_index++;
            }
        }
    }
}
