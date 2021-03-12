using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class CinePage : UserControl
    {
        private GaieteFilmControlleur m_controlleur;

        public CinePage()
        {
            InitializeComponent();
            m_controlleur = new GaieteFilmControlleur(this);
        }

        public void AfficherFilm(Film a_film)
        {
            ItemGridComponent.Children.Add(a_film);
        }

        public void AjouterRow()
        {
            RowDefinition t_row = new RowDefinition();
            t_row.Height = new GridLength(200);
            ItemGridComponent.RowDefinitions.Add(t_row);
        }
    }
}
