using System.Collections.Generic;

namespace agregateurMetier
{
    public class SubReddit
    {
        public string SubRedditName { get; set; }
        public string SubRedditLink { get; set; }
        public List<RedditPost> SubRedditPosts { get; set; }
    }
}
