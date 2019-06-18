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
using System.Net.Http;

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

            HttpResponseMessage response;
            string res;
            using (var httpClient = new HttpClient())
            {
                if(method.Equals("get", StringComparison.CurrentCultureIgnoreCase))
                {
                    response = httpClient.GetAsync(this.ApiRequestConfig.BaseUrl + "/rest/" + this.ApiRequestConfig.Version + uri + "?" + BuildQueryString(parameters)).Result;
                }
                else
                {
                    response = httpClient.PostAsync(this.ApiRequestConfig.BaseUrl + "/rest/" + this.ApiRequestConfig.Version + uri, new FormUrlEncodedContent(parameters)).Result;
                }

                res = response.Content.ReadAsStringAsync().Result;
            }

            return this.FormatResponse(res, response.StatusCode, parameters["mocean-resp-format"].Equals("xml", StringComparison.CurrentCultureIgnoreCase), uri);
        }

        public string FormatResponse(string responseString, HttpStatusCode responseCode, bool isXml, string uri)
        {
            this.RawResponse = responseString;
            var tempString = responseString;

            //remove these field for v1, no effect for v2
            tempString = tempString
                .Replace("<verify_request>", "")
                .Replace("</verify_request>", "")
                .Replace("<verify_check>", "")
                .Replace("</verify_check>", "");

            if (isXml && this.ApiRequestConfig.Version.Equals("1") && !string.IsNullOrEmpty(uri))
            {
                if (uri.Equals("/account/pricing", StringComparison.CurrentCultureIgnoreCase))
                {
                    tempString = tempString
                        .Replace("<data>", "<destinations>")
                        .Replace("</data>", "</destinations>");
                }
                else if (uri.Equals("/sms", StringComparison.CurrentCultureIgnoreCase))
                {
                    tempString = tempString
                        .Replace("<result>", "<result><messages>")
                        .Replace("</result>", "</messages></result>");
                }
            }

            if (responseCode >= HttpStatusCode.BadRequest)
            {
                throw new MoceanErrorException(
                        (ErrorResponse)ResponseFactory.CreateObjectfromRawResponse<ErrorResponse>(tempString).SetRawResponse(this.RawResponse)
                    );
            }

            //these check is for v1 cause v1 http response code is not > 400, no effect for v2
            var tempParsedObject = ResponseFactory.CreateObjectfromRawResponse<GenericModel>(tempString);
            if (tempParsedObject.Status != null && !tempParsedObject.Status.Equals("0", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new MoceanErrorException(
                        (ErrorResponse)ResponseFactory.CreateObjectfromRawResponse<ErrorResponse>(tempString).SetRawResponse(this.RawResponse)
                    );
            }

            return tempString;
        }

        public static StringBuilder BuildQueryString(IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            foreach (var kvp in parameters)
            {
                JsonConvert.SerializeObject(kvp.Key, Formatting.Indented);
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.AppendFormat("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
            }

            return sb;
        }
    }
}
