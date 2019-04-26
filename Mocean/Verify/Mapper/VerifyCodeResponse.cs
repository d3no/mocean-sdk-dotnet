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
    public class VerifyCodeResponse : AbstractResponse
    {
        [JsonProperty("reqid")]
        [XmlElement("reqid")]
        public string ReqId { get; set; }

        [JsonProperty("msgid")]
        [XmlElement("msgid")]
        public string MsgId { get; set; }

        [JsonProperty("price")]
        [XmlElement("price")]
        public string Price { get; set; }

        [JsonProperty("currency")]
        [XmlElement("currency")]
        public string Currency { get; set; }
    }
}
