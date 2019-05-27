using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Verify
{
    public class SendCodeRequest
    {
        [JsonProperty("mocean-to")]
        public string mocean_to { get; set; }

        [JsonProperty("mocean-brand")]
        public string mocean_brand { get; set; }

        [JsonProperty("mocean-from")]
        public string mocean_from { get; set; }

        [JsonProperty("mocean-code-length")]
        public string mocean_code_length { get; set; }

        [JsonProperty("mocean-template")]
        public string mocean_template { get; set; }

        [JsonProperty("mocean-pin-validity")]
        public string mocean_pin_validity { get; set; }

        [JsonProperty("mocean-next-event-wait")]
        public string mocean_next_event_wait { get; set; }

        [JsonProperty("mocean-reqid")]
        public string mocean_reqid { get; set; }

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
