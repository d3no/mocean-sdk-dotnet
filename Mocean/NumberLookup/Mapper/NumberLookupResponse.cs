using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean.NumberLookup
{
    [XmlRoot("result")]
    public class NumberLookupResponse : AbstractResponse
    {
        [JsonProperty("msgid")]
        [XmlElement("msgid")]
        public string MsgId { get; set; }

        [JsonProperty("to")]
        [XmlElement("to")]
        public string To { get; set; }

        [JsonProperty("imsi")]
        [XmlElement("imsi")]
        public string Imsi { get; set; }

        [JsonProperty("current_carrier")]
        [XmlElement("current_carrier")]
        public Carrier CurrentCarrier { get; set; }

        [JsonProperty("original_carrier")]
        [XmlElement("original_carrier")]
        public Carrier OriginalCarrier { get; set; }

        [JsonProperty("ported")]
        [XmlElement("ported")]
        public string Ported { get; set; }

        [JsonProperty("reachable")]
        [XmlElement("reachable")]
        public string Reachable { get; set; }

        public class Carrier
        {
            [JsonProperty("country")]
            [XmlElement("country")]
            public string Country { get; set; }

            [JsonProperty("name")]
            [XmlElement("name")]
            public string Name { get; set; }
        }
    }
}
