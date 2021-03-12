using System.Windows;
using agregateurWPF;

namespace agregateurMetier
{
    public class RedditTabControlleur
    {
        private int m_CurrentPost;
        private int m_currentSubReddit;
        RedditTabItem m_Vue;
        private MainWindowControlleur m_MainWindowControlleur;

        public RedditTabControlleur(RedditTabItem a_Vue, int a_index)
        {
            m_MainWindowControlleur = MainWindowControlleur.Instance;
            m_currentSubReddit = a_index;
            m_CurrentPost = 0;
            m_Vue = a_Vue;
            m_Vue.AfficherPost(m_MainWindowControlleur.SubReddits[m_currentSubReddit].SubRedditPosts[m_CurrentPost]);
        }

        public void NextPost()
        {
            if (m_CurrentPost == (m_MainWindowControlleur.SubReddits[m_currentSubReddit].SubRedditPosts.Count - 1))
                return;

            m_CurrentPost++;
            m_Vue.AfficherPost(m_MainWindowControlleur.SubReddits[m_currentSubReddit].SubRedditPosts[m_CurrentPost]);
        }

        public void LastPost()
        {
            if (m_CurrentPost == 0)
                return;

            m_CurrentPost--;
            m_Vue.AfficherPost(m_MainWindowControlleur.SubReddits[m_currentSubReddit].SubRedditPosts[m_CurrentPost]);
        }

        public void OpenPost()
        {
            System.Diagnostics.Process.Start(m_MainWindowControlleur.SubReddits[m_currentSubReddit].SubRedditPosts[m_CurrentPost].PostLink);
        }

        public void SharePost()
        {
            Clipboard.SetText(m_MainWindowControlleur.SubReddits[m_currentSubReddit].SubRedditPosts[m_CurrentPost].PostLink);
        }
    }
}
