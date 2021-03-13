using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class YoutubeTabItem : UserControl
    {
        private YoutubeTabControlleur m_controlleur;

        public YoutubeTabItem(int a_index) {
            InitializeComponent();
            m_controlleur = new YoutubeTabControlleur(this, a_index);
        }

        public void AjouterRow() {
            RowDefinition t_row = new RowDefinition();
            t_row.Height = new GridLength(275);
            ItemGridComponent.RowDefinitions.Add(t_row);
        }

        public void AfficherVideo(YT_MediaBtn a_video) {
            ItemGridComponent.Children.Add(a_video);
        }
    }
}
