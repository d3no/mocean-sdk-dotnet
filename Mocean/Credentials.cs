using Newtonsoft.Json;

namespace Mocean
{
    public class Credentials
    {
        /// <summary>
        /// Mocean API Key (from your account dashboard)
        /// </summary>
        [JsonProperty("mocean-api-key")]
        public string mocean_api_key { get; set; }
        /// <summary>
        /// Mocean API Secret (from your account dashboard)
        /// </summary>
        [JsonProperty("mocean-api-secret")]
        public string mocean_api_secret { get; set; }

    }
}
