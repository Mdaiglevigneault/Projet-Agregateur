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
    public partial class Twitch_MediaBtn : UserControl
    {
        private TwitchLive m_Live = null;

        public Twitch_MediaBtn(Object a_Media, int a_Row, int a_Column)
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
        }

        public void OpenLiveLink(object a_sender, RoutedEventArgs a_e) {
            System.Diagnostics.Process.Start(m_Live.Link);
        }
    }
}
