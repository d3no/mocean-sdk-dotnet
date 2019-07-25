using Mocean;
using Mocean.Auth;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace MoceanTests
{
    public static class TestingUtils
    {
        public static string ReadFile(string fileName)
        {
            string text;
            var fileStream = new FileStream(TestContext.CurrentContext.TestDirectory + "/Resources/" + fileName, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }

            return text;
        }

        public static Client GetClientObj()
        {
            return TestingUtils.GetClientObj(new ApiRequest(ApiRequestConfig.make()));
        }

        public static Client GetClientObj(ApiRequest apiRequest)
        {
            return new Client(new Basic("test api key", "test api secret"), apiRequest);
        }

        public static HttpResponseMessage GetResponse(string fileName, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            HttpResponseMessage response = new HttpResponseMessage(statusCode);
            response.Content = new StringContent(TestingUtils.ReadFile(fileName));
            return response;
        }

        public static HttpClient GetMockHttpClient(Func<HttpRequestMessage, HttpResponseMessage> testRequest)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Fallback.Respond(testRequest);

            return mockHttp.ToHttpClient();
        }

        public static string GetTestUri(string uri, string version = "2")
        {
            return "/rest/" + version + uri;
        }

        public static Dictionary<string, string> RewindBody(HttpContent content)
        {
            var dictData = new Dictionary<string, string>();
            var stringData = content.ReadAsStringAsync().Result;
            var nvcData = HttpUtility.ParseQueryString(stringData);
            foreach (var key in nvcData.AllKeys)
            {
                dictData.Add(key, nvcData.Get(key));
            }
            return dictData;
        }
    }
}
