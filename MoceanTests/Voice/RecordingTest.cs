using Mocean;
using Mocean.Exceptions;
using Mocean.Voice;
using Mocean.Voice.Mapper;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class RecordingTest
    {
        [Test]
        public void RecordingCallTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/voice/rec"), httpRequest.RequestUri.LocalPath);
                    var mockResponse = TestingUtils.GetResponse("recording.json");
                    mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
                    return mockResponse;
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Voice.Recording("xxx-xxx-xxx-xxx");
            Assert.IsNotNull(res.RecordingBuffer);
            Assert.AreEqual(res.Filename, "xxx-xxx-xxx-xxx.mp3");
        }

        [Test]
        public void RecordingErrorTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Get, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/voice/rec"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("error_response.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            Assert.Throws<MoceanErrorException>(() =>
            {
                var res = mocean.Voice.Recording("xxx-xxx-xxx-xxx");
            });
        }
    }
}
