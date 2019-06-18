using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean
{
    public abstract class AbstractResponse
    {
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }

        [XmlIgnore]
        public string RawResponse { get; protected set; }

        public override string ToString()
        {
            return this.RawResponse;
        }

        public AbstractResponse SetRawResponse(string rawResponse)
        {
            this.RawResponse = rawResponse;
            return this;
        }
    }
}
