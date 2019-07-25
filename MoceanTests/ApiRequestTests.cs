using Mocean.Exceptions;
using MoceanTests;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mocean.Tests
{
    [TestFixture]
    public class ApiRequestTests
    {
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

        [Test]
        public void BuildQueryStringTest()
        {
            var result = ApiRequest.BuildQueryString(new Dictionary<string, string>
            {
                {"test", "testing"},
                {"test2", "testing2"}
            });

            Assert.AreEqual("test=testing&test2=testing2", result.ToString());
        }
    }
}
