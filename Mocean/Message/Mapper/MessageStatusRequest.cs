using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Message
{
    public class MessageStatusRequest
    {
        [JsonProperty("mocean-msgid")]
        public string mocean_msgid { get; set; }

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
