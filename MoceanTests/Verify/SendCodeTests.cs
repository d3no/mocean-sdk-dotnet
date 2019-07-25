using Mocean.Exceptions;
using MoceanTests;
using NUnit.Framework;
using System.Net.Http;

namespace Mocean.Verify.Tests
{
    [TestFixture]
    public class SendCodeTests
    {
        [Test]
        public void sendCodeRequest()
        {
            var sendCodeRequest = new SendCodeRequest
            {
                mocean_brand = "test brand",
                mocean_code_length = "test codelength",
                mocean_from = "test from",
                mocean_next_event_wait = "test nexteventwait",
                mocean_pin_validity = "test pinvalidity",
                mocean_template = "test template",
                mocean_to = "test to",
                mocean_reqid = "test req id",
                mocean_request_nl = "test request nl",
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(sendCodeRequest.mocean_brand);
            Assert.AreEqual(sendCodeRequest.mocean_brand, "test brand");
            Assert.IsNotNull(sendCodeRequest.mocean_code_length);
            Assert.AreEqual(sendCodeRequest.mocean_code_length, "test codelength");
            Assert.IsNotNull(sendCodeRequest.mocean_from);
            Assert.AreEqual(sendCodeRequest.mocean_from, "test from");
            Assert.IsNotNull(sendCodeRequest.mocean_next_event_wait);
            Assert.AreEqual(sendCodeRequest.mocean_next_event_wait, "test nexteventwait");
            Assert.IsNotNull(sendCodeRequest.mocean_pin_validity);
            Assert.AreEqual(sendCodeRequest.mocean_pin_validity, "test pinvalidity");
            Assert.IsNotNull(sendCodeRequest.mocean_template);
            Assert.AreEqual(sendCodeRequest.mocean_template, "test template");
            Assert.IsNotNull(sendCodeRequest.mocean_to);
            Assert.AreEqual(sendCodeRequest.mocean_to, "test to");
            Assert.IsNotNull(sendCodeRequest.mocean_reqid);
            Assert.AreEqual(sendCodeRequest.mocean_reqid, "test req id");
            Assert.IsNotNull(sendCodeRequest.mocean_request_nl);
            Assert.AreEqual(sendCodeRequest.mocean_request_nl, "test request nl");
            Assert.IsNotNull(sendCodeRequest.mocean_resp_format);
            Assert.AreEqual(sendCodeRequest.mocean_resp_format, "json");

            sendCodeRequest = new SendCodeRequest();
            Assert.IsNull(sendCodeRequest.mocean_brand);
            Assert.IsNull(sendCodeRequest.mocean_code_length);
            Assert.IsNull(sendCodeRequest.mocean_from);
            Assert.IsNull(sendCodeRequest.mocean_next_event_wait);
            Assert.IsNull(sendCodeRequest.mocean_pin_validity);
            Assert.IsNull(sendCodeRequest.mocean_template);
            Assert.IsNull(sendCodeRequest.mocean_to);
            Assert.IsNull(sendCodeRequest.mocean_reqid);
            Assert.IsNull(sendCodeRequest.mocean_request_nl);
            Assert.IsNull(sendCodeRequest.mocean_resp_format);
            sendCodeRequest.mocean_brand = "test brand";
            Assert.AreEqual(sendCodeRequest.mocean_brand, "test brand");
            sendCodeRequest.mocean_code_length = "test codelength";
            Assert.AreEqual(sendCodeRequest.mocean_code_length, "test codelength");
            sendCodeRequest.mocean_from = "test from";
            Assert.AreEqual(sendCodeRequest.mocean_from, "test from");
            sendCodeRequest.mocean_next_event_wait = "test nexteventwait";
            Assert.AreEqual(sendCodeRequest.mocean_next_event_wait, "test nexteventwait");
            sendCodeRequest.mocean_pin_validity = "test pinvalidity";
            Assert.AreEqual(sendCodeRequest.mocean_pin_validity, "test pinvalidity");
            sendCodeRequest.mocean_template = "test template";
            Assert.AreEqual(sendCodeRequest.mocean_template, "test template");
            sendCodeRequest.mocean_to = "test to";
            Assert.AreEqual(sendCodeRequest.mocean_to, "test to");
            sendCodeRequest.mocean_reqid = "test req id";
            Assert.AreEqual(sendCodeRequest.mocean_reqid, "test req id");
            sendCodeRequest.mocean_request_nl = "test request nl";
            Assert.AreEqual(sendCodeRequest.mocean_request_nl, "test request nl");
            sendCodeRequest.mocean_resp_format = "json";
            Assert.AreEqual(sendCodeRequest.mocean_resp_format, "json");
        }

        [Test]
        public void RequiredFieldNotSetTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    return TestingUtils.GetResponse("send_code.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            Assert.Throws<RequiredFieldException>(() =>
            {
                mocean.SendCode.Send(new SendCodeRequest());
            });

        }

        [Test]
        public void SendCodeAsSmsTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/verify/req/sms"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("send_code.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var sendCode = mocean.SendCode;
            Assert.AreEqual(Channel.Auto, sendCode.Channel);
            sendCode.SendAs(Channel.Sms);
            Assert.AreEqual(Channel.Sms, sendCode.Channel);
            sendCode.Send(new SendCodeRequest
            {
                mocean_to = "testing to",
                mocean_brand = "testing brand"
            });
        }

        [Test]
        public void ResendTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/verify/resend/sms"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("resend_code.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            mocean.SendCode.Resend(new SendCodeRequest
            {
                mocean_reqid = "test req id"
            });
        }

        [Test]
        public void ResendThroughResponseObjectTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    var dictBody = TestingUtils.RewindBody(httpRequest.Content);
                    Assert.AreEqual("CPASS_restapi_C0000002737000000.0002", dictBody["mocean-reqid"]);
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/verify/resend/sms"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("resend_code.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var sendCodeResponse = (SendCodeResponse)ResponseFactory.CreateObjectfromRawResponse<SendCodeResponse>(TestingUtils.ReadFile("resend_code.json"))
                .SetRawResponse(TestingUtils.ReadFile("resend_code.json"));
            sendCodeResponse.Client = mocean.SendCode;

            var resendCodeResponse = sendCodeResponse.Resend();
            Assert.AreEqual(resendCodeResponse.Status, "0");
            Assert.AreEqual(resendCodeResponse.ReqId, "CPASS_restapi_C0000002737000000.0002");
            Assert.AreEqual(resendCodeResponse.To, "60123456789");
            Assert.AreEqual(resendCodeResponse.ResendNumber, "1");
        }

        [Test]
        public void JsonSendTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/verify/req"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("send_code.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.SendCode.Send(new SendCodeRequest
            {
                mocean_to = "testing to",
                mocean_brand = "testing brand"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("send_code.json"));
            TestObject(res);
        }

        [Test]
        public void XmlSendTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/verify/req"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("send_code.xml");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.SendCode.Send(new SendCodeRequest
            {
                mocean_to = "testing to",
                mocean_brand = "testing brand",
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("send_code.xml"));
            TestObject(res);
        }

        private static void TestObject(SendCodeResponse sendCodeResponse)
        {
            Assert.AreEqual(sendCodeResponse.Status, "0");
            Assert.AreEqual(sendCodeResponse.ReqId, "CPASS_restapi_C0000002737000000.0002");
        }
    }
}