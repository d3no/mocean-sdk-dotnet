using Mocean.Exceptions;
using NUnit.Framework;

namespace Mocean.Auth.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void ConstructWithEmptyParametersTest()
        {
            Basic basic = new Basic
            {
                ApiKey = "test api key",
                ApiSecret = "test api secret"
            };

            Assert.AreEqual(basic.ApiKey, "test api key");
            Assert.AreEqual(basic.ApiSecret, "test api secret");
        }

        [Test]
        public void ConstructUsingParametersTest()
        {
            Basic basic = new Basic("test api key", "test api secret");
            Assert.AreEqual(basic.ApiKey, "test api key");
            Assert.AreEqual(basic.ApiSecret, "test api secret");
        }

        [Test]
        public void ConstructUsingCredentialsObjectTest()
        {
            Basic basic = new Basic(new Credential
            {
                ApiKey = "test api key",
                ApiSecret = "test api secret"
            });
            Assert.AreEqual(basic.ApiKey, "test api key");
            Assert.AreEqual(basic.ApiSecret, "test api secret");
        }

        [Test]
        public void GetParamsTest()
        {
            Basic basic = new Basic("test api key", "test api secret");
            Assert.IsTrue(basic.GetParams().ContainsKey("mocean-api-key"));
            Assert.IsTrue(basic.GetParams().ContainsKey("mocean-api-secret"));

            Assert.AreEqual(basic.GetParams()["mocean-api-key"], "test api key");
            Assert.AreEqual(basic.GetParams()["mocean-api-secret"], "test api secret");

            Assert.Throws<RequiredFieldException>(() =>
            {
                basic = new Basic();
                basic.GetParams();
            });
        }
    }
}