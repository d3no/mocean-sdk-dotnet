using MoceanTests;
using NUnit.Framework;

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
        public void JsonSendCodeResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("send_code.json");
            var res = (SendCodeResponse)ResponseFactory.CreateObjectfromRawResponse<SendCodeResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            Assert.AreEqual(res.Status, "0");
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
            Assert.AreEqual(res.Status, "0");
        }
    }
}