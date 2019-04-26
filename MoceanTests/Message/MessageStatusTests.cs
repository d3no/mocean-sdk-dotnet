using MoceanTests;
using NUnit.Framework;

namespace Mocean.Message.Tests
{
    [TestFixture]
    public class MessageStatusTests
    {
        [Test]
        public void MessageStatusRequestTest()
        {
            var messageStatusRequest = new MessageStatusRequest
            {
                mocean_msgid = "test msgid",
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(messageStatusRequest.mocean_msgid);
            Assert.AreEqual(messageStatusRequest.mocean_msgid, "test msgid");
            Assert.IsNotNull(messageStatusRequest.mocean_resp_format);
            Assert.AreEqual(messageStatusRequest.mocean_resp_format, "json");

            messageStatusRequest = new MessageStatusRequest();
            Assert.IsNull(messageStatusRequest.mocean_msgid);
            Assert.IsNull(messageStatusRequest.mocean_resp_format);
            messageStatusRequest.mocean_msgid = "test msgid";
            Assert.AreEqual(messageStatusRequest.mocean_msgid, "test msgid");
            messageStatusRequest.mocean_resp_format = "json";
            Assert.AreEqual(messageStatusRequest.mocean_resp_format, "json");
        }

        [Test]
        public void JsonMessageStatusResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("message_status.json");
            var res = (MessageStatusResponse)ResponseFactory.CreateObjectfromRawResponse<MessageStatusResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            Assert.AreEqual(res.Status, "0");
        }

        [Test]
        public void XmlMessageStatusResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("message_status.xml");
            var res = (MessageStatusResponse)ResponseFactory.CreateObjectfromRawResponse<MessageStatusResponse>(xmlResponse)
                .SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            Assert.AreEqual(res.Status, "0");
        }
    }
}