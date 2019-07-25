using MoceanTests;
using NUnit.Framework;
using System.Net.Http;

namespace Mocean.Account.Tests
{
    [TestFixture]
    public class BalanceTests
    {
        [Test]
        public void BalanceRequestTest()
        {
            var balanceRequest = new BalanceRequest
            {
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(balanceRequest.mocean_resp_format);
            Assert.AreEqual(balanceRequest.mocean_resp_format, "json");

            balanceRequest = new BalanceRequest();
            Assert.IsNull(balanceRequest.mocean_resp_format);
            balanceRequest.mocean_resp_format = "json";
            Assert.AreEqual(balanceRequest.mocean_resp_format, "json");
        }

        [Test]
        public void JsonInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/account/balance"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("balance.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Balance.Inquiry();
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("balance.json"));
            TestObject(res);
        }

        [Test]
        public void XmlInquiryTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/account/balance"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("balance.xml");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Balance.Inquiry(new BalanceRequest
            {
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("balance.xml"));
            TestObject(res);
        }

        private static void TestObject(BalanceResponse balanceResponse)
        {
            Assert.AreEqual(balanceResponse.Status, "0");
            Assert.AreEqual(balanceResponse.Value, "100.0000");
        }
    }
}