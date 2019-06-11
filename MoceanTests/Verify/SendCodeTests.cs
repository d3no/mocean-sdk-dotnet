using Mocean.Auth;
using MoceanTests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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
                mocean_reqid= "test req id",
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
        public void InquiryTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                    Assert.AreEqual("/verify/req", uri);
                })
                .Returns(() => TestingUtils.ReadFile("send_code.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.SendCode.Send(new SendCodeRequest
            {
                mocean_to = "testing to",
                mocean_brand = "testing brand"
            });

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void SendCodeAsSmsTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                    Assert.AreEqual("/verify/req/sms", uri);
                })
                .Returns(() => TestingUtils.ReadFile("send_code.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var sendCode = mocean.SendCode;
            Assert.AreEqual(Channel.Auto, sendCode.Channel);
            sendCode.SendAs(Channel.Sms);
            Assert.AreEqual(Channel.Sms, sendCode.Channel);
            sendCode.Send(new SendCodeRequest
            {
                mocean_to = "testing to",
                mocean_brand = "testing brand"
            });

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void ResendTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                    Assert.AreEqual("/verify/resend/sms", uri);
                })
                .Returns(() => TestingUtils.ReadFile("send_code.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.SendCode.Resend(new SendCodeRequest
            {
                mocean_reqid = "test req id"
            });

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void ResendThroughResponseObjectTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                    Assert.AreEqual("/verify/resend/sms", uri);
                    Assert.AreEqual("CPASS_restapi_C0000002737000000.0002", parameters["mocean-reqid"]);
                })
                .Returns(() => TestingUtils.ReadFile("resend_code.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var sendCodeSampleResponse = TestingUtils.ReadFile("send_code.json");
            var sendCodeResponse = (SendCodeResponse)ResponseFactory.CreateObjectfromRawResponse<SendCodeResponse>(sendCodeSampleResponse)
                .SetRawResponse(sendCodeSampleResponse);
            sendCodeResponse.Client = mocean.SendCode;

            var resendCodeResponse = sendCodeResponse.Resend();
            Assert.AreEqual(resendCodeResponse.Status, "0");
            Assert.AreEqual(resendCodeResponse.ReqId, "CPASS_restapi_C0000002737000000.0002");
            Assert.AreEqual(resendCodeResponse.To, "60123456789");
            Assert.AreEqual(resendCodeResponse.ResendNumber, "1");

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void JsonSendCodeResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("send_code.json");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                    Assert.AreEqual("/verify/req", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(jsonResponse, System.Net.HttpStatusCode.OK, false, "/verify/req"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.SendCode.Send(new SendCodeRequest
            {
                mocean_to = "testing to",
                mocean_brand = "testing brand"
            });
            Assert.AreEqual(res.ToString(), jsonResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void XmlSendCodeResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("send_code.xml");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                    Assert.AreEqual("/verify/req", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(xmlResponse, System.Net.HttpStatusCode.OK, true, "/verify/req"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.SendCode.Send(new SendCodeRequest
            {
                mocean_to = "testing to",
                mocean_brand = "testing brand"
            });
            Assert.AreEqual(res.ToString(), xmlResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        private static void TestObject(SendCodeResponse sendCodeResponse)
        {
            Assert.AreEqual(sendCodeResponse.Status, "0");
            Assert.AreEqual(sendCodeResponse.ReqId, "CPASS_restapi_C0000002737000000.0002");
        }
    }
}