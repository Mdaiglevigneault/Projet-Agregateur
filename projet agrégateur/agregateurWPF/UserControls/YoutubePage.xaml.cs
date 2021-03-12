using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class YoutubePage : UserControl
    {
        private YoutubePageControlleur m_controlleur;

        public YoutubePage() {
            InitializeComponent();
            m_controlleur = new YoutubePageControlleur(this);
        }

        public void AfficherTab(TabItem a_tab) {
            TabControlComponent.Items.Add(a_tab);
        }
    }
}
