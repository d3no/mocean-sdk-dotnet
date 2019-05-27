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
        }

        [Test]
        public void SendCodeAsCPATest()
        {
            var mocean = new Client(new Basic("test api key", "test api secret"));
            var sendCode = mocean.SendCode;
            Assert.AreEqual(ChargeType.ChargePerConversion, sendCode.VerifyChargeType);
            sendCode.SendAs(ChargeType.ChargePerAttempt);
            Assert.AreEqual(ChargeType.ChargePerAttempt, sendCode.VerifyChargeType);

        }

        [Test]
        public void JsonSendCodeResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("send_code.json");
            var res = (SendCodeResponse)ResponseFactory.CreateObjectfromRawResponse<SendCodeResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            this.TestObject(res);
        }

        [Test]
        public void XmlSendCodeResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("send_code.xml");
            var res = (SendCodeResponse)ResponseFactory.CreateObjectfromRawResponse<SendCodeResponse>(xmlResponse
                    .Replace("<verify_request>", "")
                    .Replace("</verify_request>", "")
                ).SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            this.TestObject(res);
        }

        private void TestObject(SendCodeResponse sendCodeResponse)
        {
            Assert.AreEqual(sendCodeResponse.Status, "0");
            Assert.AreEqual(sendCodeResponse.ReqId, "CPASS_restapi_C0000002737000000.0002");
        }
    }
}