using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mocean.Tests
{
    [TestFixture]
    public class ApiRequestTests
    {
        [Test]
        public void GetMethodTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                })
                .Returns("testing only");

            var apiRequestObj = apiRequestMock.Object;
            Assert.AreEqual("testing only", apiRequestObj.Get("testing uri", new Dictionary<string, string>()));
        }

        [Test]
        public void PostMethodTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("post", method);
                })
                .Returns("testing only");

            var apiRequestObj = apiRequestMock.Object;
            Assert.AreEqual("testing only", apiRequestObj.Post("testing uri", new Dictionary<string, string>()));
        }
    }
}
