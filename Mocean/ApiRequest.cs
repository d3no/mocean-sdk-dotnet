using System;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;
using Mocean.Exceptions;

namespace Mocean
{
    public class ApiRequest
    {
        public const string REST_API = "https://rest.moceanapi.com/rest/1";
        public const string PL = "DOTNET-SDK";
        private string Uri { get; set; }
        public HttpStatusCode ResponseCode { get; private set; }
        private string _response;
        public string Response
        {
            get
            {
                if (this.ResponseCode >= HttpStatusCode.BadRequest)
                {
                    throw new MoceanErrorException(
                        (ErrorResponse)ResponseFactory.CreateObjectfromRawResponse<ErrorResponse>(
                                _response
                                    .Replace("<verify_request>", "")
                                    .Replace("</verify_request>", "")
                                    .Replace("<verify_check>", "")
                                    .Replace("</verify_check>", "")
                            ).SetRawResponse(_response)
                    );
                }
                return _response;
            }
            private set => _response = value;
        }

        public ApiRequest(string uri, string method, IDictionary<string, string> parameters)
        {
            this.Uri = uri;
            if (method.Equals("get", StringComparison.InvariantCultureIgnoreCase))
            {
                this.DoGetRequest(parameters);
            }
            else if (method.Equals("post", StringComparison.InvariantCultureIgnoreCase))
            {
                this.DoPostRequest(parameters);
            }
        }

        private void DoGetRequest(IDictionary<string, string> parameters)
        {
            // Check all passed parameter is null or empty
            if (string.IsNullOrEmpty(this.Uri) || parameters == null)
            {
                return;
            }

            string url = REST_API + this.Uri + "?" + BuildQueryString(parameters);
            var request = WebRequest.Create(url);

            this.Response = this.ReadResponse(request);
        }

        private void DoPostRequest(IDictionary<string, string> parameters)
        {
            byte[] paramData = null;

            var request = WebRequest.Create(REST_API + this.Uri);

            if (parameters != null)
            {
                paramData = Encoding.ASCII.GetBytes(BuildQueryString(parameters).ToString());
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = paramData.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(paramData, 0, paramData.Length);
                }
            }

            this.Response = this.ReadResponse(request);
        }

        private string ReadResponse(WebRequest webRequest)
        {
            string res;

            try
            {
                using (WebResponse response = webRequest.GetResponse())
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    this.ResponseCode = httpResponse.StatusCode;
                    using (Stream data = httpResponse.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        res = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    this.ResponseCode = httpResponse.StatusCode;
                    using (Stream data = httpResponse.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        res = reader.ReadToEnd();
                    }
                }
            }

            return res;
        }

        private StringBuilder BuildQueryString(IDictionary<string, string> parameters)
        {
            parameters["mocean-medium"] = PL;

            var sortedParams = new SortedDictionary<string, string>(parameters);
            var sb = new StringBuilder();
            foreach (var kvp in sortedParams)
            {
                JsonConvert.SerializeObject(kvp.Key, Formatting.Indented);
                sb.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
            }

            return sb;
        }
    }
}
