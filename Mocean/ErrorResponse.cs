using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean
{
    [XmlRoot("result")]
    public class ErrorResponse : AbstractResponse
    {
        [JsonProperty("err_msg")]
        [XmlElement("err_msg")]
        public string ErrMsg { get; set; }
    }
}
