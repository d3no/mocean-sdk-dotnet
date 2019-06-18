using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean.Account
{
    [XmlRoot("result")]
    public class BalanceResponse : AbstractResponse
    {
        [JsonProperty("value")]
        [XmlElement("value")]
        public string Value { get; set; }
    }
}
