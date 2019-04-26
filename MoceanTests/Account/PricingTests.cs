using MoceanTests;
using NUnit.Framework;

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
        public void JsonPricingResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("price.json");
            var res = (PricingResponse)ResponseFactory.CreateObjectfromRawResponse<PricingResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            Assert.AreEqual(res.Status, "0");
        }

        [Test]
        public void XmlPricingResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("price.xml");
            var res = (PricingResponse)ResponseFactory.CreateObjectfromRawResponse<PricingResponse>(xmlResponse)
                .SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            Assert.AreEqual(res.Status, "0");
        }
    }
}