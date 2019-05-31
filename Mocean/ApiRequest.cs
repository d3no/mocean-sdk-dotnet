using System;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;
using Mocean.Exceptions;
using Newtonsoft.Json.Linq;

namespace Mocean
{
    public class ApiRequest
    {
        public ApiRequestConfig ApiRequestConfig { get; set; }

        public string RawResponse { get; private set; }

        public ApiRequest(ApiRequestConfig apiRequestConfig)
        {
            this.ApiRequestConfig = apiRequestConfig;
        }

        public ApiRequest() : this(ApiRequestConfig.make()) { }

        public string Get(string uri, IDictionary<string, string> parameters)
        {
            return this.Send("get", uri, parameters);
        }

        public string Post(string uri, IDictionary<string, string> parameters)
        {
            return this.Send("post", uri, parameters);
        }

        public virtual string Send(string method, string uri, IDictionary<string, string> parameters)
        {
            parameters["mocean-medium"] = "DOTNET-SDK";

            //use json if default not set
            if (!parameters.ContainsKey("mocean-resp-format"))
            {
                parameters["mocean-resp-format"] = "json";
            }

            WebRequest request;
            if (method.Equals("get", StringComparison.InvariantCultureIgnoreCase))
            {
                request = WebRequest.Create(this.ApiRequestConfig.BaseUrl + "/rest/" + this.ApiRequestConfig.Version + uri + "?" + this.BuildQueryString(parameters));
            }
            else
            {
                request = WebRequest.Create(this.ApiRequestConfig.BaseUrl + "/rest/" + this.ApiRequestConfig.Version + uri);

                if (parameters != null)
                {
                    var paramData = Encoding.ASCII.GetBytes(BuildQueryString(parameters).ToString());
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = paramData.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(paramData, 0, paramData.Length);
                    }
                }
            }

            string res;
            HttpStatusCode responseCode;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    responseCode = httpResponse.StatusCode;
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
                    responseCode = httpResponse.StatusCode;
                    using (Stream data = httpResponse.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        res = reader.ReadToEnd();
                    }
                }
            }

            return this.FormatResponse(res, responseCode, parameters["mocean-resp-format"].Equals("xml", StringComparison.InvariantCultureIgnoreCase), uri);
        }

        public string FormatResponse(string responseString, HttpStatusCode responseCode, bool isXml, string uri)
        {
            this.RawResponse = responseString;

            //remove these field for v1, no effect for v2
            responseString = responseString
                .Replace("<verify_request>", "")
                .Replace("</verify_request>", "")
                .Replace("<verify_check>", "")
                .Replace("</verify_check>", "");

            if (isXml && this.ApiRequestConfig.Version.Equals("1") && !string.IsNullOrEmpty(uri))
            {
                if (uri.Equals("/account/pricing", StringComparison.InvariantCultureIgnoreCase))
                {
                    responseString = responseString
                        .Replace("<data>", "<destinations>")
                        .Replace("</data>", "</destinations>");
                }
                else if (uri.Equals("/sms", StringComparison.InvariantCultureIgnoreCase))
                {
                    responseString = responseString
                        .Replace("<result>", "<result><messages>")
                        .Replace("</result>", "</messages></result>");
                }
            }

            if (responseCode >= HttpStatusCode.BadRequest)
            {
                throw new MoceanErrorException(
                        (ErrorResponse)ResponseFactory.CreateObjectfromRawResponse<ErrorResponse>(responseString).SetRawResponse(this.RawResponse)
                    );
            }

            //these check is for v1 cause v1 http response code is not > 400, no effect for v2
            var tempParsedObject = ResponseFactory.CreateObjectfromRawResponse<GenericModel>(responseString);
            if (tempParsedObject.Status != null && !tempParsedObject.Status.Equals("0", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new MoceanErrorException(
                        (ErrorResponse)ResponseFactory.CreateObjectfromRawResponse<ErrorResponse>(responseString).SetRawResponse(this.RawResponse)
                    );
            }

            return responseString;
        }

        private StringBuilder BuildQueryString(IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            foreach (var kvp in parameters)
            {
                JsonConvert.SerializeObject(kvp.Key, Formatting.Indented);
                sb.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
            }

            return sb;
        }
    }
}
