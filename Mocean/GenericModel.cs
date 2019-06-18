using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean
{
    [XmlRoot("result")]
    public class GenericModel
    {
        [XmlElement("status")]
        public string Status { get; set; }
    }
}
