using Mocean.Exceptions;
using MoceanTests;
using NUnit.Framework;
using System.Net.Http;

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
        public void RequiredFieldNotSetTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    return TestingUtils.GetResponse("number_lookup.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            Assert.Throws<RequiredFieldException>(() =>
            {
                mocean.NumberLookup.Inquiry(new NumberLookupRequest());
            });

        }

        [Test]
        public void JsonInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/nl"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("number_lookup.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.NumberLookup.Inquiry(new NumberLookupRequest
            {
                mocean_to = "testing to"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("number_lookup.json"));
            TestObject(res);
        }

        [Test]
        public void XmlInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/nl"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("number_lookup.xml");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.NumberLookup.Inquiry(new NumberLookupRequest
            {
                mocean_to = "testing to",
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("number_lookup.xml"));
            TestObject(res);
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