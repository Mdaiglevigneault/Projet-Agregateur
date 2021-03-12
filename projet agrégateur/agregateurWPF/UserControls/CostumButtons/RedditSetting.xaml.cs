using System.Windows;
using System.Windows.Controls;
using agregateurMetier;

namespace agregateurWPF
{
    /// <summary>
    /// Logique d'interaction pour YoutubePage.xaml
    /// </summary>
    public partial class RedditSetting : UserControl
    {
        private SubReddit m_BufferSred;
        private SettingsPageControlleur m_settingcontrolleur;

        public RedditSetting(SettingsPageControlleur a_settingcontrolleur,SubReddit a_Sred) {
            InitializeComponent();
            m_settingcontrolleur = a_settingcontrolleur;

            if (a_Sred != null) {
                m_BufferSred = a_Sred;
                SredName.Text = a_Sred.SubRedditName;
                SredLink.Text = a_Sred.SubRedditLink;
            }
            else {
                m_BufferSred = new SubReddit();
                SredName.Text = "Enter NAME";
                SredLink.Text = "https://www.reddit.com/r/NAME/.rss";
            }
        }

        public SubReddit Setting {
            get {
                m_BufferSred.SubRedditName = SredName.Text;
                m_BufferSred.SubRedditLink = SredLink.Text;
                return m_BufferSred;
            }
        }

        private void DeleteSetting_Click(object a_sender, RoutedEventArgs a_e) {
            m_settingcontrolleur.RemoveFromBuffer(this);
        }
    }
}
