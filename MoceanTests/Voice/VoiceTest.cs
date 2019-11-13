using Mocean;
using Mocean.Exceptions;
using Mocean.Voice;
using Mocean.Voice.Mapper;
using Mocean.Voice.McObj;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

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
                mocean_event_url = "test event url",
                mocean_command = "test mocean command",
                mocean_resp_format = "json"
            };

            Assert.IsNotNull(voiceRequest.mocean_to);
            Assert.AreEqual(voiceRequest.mocean_to, "test to");
            Assert.IsNotNull(voiceRequest.mocean_event_url);
            Assert.AreEqual(voiceRequest.mocean_event_url, "test event url");
            Assert.IsNotNull(voiceRequest.mocean_command);
            Assert.AreEqual(voiceRequest.mocean_command, "test mocean command");
            Assert.IsNotNull(voiceRequest.mocean_resp_format);
            Assert.AreEqual(voiceRequest.mocean_resp_format, "json");

            voiceRequest = new VoiceRequest();
            Assert.IsNull(voiceRequest.mocean_to);
            Assert.IsNull(voiceRequest.mocean_event_url);
            Assert.IsNull(voiceRequest.mocean_command);
            Assert.IsNull(voiceRequest.mocean_resp_format);
            voiceRequest.mocean_to = "test to";
            Assert.AreEqual(voiceRequest.mocean_to, "test to");
            voiceRequest.mocean_event_url = "test event url";
            Assert.AreEqual(voiceRequest.mocean_event_url, "test event url");
            voiceRequest.mocean_resp_format = "json";
            Assert.AreEqual(voiceRequest.mocean_resp_format, "json");

            //test multiple types of mocean_command
            var dictionaryParams = new Dictionary<string, object> { { "action", "say" } };
            voiceRequest.mocean_command = dictionaryParams;
            Assert.AreEqual(voiceRequest.mocean_command, JsonConvert.SerializeObject(new List<Dictionary<string, object>>
                    {
                        dictionaryParams
                    }));

            voiceRequest = new VoiceRequest();
            var builderParams = (new McBuilder()).add(Mc.say("hello world"));
            voiceRequest.mocean_command = builderParams;
            Assert.IsNotNull(voiceRequest.mocean_command);
            Assert.AreEqual(JsonConvert.SerializeObject(builderParams.build()), voiceRequest.mocean_command);

            voiceRequest = new VoiceRequest();
            var mcParams = new Say { Text = "hello world" };
            voiceRequest.mocean_command = mcParams;
            Assert.IsNotNull(voiceRequest.mocean_command);
            Assert.AreEqual(JsonConvert.SerializeObject((new McBuilder()).add(mcParams).build()), voiceRequest.mocean_command);
        }

        [Test]
        public void RequiredFieldNotSetTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    return TestingUtils.GetResponse("voice.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            Assert.Throws<RequiredFieldException>(() =>
            {
                mocean.Voice.Call(new VoiceRequest());
            });

        }

        [Test]
        public void JsonCallTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/voice/dial"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("voice.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Voice.Call(new VoiceRequest
            {
                mocean_to = "testing to"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("voice.json"));
            TestObject(res);
        }

        [Test]
        public void XmlCallTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/voice/dial"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("voice.xml");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Voice.Call(new VoiceRequest
            {
                mocean_to = "testing to",
                mocean_resp_format = "xml"
            });
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("voice.xml"));
            TestObject(res);
        }

        [Test]
        public void JsonHangupTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    var dictBody = TestingUtils.RewindBody(httpRequest.Content);
                    Assert.AreEqual("xxx-xxx-xxx-xxx", dictBody["mocean-call-uuid"]);
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/voice/hangup"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("hangup.json");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Voice.Hangup("xxx-xxx-xxx-xxx");
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("hangup.json"));
            Assert.AreEqual(res.Status, "0");
        }

        [Test]
        public void XmlHangupTest()
        {
            var apiRequestMock = new ApiRequest(
                TestingUtils.GetMockHttpClient((HttpRequestMessage httpRequest) =>
                {
                    var dictBody = TestingUtils.RewindBody(httpRequest.Content);
                    Assert.AreEqual("xxx-xxx-xxx-xxx", dictBody["mocean-call-uuid"]);
                    Assert.AreEqual(HttpMethod.Post, httpRequest.Method);
                    Assert.AreEqual(TestingUtils.GetTestUri("/voice/hangup"), httpRequest.RequestUri.LocalPath);
                    return TestingUtils.GetResponse("hangup.xml");
                })
            );

            var mocean = TestingUtils.GetClientObj(apiRequestMock);
            var res = mocean.Voice.Hangup("xxx-xxx-xxx-xxx");
            Assert.AreEqual(res.ToString(), TestingUtils.ReadFile("hangup.xml"));
            Assert.AreEqual(res.Status, "0");
        }

        private static void TestObject(VoiceResponse voiceResponse)
        {
            Assert.AreEqual(voiceResponse.Calls[0].Status, "0");
            Assert.AreEqual(voiceResponse.Calls[0].Receiver, "60123456789");
            Assert.AreEqual(voiceResponse.Calls[0].CallUuid, "xxx-xxx-xxx-xxx");
            Assert.AreEqual(voiceResponse.Calls[0].SessionUuid, "xxx-xxx-xxx-xxx");
        }
    }
}
