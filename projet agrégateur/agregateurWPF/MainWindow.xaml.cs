using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace agregateurWPF
{
    public partial class MainWindow : Window
    {
        private MainWindowControlleur m_controlleur;

        public MainWindow() {
            InitializeComponent();
            m_controlleur = new MainWindowControlleur(this);
        }

        private void Refresh_Click(object a_sender, RoutedEventArgs a_e) {
            m_controlleur.RefreshDAO();
        }


        private void AddProfile_Click(object a_sender, RoutedEventArgs a_e) {
            m_controlleur.AddProfile();
            DisplayMenu("Settings");
        }

        private void ProfilButton_Click(object a_sender, RoutedEventArgs a_e) {
            if (!(a_sender is Button))
                return;
            Button t_btn = (Button)a_sender;
            if (m_controlleur.CurrentProfile.SelectSingleNode("./Id").InnerText != t_btn.Tag.ToString())
                m_controlleur.LoadProfile(t_btn.Tag.ToString());
        }

        public void AfficherNomProfilActuel(string a_Name) {
            CurrentProfileNameComponent.Text = a_Name;
        }

        public void ClearProfilButtons() {
            ProfilButtonComponent.Children.Clear();
        }

        public void AfficherProfilButton(string a_Name, String a_Id) {
            Button t_button = new Button();
            t_button.Content = a_Name;
            t_button.Tag = a_Id;
            t_button.Click += ProfilButton_Click;
            ProfilButtonComponent.Children.Add(t_button);
        }

        public void RetirerProfilButton(string a_name) {
            foreach (Button t_button in ProfilButtonComponent.Children) {
                if ((string)t_button.Content == a_name) {
                    ProfilButtonComponent.Children.Remove(t_button);
                    break;
                }
            }
        }

        private void ButtonOpenMenu_Click(object a_sender, RoutedEventArgs a_e) {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object a_sender, RoutedEventArgs a_e) {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void Menu_Select(object a_sender, RoutedEventArgs a_e) {
            DisplayMenu(((Button)a_sender).Name);
        }

        public void DisplayMenu(string a_Name) {
            UserControl t_usc = null;
            GridMain.Children.Clear();

            var t_converter = new System.Windows.Media.BrushConverter();
            Title.Foreground = (Brush)t_converter.ConvertFromString("#FFF");
            CurrentProfileNameComponent.Foreground = (Brush)t_converter.ConvertFromString("#FFF");
            ProfilLogoComponent.Foreground = (Brush)t_converter.ConvertFromString("#FFF");
            Title.Text = a_Name;
            MainWindowControlleur.Instance.CurrentTab = a_Name;

            switch (a_Name) {
                case "Home":
                    t_usc = new HomePage(this);
                    TitleBar.Background = (Brush)t_converter.ConvertFromString("#FF31577E");
                    break;
                case "Youtube":
                    t_usc = new YoutubePage();
                    TitleBar.Background = (Brush)t_converter.ConvertFromString("#ff0000");
                    break;
                case "Twitch":
                    t_usc = new TwitchPage();
                    TitleBar.Background = (Brush)t_converter.ConvertFromString("#6441a5");
                    break;
                case "Reddit":
                    t_usc = new RedditPage();
                    Title.Foreground = (Brush)t_converter.ConvertFromString("#FF5700");
                    CurrentProfileNameComponent.Foreground = (Brush)t_converter.ConvertFromString("#FF5700");
                    ProfilLogoComponent.Foreground = (Brush)t_converter.ConvertFromString("#FF5700");
                    TitleBar.Background = (Brush)t_converter.ConvertFromString("#f7f9ff");
                    break;
                case "ItemCine":
                    t_usc = new CinePage();
                    Title.Text = "Cinéma Gaiete";
                    TitleBar.Background = (Brush)t_converter.ConvertFromString("#991320");
                    break;
                case "ItemCSGO":
                    t_usc = new CsgoPage();
                    Title.Text = "CS:GO Updates";
                    TitleBar.Background = (Brush)t_converter.ConvertFromString("#171a21");
                    break;
                case "Settings":
                    t_usc = new SettingsPage();
                    Title.Text = "Settings";
                    TitleBar.Background = (Brush)t_converter.ConvertFromString("#FF31577E");
                    break;
                default:
                    break;
            }
            GridMain.Children.Add(t_usc);
        }
    }
}
