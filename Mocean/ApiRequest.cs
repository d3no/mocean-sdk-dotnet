using System;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mocean
{
    public class ApiRequest
    {
        public const string REST_API = "https://rest.moceanapi.com/rest/1";
        public const string PL = "DOTNET-SDK";
       
        public string subdomain { get; set; }

        private static StringBuilder BuildQueryString(IDictionary<string, string> parameters, Credentials creds = null)
        {
            var mocean_api_key = creds.mocean_api_key;
            var mocean_api_secret = creds.mocean_api_secret;

            var sb = new StringBuilder();
            Action<IDictionary<string, string>, StringBuilder> buildStringFromParams = (param, strings) =>
            {
                foreach (var kvp in param)
                {
                    JsonConvert.SerializeObject(kvp.Key, Formatting.Indented);
                    strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
                }
            };
            parameters.Add("mocean-api-key", mocean_api_key);
            parameters.Add("mocean-api-secret", mocean_api_secret);
            parameters.Add("mocean-medium", PL);
            
            // security secret provided, sort and sign request
     
            var sortedParams = new SortedDictionary<string, string>(parameters);
            buildStringFromParams(sortedParams, sb);
            
            return sb;
        }

        internal static Dictionary<string, string> GetParameters(object parameters)
        {
            var paramType = parameters.GetType().GetTypeInfo();
            var apiParams = new Dictionary<string, string>();
            foreach (var property in paramType.GetProperties())
            {
                string jsonPropertyName = null;

                if (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Any())
                {
                    jsonPropertyName =
                        ((JsonPropertyAttribute)property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).First())
                            .PropertyName;
                }

                if (null == paramType.GetProperty(property.Name).GetValue(parameters, null)) continue;

                apiParams.Add(string.IsNullOrEmpty(jsonPropertyName) ? property.Name : jsonPropertyName,
                    paramType.GetProperty(property.Name).GetValue(parameters, null).ToString());
            }
            return apiParams;
        }

        public static MoceanResponse HTTP_GET(string subdomain, Dictionary<string, string> parameters, Credentials creds)
        {
            MoceanResponse mRes = new MoceanResponse();
            // Check all passed parameter is null or empty
            if (string.IsNullOrEmpty(subdomain) || parameters == null || creds == null)
                return mRes;
            else
            {
                string url = REST_API + subdomain + "?" +BuildQueryString(parameters, creds);
                
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                mRes.HttpResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return mRes;
            }
        }

        public static MoceanResponse HTTP_POST(string subdomain, Dictionary<string, string> parameters, Credentials creds)
        {
            byte[] data = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(REST_API + subdomain);

            if (parameters != null)
            {
                data = Encoding.ASCII.GetBytes(BuildQueryString(parameters, creds).ToString());

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return new MoceanResponse
                {
                    HttpResponse = responseString
                };
            }
            else
            {
                return new MoceanResponse
                {
                    HttpResponse = string.Empty
                };
            }
        }

        internal static MoceanResponse DoGetRequest(string uri, object parameters, Credentials creds = null)
        {
            var apiParams = GetParameters(parameters);
            return DoGetRequest(uri, apiParams, creds);
        }

        internal static MoceanResponse DoPostRequest(string uri, object parameters, Credentials creds = null)
        {
            var apiParams = GetParameters(parameters);
            return DoPostRequest(uri, apiParams, creds);
        }

        internal static MoceanResponse DoGetRequest(string uri, Dictionary<string, string> parameters, Credentials creds = null) => HTTP_GET(uri, parameters, creds);
        internal static MoceanResponse DoPostRequest(string uri, Dictionary<string, string> parameters, Credentials creds = null) => HTTP_POST(uri, parameters, creds);
    }
}
