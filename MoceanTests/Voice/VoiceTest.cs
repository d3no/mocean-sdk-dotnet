using Mocean;
using Mocean.Exceptions;
using Mocean.Voice;
using Mocean.Voice.Mapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class VoiceTest
    {
        [Test]
        public void VoiceRequestTest()
        {
            var voiceRequest = new VoiceRequest
            {
                mocean_to = "test to",
                mocean_call_event_url = "test call event url",
                mocean_call_control_commands = "test control commands",
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(voiceRequest.mocean_to);
            Assert.AreEqual(voiceRequest.mocean_to, "test to");
            Assert.IsNotNull(voiceRequest.mocean_call_event_url);
            Assert.AreEqual(voiceRequest.mocean_call_event_url, "test call event url");
            Assert.IsNotNull(voiceRequest.mocean_call_control_commands);
            Assert.AreEqual(voiceRequest.mocean_call_control_commands, "test control commands");
            Assert.IsNotNull(voiceRequest.mocean_resp_format);
            Assert.AreEqual(voiceRequest.mocean_resp_format, "json");

            voiceRequest = new VoiceRequest();
            Assert.IsNull(voiceRequest.mocean_to);
            Assert.IsNull(voiceRequest.mocean_call_event_url);
            Assert.IsNull(voiceRequest.mocean_call_control_commands);
            Assert.IsNull(voiceRequest.mocean_resp_format);
            voiceRequest.mocean_to = "test to";
            Assert.AreEqual(voiceRequest.mocean_to, "test to");
            voiceRequest.mocean_call_event_url = "test call event url";
            Assert.AreEqual(voiceRequest.mocean_call_event_url, "test call event url");
            voiceRequest.mocean_resp_format = "json";
            Assert.AreEqual(voiceRequest.mocean_resp_format, "json");

            //test multiple types of mocean_call_event_url
            var dictionaryParams = new Dictionary<string, object> { { "action", "say" } };
            voiceRequest.mocean_call_control_commands = dictionaryParams;
            Assert.AreEqual(voiceRequest.mocean_call_control_commands, JsonConvert.SerializeObject(new List<Dictionary<string, object>>
                    {
                        dictionaryParams
                    }));

            voiceRequest = new VoiceRequest();
            var builderParams = (new McccBuilder()).add(Mccc.say("hello world"));
            voiceRequest.mocean_call_control_commands = builderParams;
            Assert.IsNotNull(voiceRequest.mocean_call_control_commands);
            Assert.AreEqual(JsonConvert.SerializeObject(builderParams.build()), voiceRequest.mocean_call_control_commands);

            voiceRequest = new VoiceRequest();
            var mcccParams = new Say { Text = "hello world" };
            voiceRequest.mocean_call_control_commands = mcccParams;
            Assert.IsNotNull(voiceRequest.mocean_call_control_commands);
            Assert.AreEqual(JsonConvert.SerializeObject((new McccBuilder()).add(mcccParams).build()), voiceRequest.mocean_call_control_commands);
        }

        [Test]
        public void RequiredFieldNotSetTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Returns("testing only");

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            Assert.Throws<RequiredFieldException>(() =>
            {
                mocean.Voice.Call(new VoiceRequest());
            });

        }

        [Test]
        public void CallTest()
        {
            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/voice/dial", uri);
                })
                .Returns(() => TestingUtils.ReadFile("voice.json"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            mocean.Voice.Call(new VoiceRequest
            {
                mocean_to = "testing to"
            });

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void JsonNumberLookupResponseTest()
        {
            string jsonResponse = TestingUtils.ReadFile("voice.json");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/voice/dial", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(jsonResponse, System.Net.HttpStatusCode.OK, false, "/voice/dial"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.Voice.Call(new VoiceRequest
            {
                mocean_to = "testing to"
            });
            Assert.AreEqual(res.ToString(), jsonResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void XmlNumberLookupResponseTest()
        {
            string xmlResponse = TestingUtils.ReadFile("voice.xml");

            var apiRequestMock = new Mock<ApiRequest>();
            apiRequestMock.Setup(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Callback((string method, string uri, IDictionary<string, string> parameters) =>
                {
                    Assert.AreEqual("get", method);
                    Assert.AreEqual("/voice/dial", uri);
                })
                .Returns(() => apiRequestMock.Object.FormatResponse(xmlResponse, System.Net.HttpStatusCode.OK, true, "/voice/dial"));

            var mocean = TestingUtils.GetClientObj(apiRequestMock.Object);
            var res = mocean.Voice.Call(new VoiceRequest
            {
                mocean_to = "testing to"
            });
            Assert.AreEqual(res.ToString(), xmlResponse);
            TestObject(res);

            apiRequestMock.Verify(apiRequest => apiRequest.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
        }

        private static void TestObject(VoiceResponse voiceResponse)
        {
            Assert.AreEqual(voiceResponse.Status, "0");
            Assert.AreEqual(voiceResponse.CallUuid, "xxx-xxx-xxx-xxx");
            Assert.AreEqual(voiceResponse.SessionUuid, "xxx-xxx-xxx-xxx");
        }
    }
}
