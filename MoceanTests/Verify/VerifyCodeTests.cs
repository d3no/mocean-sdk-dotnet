using MoceanTests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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
        public void InquiryTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                    Assert.AreEqual("/verify/check", uri);
                })
                .Returns(() => TestingUtils.ReadFile("verify_code.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.VerifyCode.Send(new VerifyCodeRequest
            {
                mocean_code = "testing code",
                mocean_reqid = "testing reqid"
            });
        }

        [Test]
        public void JsonVerifyCodeResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("verify_code.json");
            var res = (VerifyCodeResponse)ResponseFactory.CreateObjectfromRawResponse<VerifyCodeResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            this.TestObject(res);
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
            this.TestObject(res);
        }

        private void TestObject(VerifyCodeResponse verifyCodeResponse)
        {
            Assert.AreEqual(verifyCodeResponse.Status, "0");
            Assert.AreEqual(verifyCodeResponse.ReqId, "CPASS_restapi_C0000002737000000.0002");
            Assert.AreEqual(verifyCodeResponse.MsgId, "CPASS_restapi_C0000002737000000.0002");
            Assert.AreEqual(verifyCodeResponse.Price, "0.35");
            Assert.AreEqual(verifyCodeResponse.Currency, "MYR");
        }
    }
}