using Mocean.Exceptions;
using MoceanTests;
using NUnit.Framework;
using System.Net.Http;

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
        public void RequiredFieldNotSetTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    return TestingUtils.GetResponse("message_status.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            Assert.Throws<RequiredFieldException>(() =>
            {
                mocean.MessageStatus.Inquiry(new MessageStatusRequest());
            });

        }

        [Test]
        public void JsonInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/report/message"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("message_status.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.MessageStatus.Inquiry(new MessageStatusRequest
            {
                mocean_msgid = "test msg id"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("message_status.json"));
            TestObject(res);
        }

        [Test]
        public void XmlInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/report/message"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("message_status.xml");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.MessageStatus.Inquiry(new MessageStatusRequest
            {
                mocean_msgid = "test msg id",
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("message_status.xml"));
            TestObject(res);
        }

        private static void TestObject(MessageStatusResponse messageStatusResponse)
        {
            Assert.AreEqual(messageStatusResponse.Status, "0");
            Assert.AreEqual(messageStatusResponse.MessageStatus, "5");
            Assert.AreEqual(messageStatusResponse.MsgId, "CPASS_restapi_C0000002737000000.0001");
            Assert.AreEqual(messageStatusResponse.CreditDeducted, "0.0000");
        }
    }
}