using MoceanTests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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
        public void InquiryTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/report/message", uri);
                })
                .Returns(() => TestingUtils.ReadFile("message_status.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.MessageStatus.Inquiry(new MessageStatusRequest
            {
                mocean_msgid = "test msg id"
            });
        }

        [Test]
        public void JsonMessageStatusResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("message_status.json");
            var res = (MessageStatusResponse)ResponseFactory.CreateObjectfromRawResponse<MessageStatusResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            this.TestObject(res);
        }

        [Test]
        public void XmlMessageStatusResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("message_status.xml");
            var res = (MessageStatusResponse)ResponseFactory.CreateObjectfromRawResponse<MessageStatusResponse>(xmlResponse)
                .SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            this.TestObject(res);
        }

        private void TestObject(MessageStatusResponse messageStatusResponse)
        {
            Assert.AreEqual(messageStatusResponse.Status, "0");
            Assert.AreEqual(messageStatusResponse.MessageStatus, "5");
            Assert.AreEqual(messageStatusResponse.MsgId, "CPASS_restapi_C0000002737000000.0001");
            Assert.AreEqual(messageStatusResponse.CreditDeducted, "0.0000");
        }
    }
}