using Mocean;
using Mocean.Auth;
using NUnit.Framework;
using System.IO;
using System.Text;

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
    }
}
