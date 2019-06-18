using NUnit.Framework;

namespace Mocean.Tests
{
    [TestFixture]
    public class ApiRequestConfigTests
    {
        [Test]
        public void MakeMethodShouldReturnInstanceTest()
        {
            Assert.IsInstanceOf(typeof(ApiRequestConfig), ApiRequestConfig.make());
        }

        [Test]
        public void ApiRequestConfigShouldHaveDefaultValueTest()
        {
            var apiRequest = new ApiRequestConfig();

            Assert.IsNotNull(apiRequest.BaseUrl);
            Assert.IsNotNull(apiRequest.Version);
        }

        [Test]
        public void AbleToChangeConfigThroughPropertiesTest()
        {
            var apiRequest = new ApiRequestConfig();

            apiRequest.BaseUrl = "test base url";
            Assert.IsNotNull(apiRequest.BaseUrl);
            Assert.AreEqual("test base url", apiRequest.BaseUrl);

            apiRequest.Version = "2";
            Assert.IsNotNull(apiRequest.Version);
            Assert.AreEqual("2", apiRequest.Version);
        }

        [Test]
        public void AbleToChangeConfigThroughConstructorTest()
        {
            var apiRequest = new ApiRequestConfig("test base url", "2");

            Assert.AreEqual("test base url", apiRequest.BaseUrl);
            Assert.AreEqual("2", apiRequest.Version);
        }
    }
}
