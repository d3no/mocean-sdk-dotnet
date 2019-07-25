using Mocean.Voice;
using NUnit.Framework;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class McccTest
    {
        [Test]
        public void McccSayTest()
        {
            var say = Mccc.say();
            say.Text = "testing text";
            Assert.AreEqual("testing text", say.GetRequestData()["text"]);

            Assert.AreEqual("testing text2", Mccc.say("testing text2").GetRequestData()["text"]);
        }

        [Test]
        public void McccBridgeTest()
        {
            var bridge = Mccc.bridge();
            bridge.To = "testing to";
            Assert.AreEqual("testing to", bridge.GetRequestData()["to"]);

            Assert.AreEqual("testing to2", Mccc.bridge("testing to2").GetRequestData()["to"]);
        }

        [Test]
        public void McccCollectTest()
        {
            var collect = Mccc.collect();
            collect.EventUrl = "testing event url";
            Assert.AreEqual("testing event url", collect.GetRequestData()["event-url"]);

            Assert.AreEqual("testing event url", Mccc.collect("testing event url").GetRequestData()["event-url"]);
        }

        [Test]
        public void McccPlayTest()
        {
            var play = Mccc.play();
            play.File = "testing file";
            Assert.AreEqual("testing file", play.GetRequestData()["file"]);

            Assert.AreEqual("testing file", Mccc.play("testing file").GetRequestData()["file"]);
        }

        [Test]
        public void McccSleepTest()
        {
            var sleep = Mccc.sleep();
            sleep.Duration = 10000;
            Assert.AreEqual(10000, sleep.GetRequestData()["duration"]);

            Assert.AreEqual(10000, Mccc.sleep(10000).GetRequestData()["duration"]);
        }
    }
}
