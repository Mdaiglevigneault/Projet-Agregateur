using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class CsUpdateBtn : UserControl
    {
        private CsGoUpdate m_Update;

        public CsUpdateBtn(CsGoUpdate a_Update, int a_Row)
        {
            InitializeComponent();
            m_Update = a_Update;

            //Emplacement dans la grille
            ItemGrid.SetValue(Grid.RowProperty, a_Row);

            TitleComponent.Text = m_Update.Title;
            DateComponent.Text = m_Update.UpdateDate.ToShortDateString();
            SinceComponent.Text = m_Update.PublicatedSince;
        }

        public void OpenCsUpdateLink(object a_sender, RoutedEventArgs a_e) {
            System.Diagnostics.Process.Start(m_Update.Link);
        }
    }
}
