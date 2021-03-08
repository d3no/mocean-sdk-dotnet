using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean.Command.Mapper
{
    public class CommandResponse : AbstractResponse
    {
        [JsonProperty("session_uuid")]
        [XmlElement("session_uuid")]
        public string SessionUUID { get; set; }

        [JsonProperty("mocean_command_resp")]
        [XmlArray("mocea_command_resp")]
        [XmlArrayItem("mc_res")]
        public List<McRes> MoceanCommandResp { get; set; }

        [XmlRoot("mc_res")]
        public class McRes 
        {
            [JsonProperty("action")]
            [XmlElement("action")]
            public string Action;

            [JsonProperty("message_id")]
            [XmlElement("message_id")]
            public string MessageID;

            [JsonProperty("mc_position")]
            [XmlElement("mc_position")]
            public int McPosition;

            [JsonProperty("total_message_segments")]
            [XmlElement("total_message_segments")]
            public int TotalMessageSegments;
        }

    }
}
