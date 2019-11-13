using Mocean.Voice;
using NUnit.Framework;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class McTest
    {
        [Test]
        public void McSayTest()
        {
            var say = Mc.say();
            say.Text = "testing text";
            Assert.AreEqual("testing text", say.GetRequestData()["text"]);

            Assert.AreEqual("testing text2", Mc.say("testing text2").GetRequestData()["text"]);
        }

        [Test]
        public void McDialTest()
        {
            var dial = Mc.dial();
            dial.To = "testing to";
            Assert.AreEqual("testing to", dial.GetRequestData()["to"]);

            Assert.AreEqual("testing to2", Mc.dial("testing to2").GetRequestData()["to"]);
        }

        [Test]
        public void McCollectTest()
        {
            var collect = Mc.collect();
            collect.EventUrl = "testing event url";
            collect.Min = 1;
            collect.Max = 10;
            collect.Timeout = 500;
            Assert.AreEqual("testing event url", collect.GetRequestData()["event-url"]);

            collect = Mc.collect("testing event url");
            collect.Min = 1;
            collect.Max = 10;
            collect.Timeout = 500;

            Assert.AreEqual("testing event url", collect.GetRequestData()["event-url"]);
        }

        [Test]
        public void McPlayTest()
        {
            var play = Mc.play();
            play.File = "testing file";
            Assert.AreEqual("testing file", play.GetRequestData()["file"]);

            Assert.AreEqual("testing file", Mc.play("testing file").GetRequestData()["file"]);
        }

        [Test]
        public void McSleepTest()
        {
            var sleep = Mc.sleep();
            sleep.Duration = 10000;
            Assert.AreEqual(10000, sleep.GetRequestData()["duration"]);

            Assert.AreEqual(10000, Mc.sleep(10000).GetRequestData()["duration"]);
        }

        [Test]
        public void McRecordTest()
        {
            var record = Mc.record();
            Assert.AreEqual("record", record.GetRequestData()["action"]);
        }
    }
}
