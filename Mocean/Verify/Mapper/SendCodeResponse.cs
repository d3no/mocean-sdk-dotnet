using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean.Verify
{
    [XmlRoot("result")]
    public class SendCodeResponse : AbstractResponse
    {
        [JsonProperty("reqid")]
        [XmlElement("reqid")]
        public string ReqId { get; set; }

        [JsonProperty("is_number_reachable")]
        [XmlElement("is_number_reachable")]
        public string IsNumberReachable { get; set; }

        [JsonProperty("resend_number")]
        [XmlElement("resend_number")]
        public string ResendNumber { get; set; }

        [JsonProperty("to")]
        [XmlElement("to")]
        public string To { get; set; }

        [XmlIgnoreAttribute]
        public SendCode Client { get; set; }

        public SendCodeResponse Resend()
        {
            return this.Client.Resend(new SendCodeRequest
            {
                mocean_reqid = this.ReqId
            });
        }
    }
}
