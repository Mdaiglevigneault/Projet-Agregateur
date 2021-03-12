using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class YT_Twitch_MediaBtn : UserControl
    {
        private TwitchLive m_Live = null;
        private YoutubeVideo m_vid = null;

        public YT_Twitch_MediaBtn(Object a_Media, int a_Row, int a_Column)
        {
            InitializeComponent();

            //Emplacement dans la grille
            ItemGrid.SetValue(Grid.RowProperty, a_Row);
            ItemGrid.SetValue(Grid.ColumnProperty, a_Column);

            if (a_Media is TwitchLive) {
                m_Live = (TwitchLive)a_Media;
                TitleComponent.Text = m_Live.Title;
                LiveThumbnailComponent.Source = m_Live.Thumbnail;
                ChannelNameComponent.Text = m_Live.ChannelName;
                ChannelLogoComponent.Source = m_Live.ChannelImage;
                var t_converter = new System.Windows.Media.BrushConverter();
                LiveCircle.Foreground = (Brush)t_converter.ConvertFromString(m_Live.IsLive ? "#e91916" : "#9c7a6f");
                DateComponent.Text = m_Live.PublicationDate.ToShortDateString();
                SinceComponent.Text = m_Live.PublicatedSince;
                IsLiveTextComponent.Foreground = (Brush)t_converter.ConvertFromString(m_Live.IsLive ? "#FFF" : "#8f8f8f");
            }
            else if (a_Media is YoutubeVideo) {
                m_vid = (YoutubeVideo)a_Media;
                TitleComponent.Text = m_vid.Title;
                LiveThumbnailComponent.Source = m_vid.Thumbnail;
                ChannelLogoComponent.Source = m_vid.ChannelImage;
                ChannelNameComponent.Text = string.Format("{0} views", m_vid.Views);
                var t_converter = new System.Windows.Media.BrushConverter();
                ButtonComponent.Background = (Brush)t_converter.ConvertFromString("#000000");
                TimeSpan t_DeltaRelease = DateTime.UtcNow - m_vid.PublicationDate;

                SinceComponent.Text = t_DeltaRelease.Days > 365 ? String.Format("{0} Ans, {1} Mois", (t_DeltaRelease.Days / 365), (t_DeltaRelease.Days / 31) - (t_DeltaRelease.Days / 365) * 12) :
                                t_DeltaRelease.Days > 31 ? String.Format("{0} Mois, {1} Jours", (t_DeltaRelease.Days / 31), t_DeltaRelease.Days - (t_DeltaRelease.Days / 31) * 31) :
                                t_DeltaRelease.Days > 0 ? String.Format("{0} Jours, {1} Heures", t_DeltaRelease.Days, t_DeltaRelease.Hours) :
                                t_DeltaRelease.Hours > 0 ? String.Format("{0} Heures, {1} Minutes", t_DeltaRelease.Hours, t_DeltaRelease.Minutes) :
                                t_DeltaRelease.Minutes > 0 ? String.Format("{0} Minutes, {1} Secondes", t_DeltaRelease.Minutes, t_DeltaRelease.Seconds) :
                                String.Format("{0} Secondes", t_DeltaRelease.Seconds);

                Boolean t_isLive = (m_vid.Views == 0 && t_DeltaRelease.TotalSeconds > 60);
                Boolean t_IsRecent = (t_DeltaRelease.Hours < 2) && (t_DeltaRelease.Days == 0);
                LiveCircle.Foreground = (Brush)t_converter.ConvertFromString(t_isLive ? (t_IsRecent ? "#e91916" : "#9c7a6f") : "#000000FF");
                DateComponent.Text = m_vid.PublicationDate.ToShortDateString();
                IsLiveTextComponent.Foreground = (Brush)t_converter.ConvertFromString(t_isLive ? (t_IsRecent ? "#FFF" : "#8f8f8f") : "#000000FF");
            }
        }

        public void OpenLiveLink(object a_sender, RoutedEventArgs a_e) {
            System.Diagnostics.Process.Start(m_Live == null ? m_vid.Link : m_Live.Link);
        }
    }
}
