using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour TwitchPage.xaml
    /// </summary>
    public partial class TwitchPage : UserControl
    {
        TwitchPageControlleur m_controlleur;

        public TwitchPage()
        {
            InitializeComponent();
            m_controlleur = new TwitchPageControlleur(this);
        }
        public void AfficherLive(YT_Twitch_MediaBtn a_LiveBtn) {
            ItemGridComponent.Children.Add(a_LiveBtn);
        }

        public void AjouterRow()
        {
            RowDefinition t_row = new RowDefinition();
            t_row.Height = new GridLength(275);
            ItemGridComponent.RowDefinitions.Add(t_row);
        }
    }
}
