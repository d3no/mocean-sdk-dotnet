using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean
{
    public class ResponseFactory
    {
        public static T CreateObjectfromRawResponse<T>(string rawResponse)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(rawResponse);
            }
            catch (JsonReaderException)
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                T result;

                using (TextReader reader = new StringReader(rawResponse))
                {
                    result = (T)xmlSerializer.Deserialize(reader);
                }

                return result;
            }
        }
    }
}
