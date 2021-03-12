using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class RedditPage : UserControl
    {
        private RedditPageControlleur m_Controlleur;

        public RedditPage() {
            InitializeComponent();
            m_Controlleur = new RedditPageControlleur(this);
        }

        public void AfficherTab(TabItem a_tab) {
            TabControlComponent.Items.Add(a_tab);
        }
    }
}
