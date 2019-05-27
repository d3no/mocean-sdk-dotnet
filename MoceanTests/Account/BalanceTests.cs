using MoceanTests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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
        public void InquiryTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/account/balance", uri);
                })
                .Returns(() => TestingUtils.ReadFile("balance.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.Balance.Inquiry();
        }

        [Test]
        public void JsonBalanceResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("balance.json");
            var res = (BalanceResponse)ResponseFactory.CreateObjectfromRawResponse<BalanceResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            this.TestObject(res);
        }

        [Test]
        public void XmlBalanceResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("balance.xml");
            var res = (BalanceResponse)ResponseFactory.CreateObjectfromRawResponse<BalanceResponse>(xmlResponse)
                .SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            this.TestObject(res);
        }

        private void TestObject(BalanceResponse balanceResponse)
        {
            Assert.AreEqual(balanceResponse.Status, "0");
            Assert.AreEqual(balanceResponse.Value, "100.0000");
        }
    }
}