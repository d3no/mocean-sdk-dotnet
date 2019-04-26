using MoceanTests;
using NUnit.Framework;

namespace Mocean.Verify.Tests
{
    [TestFixture]
    public class VerifyCodeTests
    {
        [Test]
        public void VerifyCodeRequestTest()
        {
            var verifyCodeRequest = new VerifyCodeRequest
            {
                mocean_code = "test code",
                mocean_reqid = "test reqid",
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(verifyCodeRequest.mocean_code);
            Assert.AreEqual(verifyCodeRequest.mocean_code, "test code");
            Assert.IsNotNull(verifyCodeRequest.mocean_reqid);
            Assert.AreEqual(verifyCodeRequest.mocean_reqid, "test reqid");
            Assert.IsNotNull(verifyCodeRequest.mocean_resp_format);
            Assert.AreEqual(verifyCodeRequest.mocean_resp_format, "json");

            verifyCodeRequest = new VerifyCodeRequest();
            Assert.IsNull(verifyCodeRequest.mocean_code);
            Assert.IsNull(verifyCodeRequest.mocean_reqid);
            Assert.IsNull(verifyCodeRequest.mocean_resp_format);
            verifyCodeRequest.mocean_code = "test code";
            Assert.AreEqual(verifyCodeRequest.mocean_code, "test code");
            verifyCodeRequest.mocean_reqid = "test reqid";
            Assert.AreEqual(verifyCodeRequest.mocean_reqid, "test reqid");
            verifyCodeRequest.mocean_resp_format = "json";
            Assert.AreEqual(verifyCodeRequest.mocean_resp_format, "json");
        }

        [Test]
        public void JsonVerifyCodeResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("verify_code.json");
            var res = (VerifyCodeResponse)ResponseFactory.CreateObjectfromRawResponse<VerifyCodeResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            Assert.AreEqual(res.Status, "0");
        }

        [Test]
        public void XmlVerifyCodeResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("verify_code.xml");
            var res = (VerifyCodeResponse)ResponseFactory.CreateObjectfromRawResponse<VerifyCodeResponse>(xmlResponse
                    .Replace("<verify_check>", "")
                    .Replace("</verify_check>", "")
                ).SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            Assert.AreEqual(res.Status, "0");
        }
    }
}