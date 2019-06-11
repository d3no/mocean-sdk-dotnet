using MoceanTests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mocean.Account.Tests
{
    [TestFixture]
    public class PricingTests
    {
        [Test]
        public void PricingRequestTest()
        {
            var pricingRequest = new PricingRequest
            {
                mocean_mcc = "test mcc",
                mocean_mnc = "test mnc",
                mocean_delimiter = "test delimiter",
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(pricingRequest.mocean_mcc);
            Assert.AreEqual(pricingRequest.mocean_mcc, "test mcc");
            Assert.IsNotNull(pricingRequest.mocean_mnc);
            Assert.AreEqual(pricingRequest.mocean_mnc, "test mnc");
            Assert.IsNotNull(pricingRequest.mocean_delimiter);
            Assert.AreEqual(pricingRequest.mocean_delimiter, "test delimiter");
            Assert.IsNotNull(pricingRequest.mocean_resp_format);
            Assert.AreEqual(pricingRequest.mocean_resp_format, "json");

            pricingRequest = new PricingRequest();
            Assert.IsNull(pricingRequest.mocean_mcc);
            Assert.IsNull(pricingRequest.mocean_mnc);
            Assert.IsNull(pricingRequest.mocean_delimiter);
            Assert.IsNull(pricingRequest.mocean_resp_format);
            pricingRequest.mocean_mcc = "test mcc";
            Assert.AreEqual(pricingRequest.mocean_mcc, "test mcc");
            pricingRequest.mocean_mnc = "test mnc";
            Assert.AreEqual(pricingRequest.mocean_mnc, "test mnc");
            pricingRequest.mocean_delimiter = "test delimiter";
            Assert.AreEqual(pricingRequest.mocean_delimiter, "test delimiter");
            pricingRequest.mocean_resp_format = "json";
            Assert.AreEqual(pricingRequest.mocean_resp_format, "json");
        }

        [Test]
        public void InquiryTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/account/pricing", uri);
                })
                .Returns(() => TestingUtils.ReadFile("price.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.Pricing.Inquiry();

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void JsonPricingResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("price.json");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/account/pricing", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(jsonResponse, System.Net.HttpStatusCode.OK, false, "/account/pricing"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.Pricing.Inquiry();
            Assert.AreEqual(res.ToString(), jsonResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void XmlPricingResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("price.xml");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/account/pricing", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(xmlResponse, System.Net.HttpStatusCode.OK, true, "/account/pricing"));

            apiRequestMock.Object.ApiRequestConfig.Version = "1";
            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.Pricing.Inquiry();
            Assert.AreEqual(res.ToString(), xmlResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);


            //V2 Test
            xmlResponse = TestingUtils.ReadFile("price_v2.xml");
            apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/account/pricing", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(xmlResponse, System.Net.HttpStatusCode.OK, true, "/account/pricing"));

            apiRequestMock.Object.ApiRequestConfig.Version = "2";
            mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            res = mocean.Pricing.Inquiry();
            Assert.AreEqual(res.ToString(), xmlResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        private static void TestObject(PricingResponse pricingResponse)
        {
            Assert.AreEqual(pricingResponse.Status, "0");
            Assert.AreEqual(pricingResponse.Destinations.Count, 25);
            Assert.AreEqual(pricingResponse.Destinations[0].Country, "Default");
            Assert.AreEqual(pricingResponse.Destinations[0].Operator, "Default");
            Assert.AreEqual(pricingResponse.Destinations[0].Mcc, "Default");
            Assert.AreEqual(pricingResponse.Destinations[0].Mnc, "Default");
            Assert.AreEqual(pricingResponse.Destinations[0].Price, "2.0000");
            Assert.AreEqual(pricingResponse.Destinations[0].Currency, "MYR");
        }
    }
}