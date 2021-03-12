using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace agregateurMetier
{
    public class CsGoUpdateitems
    {
        [JsonProperty("items")]
        public List<CsGoUpdate> ListItems {get; set;}
    }
}
