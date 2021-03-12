using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class TwitchSetting : UserControl
    {
        private TwitchLive m_BufferChannel;
        private SettingsPageControlleur m_settingcontrolleur;

        public TwitchSetting(SettingsPageControlleur a_settingcontrolleur, TwitchLive a_channel) {
            InitializeComponent();
            m_settingcontrolleur = a_settingcontrolleur;

            if (a_channel != null) {
                m_BufferChannel = a_channel;
                ChannelLink.Text = a_channel.ChannelLink;
                ChannelImg.Text = a_channel.ChannelImageLink;
            }
            else {
                m_BufferChannel = new TwitchLive();
                ChannelLink.Text = "http://twitchrss.appspot.com/vod/NAME";
            }
        }

        public TwitchLive Setting {
            get {
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
