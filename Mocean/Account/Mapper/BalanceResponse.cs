using Newtonsoft.Json;
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
