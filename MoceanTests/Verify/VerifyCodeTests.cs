using Mocean.Exceptions;
using MoceanTests;
using NUnit.Framework;
using System.Net.Http;

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
        public void RequiredFieldNotSetTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    return TestingUtils.GetResponse("verify_code.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            Assert.Throws<RequiredFieldException>(() =>
            {
                mocean.VerifyCode.Send(new VerifyCodeRequest());
            });

        }

        [Test]
        public void JsonSendTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/verify/check"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("verify_code.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.VerifyCode.Send(new VerifyCodeRequest
            {
                mocean_code = "testing code",
                mocean_reqid = "testing reqid"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("verify_code.json"));
            TestObject(res);
        }

        [Test]
        public void XmlSendTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/verify/check"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("verify_code.xml");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.VerifyCode.Send(new VerifyCodeRequest
            {
                mocean_code = "testing code",
                mocean_reqid = "testing reqid",
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("verify_code.xml"));
            TestObject(res);
        }

        private static void TestObject(VerifyCodeResponse verifyCodeResponse)
        {
            Assert.AreEqual(verifyCodeResponse.Status, "0");
            Assert.AreEqual(verifyCodeResponse.ReqId, "CPASS_restapi_C0000002737000000.0002");
        }
    }
}