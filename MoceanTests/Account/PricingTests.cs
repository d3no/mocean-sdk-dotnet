using MoceanTests;
using NUnit.Framework;
using System.Net.Http;

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
        public void JsonInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/account/pricing"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("price.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Pricing.Inquiry();
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("price.json"));
            TestObject(res);
        }

        [Test]
        public void XmlInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/account/pricing", "1"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("price.xml");
                })
            );

            apiRequestMock.ApiRequestConfig.Version = "1";
            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Pricing.Inquiry(new PricingRequest
            {
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("price.xml"));
            TestObject(res);

            //V2 Test
            apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/account/pricing"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("price_v2.xml");
                })
            );

            apiRequestMock.ApiRequestConfig.Version = "2";
            mocean = TestingUtils.GetClientObj(apiRequestMock);
            res = mocean.Pricing.Inquiry(new PricingRequest
            {
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("price_v2.xml"));
            TestObject(res);
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