using Mocean.Exceptions;
using MoceanTests;
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

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
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

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void ErrorResponseWith2xxStatusCodeTest()
        {
            string jsonErrorResponse = TestingUtils.ReadFile("error_response.json");
            var apiRequest = new ApiRequest();

            try
            {
                apiRequest.FormatResponse(jsonErrorResponse, System.Net.HttpStatusCode.Accepted, false, null);
                Assert.Fail();
            }
            catch (MoceanErrorException ex)
            {
                Assert.AreEqual(ex.Message, ex.ErrorResponse.ToString());
                Assert.AreEqual(jsonErrorResponse, ex.ErrorResponse.ToString());
                Assert.AreEqual("1", ex.ErrorResponse.Status);
            }

            try
            {
                apiRequest.FormatResponse(jsonErrorResponse, System.Net.HttpStatusCode.OK, false, null);
                Assert.Fail();
            }
            catch (MoceanErrorException ex)
            {
                Assert.AreEqual(ex.Message, ex.ErrorResponse.ToString());
                Assert.AreEqual(jsonErrorResponse, ex.ErrorResponse.ToString());
                Assert.AreEqual("1", ex.ErrorResponse.Status);
            }

            string xmlErrorResponse = TestingUtils.ReadFile("error_response.xml");

            try
            {
                apiRequest.FormatResponse(xmlErrorResponse, System.Net.HttpStatusCode.Accepted, false, null);
                Assert.Fail();
            }
            catch (MoceanErrorException ex)
            {
                Assert.AreEqual(ex.Message, ex.ErrorResponse.ToString());
                Assert.AreEqual(xmlErrorResponse, ex.ErrorResponse.ToString());
                Assert.AreEqual("1", ex.ErrorResponse.Status);
            }

            try
            {
                apiRequest.FormatResponse(xmlErrorResponse, System.Net.HttpStatusCode.OK, false, null);
                Assert.Fail();
            }
            catch (MoceanErrorException ex)
            {
                Assert.AreEqual(ex.Message, ex.ErrorResponse.ToString());
                Assert.AreEqual(xmlErrorResponse, ex.ErrorResponse.ToString());
                Assert.AreEqual("1", ex.ErrorResponse.Status);
            }
        }

        [Test]
        public void ErrorResponseWith4xxStatusCodeTest()
        {
            string jsonErrorResponse = TestingUtils.ReadFile("error_response.json");
            var apiRequest = new ApiRequest();

            try
            {
                apiRequest.FormatResponse(jsonErrorResponse, System.Net.HttpStatusCode.BadRequest, false, null);
                Assert.Fail();
            }
            catch (MoceanErrorException ex)
            {
                Assert.AreEqual(ex.Message, ex.ErrorResponse.ToString());
                Assert.AreEqual(jsonErrorResponse, ex.ErrorResponse.ToString());
                Assert.AreEqual("1", ex.ErrorResponse.Status);
            }
        }
    }
}
