using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean.Voice.Mapper
{
    [XmlRoot("result")]
    public class VoiceResponse : AbstractResponse
    {
        [JsonProperty("session-uuid")]
        [XmlElement("session-uuid")]
        public string SessionUuid { get; set; }

        [JsonProperty("call-uuid")]
        [XmlElement("call-uuid")]
        public string CallUuid { get; set; }
    }
}
