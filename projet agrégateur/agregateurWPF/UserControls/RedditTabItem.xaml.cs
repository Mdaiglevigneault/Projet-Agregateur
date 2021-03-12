using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class RedditTabItem : UserControl
    {
        RedditTabControlleur m_controlleur;

        public RedditTabItem(int a_index)
        {
            InitializeComponent();
            m_controlleur = new RedditTabControlleur(this, a_index);
        }

        public void NextPost_Click(object a_sender, RoutedEventArgs a_e)
        {
            m_controlleur.NextPost();
        }

        public void LastPost_Click(object a_sender, RoutedEventArgs a_e)
        {
            m_controlleur.LastPost();
        }

        public void OpenPost_Click(object a_sender, RoutedEventArgs a_e)
        {
            m_controlleur.OpenPost();
        }

        public void SharePost_Click(object a_sender, RoutedEventArgs a_e)
        {
            m_controlleur.SharePost();
        }

        public void AfficherPost(RedditPost a_post)
        {
            TitleComponent.Text = a_post.Titre;
            MediaComponent.Source = a_post.Img;
        }
    }
}
