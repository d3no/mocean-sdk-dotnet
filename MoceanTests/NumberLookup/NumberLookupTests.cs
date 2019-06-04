using MoceanTests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mocean.NumberLookup.Tests
{
    [TestFixture]
    public class NumberLookupTests
    {
        [Test]
        public void NumberLookupRequestTest()
        {
            var numberLookupRequest = new NumberLookupRequest
            {
                mocean_to = "test to",
                mocean_nl_url = "test nlurl",
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(numberLookupRequest.mocean_to);
            Assert.AreEqual(numberLookupRequest.mocean_to, "test to");
            Assert.IsNotNull(numberLookupRequest.mocean_nl_url);
            Assert.AreEqual(numberLookupRequest.mocean_nl_url, "test nlurl");
            Assert.IsNotNull(numberLookupRequest.mocean_resp_format);
            Assert.AreEqual(numberLookupRequest.mocean_resp_format, "json");

            numberLookupRequest = new NumberLookupRequest();
            Assert.IsNull(numberLookupRequest.mocean_to);
            Assert.IsNull(numberLookupRequest.mocean_nl_url);
            Assert.IsNull(numberLookupRequest.mocean_resp_format);
            numberLookupRequest.mocean_to = "test to";
            Assert.AreEqual(numberLookupRequest.mocean_to, "test to");
            numberLookupRequest.mocean_nl_url = "test nlurl";
            Assert.AreEqual(numberLookupRequest.mocean_nl_url, "test nlurl");
            numberLookupRequest.mocean_resp_format = "json";
            Assert.AreEqual(numberLookupRequest.mocean_resp_format, "json");
        }

        [Test]
        public void InquiryTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/nl", uri);
                })
                .Returns(() => TestingUtils.ReadFile("number_lookup.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.NumberLookup.Inquiry(new NumberLookupRequest
            {
                mocean_to = "testing to"
            });

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void JsonNumberLookupResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("number_lookup.json");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/nl", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(jsonResponse, System.Net.HttpStatusCode.OK, false, "/nl"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.NumberLookup.Inquiry(new NumberLookupRequest
            {
                mocean_to = "testing to"
            });
            Assert.AreEqual(res.ToString(), jsonResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void XmlNumberLookupResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("number_lookup.xml");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/nl", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(xmlResponse, System.Net.HttpStatusCode.OK, true, "/nl"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.NumberLookup.Inquiry(new NumberLookupRequest
            {
                mocean_to = "testing to"
            });
            Assert.AreEqual(res.ToString(), xmlResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        private static void TestObject(NumberLookupResponse numberLookupResponse)
        {
            Assert.AreEqual(numberLookupResponse.Status, "0");
            Assert.AreEqual(numberLookupResponse.MsgId, "CPASS_restapi_C00000000000000.0002");
            Assert.AreEqual(numberLookupResponse.To, "60123456789");
            Assert.AreEqual(numberLookupResponse.Ported, "ported");
            Assert.AreEqual(numberLookupResponse.Reachable, "reachable");
            Assert.AreEqual(numberLookupResponse.CurrentCarrier.Country, "MY");
            Assert.AreEqual(numberLookupResponse.CurrentCarrier.Name, "U Mobile");
            Assert.AreEqual(numberLookupResponse.CurrentCarrier.NetworkCode, "50218");
            Assert.AreEqual(numberLookupResponse.CurrentCarrier.Mcc, "502");
            Assert.AreEqual(numberLookupResponse.CurrentCarrier.Mnc, "18");
            Assert.AreEqual(numberLookupResponse.OriginalCarrier.Country, "MY");
            Assert.AreEqual(numberLookupResponse.OriginalCarrier.Name, "Maxis Mobile");
            Assert.AreEqual(numberLookupResponse.OriginalCarrier.NetworkCode, "50212");
            Assert.AreEqual(numberLookupResponse.OriginalCarrier.Mcc, "502");
            Assert.AreEqual(numberLookupResponse.OriginalCarrier.Mnc, "12");
        }
    }
}