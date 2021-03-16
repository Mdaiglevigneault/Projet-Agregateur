using agregateurMetier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agregateurWPF.Models
{
    public class Profile
    {
        public int Id;
        public string Name;
        public List<RedditEntry> SubReddits;
        public List<YoutubeEntry> YoutubeChannels;
        public List<TwitchEntry> TwitchLives;

        public Profile() {
            SubReddits = new List<RedditEntry>();
            YoutubeChannels = new List<YoutubeEntry>();
            TwitchLives = new List<TwitchEntry>();
        }

        public void clean() {
            SubReddits = new List<RedditEntry>();
            YoutubeChannels = new List<YoutubeEntry>();
            TwitchLives = new List<TwitchEntry>();
        }
    }
}
