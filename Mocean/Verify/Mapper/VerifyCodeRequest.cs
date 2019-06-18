using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Verify
{
    public class VerifyCodeRequest
    {
        [JsonProperty("mocean-reqid")]
        public string mocean_reqid { get; set; }

        [JsonProperty("mocean-code")]
        public string mocean_code { get; set; }

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
