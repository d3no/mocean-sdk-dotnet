﻿using Newtonsoft.Json;
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
        [JsonProperty("calls")]
        [XmlArray("calls")]
        [XmlArrayItem("call")]
        public List<Call> Calls { get; set; }

        [XmlRoot("call")]
        public class Call
        {
            [JsonProperty("status")]
            [XmlElement("status")]
            public string Status { get; set; }

            [JsonProperty("receiver")]
            [XmlElement("receiver")]
            public string Receiver { get; set; }

            [JsonProperty("session_uuid")]
            [XmlElement("session_uuid")]
            public string SessionUuid { get; set; }

            [JsonProperty("call_uuid")]
            [XmlElement("call_uuid")]
            public string CallUuid { get; set; }
        }
    }
}
