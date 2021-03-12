using System;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        private MainWindow m_MainwindowVue;
        private HomePageControlleur m_controlleur;

        public HomePage(MainWindow a_MainwindowVue) {
            InitializeComponent();
            m_MainwindowVue = a_MainwindowVue;
            m_controlleur = new HomePageControlleur(this);

        }

        public void AfficherYoutube(int a_nbChan, int a_NbVids, int a_NbTodayVids) {
            YTChannelsComponent.Text = a_nbChan.ToString();
            YTVideosComponent.Text = a_NbVids.ToString();
            YTTodayComponent.Text = a_NbTodayVids.ToString();
        }

        public void AfficherReddit(int a_Subreddits, int a_Posts) {
            SubredditsComponent.Text = a_Subreddits.ToString();
            PostsComponent.Text = a_Posts.ToString();
        }

        public void AfficherTwitch(int a_NBChan, int a_NBInLive) {
            TwitchChannelsComponent.Text = a_NBChan.ToString();
            TwitchInLiveComponent.Text = a_NBInLive.ToString();
        }

        public void AfficherFilms(int a_Nbfilms) {
            FilmsComponent.Text = a_Nbfilms.ToString();
        }

        public void AfficherCsGo(int a_NbUpdates, int a_NBNewUpdates) {
            CsUpdatesComponent.Text = a_NbUpdates.ToString();
            CsNewUpdatesComponent.Text = a_NBNewUpdates.ToString();
        }

        private void Menu_Select(Object a_sender, EventArgs a_e) {
            m_MainwindowVue.DisplayMenu(((Button)a_sender).Name);
        }
    }
}
