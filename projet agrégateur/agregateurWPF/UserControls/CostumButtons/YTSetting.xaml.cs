using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class YTSetting : UserControl
    {
        private YoutubeChannel m_BufferChannel;
        private SettingsPageControlleur m_settingcontrolleur;

        public YTSetting(SettingsPageControlleur a_settingcontrolleur,YoutubeChannel a_channel) {
            InitializeComponent();
            m_settingcontrolleur = a_settingcontrolleur;

            if (a_channel != null) {
                m_BufferChannel = a_channel;
                ChannelName.Text = a_channel.ChannelName;
                ChannelLink.Text = a_channel.ChannelLink;
                ChannelImg.Text = a_channel.ChannelImageLink;
            }
            else {
                m_BufferChannel = new YoutubeChannel();
                ChannelName.Text = "Enter NAME";
                ChannelLink.Text = "https://www.youtube.com/feeds/videos.xml?user=NAME";
            }
        }

        public YoutubeChannel Setting {
            get {
                m_BufferChannel.ChannelName = ChannelName.Text;
                m_BufferChannel.ChannelImageLink = ChannelImg.Text;
                m_BufferChannel.ChannelLink = ChannelLink.Text;
                return m_BufferChannel;
            }
        }

        private void DeleteSetting_Click(object a_sender, RoutedEventArgs a_e) {
            m_settingcontrolleur.RemoveFromBuffer(this);
        }
    }
}
