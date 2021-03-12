using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class Film : UserControl
    {
        private GaieteFilm m_Film;

        public Film(GaieteFilm a_Film, int a_Row, int a_Column)
        {
            InitializeComponent();
            m_Film = a_Film;

            //Emplacement dans la grille
            ItemGrid.SetValue(Grid.RowProperty, a_Row);
            ItemGrid.SetValue(Grid.ColumnProperty, a_Column);

            TitleComponent.Text = m_Film.Title;
            DescComponent.Text = m_Film.Desc;
        }

        public void OpenFilmLink(object a_sender, RoutedEventArgs a_e) {
            System.Diagnostics.Process.Start(m_Film.Link);
        }
    }
}
