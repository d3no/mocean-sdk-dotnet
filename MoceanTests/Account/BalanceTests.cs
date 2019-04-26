using MoceanTests;
using NUnit.Framework;

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
        public void JsonBalanceResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("balance.json");
            var res = (BalanceResponse)ResponseFactory.CreateObjectfromRawResponse<BalanceResponse>(jsonResponse)
                .SetRawResponse(jsonResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), jsonResponse);
            Assert.AreEqual(res.Status, "0");
        }

        [Test]
        public void XmlBalanceResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("balance.xml");
            var res = (BalanceResponse)ResponseFactory.CreateObjectfromRawResponse<BalanceResponse>(xmlResponse)
                .SetRawResponse(xmlResponse);

            Assert.IsInstanceOf(typeof(AbstractResponse), res);
            Assert.AreEqual(res.ToString(), xmlResponse);
            Assert.AreEqual(res.Status, "0");
        }
    }
}