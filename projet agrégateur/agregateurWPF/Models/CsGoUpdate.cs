using Newtonsoft.Json;
using System;

namespace agregateurMetier
{
    public class CsGoUpdate {
        [JsonProperty("title")]
        public string Title {get; set;}
        [JsonProperty("link")]
        public string Link {get; set;}
        [JsonProperty("pubDate")]
        public string pubDateStr { get; set; }
        public DateTime UpdateDate {get; set;}
        public string PublicatedSince { get; set; }
    }
}
