using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class CsgoPage : UserControl
    {
        private CsgoPageControlleur m_Controlleur;

        public CsgoPage()
        {
            InitializeComponent();
            m_Controlleur = new CsgoPageControlleur(this);
        }

        public void AfficherUpdate(CsUpdateBtn a_updateBtn)
        {
            ItemGridComponent.Children.Add(a_updateBtn);
        }

        public void AjouterRow()
        {
            RowDefinition t_row = new RowDefinition();
            t_row.Height = new GridLength(130);
            ItemGridComponent.RowDefinitions.Add(t_row);
        }
    }
}
