using agregateurMetier;
using System;
using System.Windows;
using System.Windows.Controls;

namespace agregateurWPF
{
    public partial class SettingsPage : UserControl
    {
        private SettingsPageControlleur m_controlleur;

        public SettingsPage() {
            InitializeComponent();
            m_controlleur = new SettingsPageControlleur(this);
            m_controlleur.LoadCurrentConfig();
        }

        public void SetProfileName(string a_Name){
            ProfilName.Text = a_Name;
        }

        private void AddSettings_Click(Object a_sender, EventArgs a_e) {
            AddThisSetting(((Button)a_sender).Name, null);
        }

        public void AddThisSetting(String a_name, Object a_setting) {
            RowDefinition t_row = new RowDefinition();
            
            switch (a_name) {
                case "YoutubeSettingBtn":
                    YTSetting t_Ytsetting = new YTSetting(m_controlleur,(YoutubeChannel)a_setting);
                    t_Ytsetting.SetValue(Grid.RowProperty, YoutubeSettingComponent.RowDefinitions.Count);
                    t_row.Height = new GridLength(150);
                    YoutubeSettingComponent.RowDefinitions.Add(t_row);
                    YoutubeSettingComponent.Children.Add(t_Ytsetting);
                    m_controlleur.AddtoBuffer(t_Ytsetting);
                    break;
                case "TwitchSettingBtn":
                    TwitchSetting t_twitchsetting = new TwitchSetting(m_controlleur, (TwitchLive)a_setting);
                    t_twitchsetting.SetValue(Grid.RowProperty, TwitchSettingComponent.RowDefinitions.Count);
                    t_row.Height = new GridLength(102);
                    TwitchSettingComponent.RowDefinitions.Add(t_row);
                    TwitchSettingComponent.Children.Add(t_twitchsetting);
                    m_controlleur.AddtoBuffer(t_twitchsetting);
                    break;
                case "RedditSettingBtn":
                    RedditSetting t_Redditsetting = new RedditSetting(m_controlleur, (SubReddit)a_setting);
                    t_Redditsetting.SetValue(Grid.RowProperty, RedditSettingComponent.RowDefinitions.Count);
                    t_row.Height = new GridLength(102);
                    RedditSettingComponent.RowDefinitions.Add(t_row);
                    RedditSettingComponent.Children.Add(t_Redditsetting);
                    m_controlleur.AddtoBuffer(t_Redditsetting);
                    break;
            }
        }

        public void ClearVue() {
            YoutubeSettingComponent.Children.Clear();
            YoutubeSettingComponent.RowDefinitions.Clear();
            TwitchSettingComponent.Children.Clear();
            TwitchSettingComponent.RowDefinitions.Clear();
            RedditSettingComponent.Children.Clear();
            RedditSettingComponent.RowDefinitions.Clear();
        }

        private void SaveButton_CLick(object a_sender, RoutedEventArgs a_e) {
            m_controlleur.SaveSettings(ProfilName.Text);
        }

        private void DeleteButton_Click(object a_sender, RoutedEventArgs a_e) {
            MainWindowControlleur.Instance.DeleteCurrentProfile();
        }
    }
}
