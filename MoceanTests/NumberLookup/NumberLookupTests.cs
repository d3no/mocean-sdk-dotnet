using MoceanTests;
using NUnit.Framework;

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
        public void JsonNumberLookupResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("number_lookup.json");
            var res = (NumberLookupResponse)ResponseFactory.CreateObjectfromRawResponse<NumberLookupResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            Assert.AreEqual(res.Status, "0");
            Assert.AreEqual(res.OriginalCarrier.Country, "MY");
        }

        [Test]
        public void XmlNumberLookupResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("number_lookup.xml");
            var res = (NumberLookupResponse)ResponseFactory.CreateObjectfromRawResponse<NumberLookupResponse>(xmlResponse)
                .SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            Assert.AreEqual(res.Status, "0");
            Assert.AreEqual(res.OriginalCarrier.Country, "MY");
        }
    }
}