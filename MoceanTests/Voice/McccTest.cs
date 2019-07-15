using Mocean.Voice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class McccTest
    {
        [Test]
        public void McccSayTest()
        {
            var say = Mccc.say();
            try
            {
                say.GetRequestData();
                Assert.Fail();
            }
            catch (Exception)
            {
                // required field check pass when comes here
            }

            say.Text = "testing text";
            Assert.AreEqual("testing text", say.GetRequestData()["text"]);

            Assert.AreEqual("testing text2", Mccc.say("testing text2").GetRequestData()["text"]);
        }

        [Test]
        public void McccBridgeTest()
        {
            var bridge = Mccc.bridge();
            try
            {
                bridge.GetRequestData();
                Assert.Fail();
            }
            catch (Exception)
            {
                // required field check pass when comes here
            }

            bridge.To = "testing to";
            Assert.AreEqual("testing to", bridge.GetRequestData()["to"]);

            Assert.AreEqual("testing to2", Mccc.bridge("testing to2").GetRequestData()["to"]);
        }

        [Test]
        public void McccCollectTest()
        {
            var collect = Mccc.collect();
            try
            {
                collect.GetRequestData();
                Assert.Fail();
            }
            catch (Exception)
            {
                // required field check pass when comes here
            }

            collect.EventUrl = "testing event url";
            Assert.AreEqual("testing event url", collect.GetRequestData()["event-url"]);

            Assert.AreEqual("testing event url", Mccc.collect("testing event url").GetRequestData()["event-url"]);
        }

        [Test]
        public void McccPlayTest()
        {
            var play = Mccc.play();
            try
            {
                play.GetRequestData();
                Assert.Fail();
            }
            catch (Exception)
            {
                // required field check pass when comes here
            }

            play.File = "testing file";
            Assert.AreEqual("testing file", play.GetRequestData()["file"]);

            Assert.AreEqual("testing file", Mccc.play("testing file").GetRequestData()["file"]);
        }

        [Test]
        public void McccSleepTest()
        {
            var sleep = Mccc.sleep();
            try
            {
                sleep.GetRequestData();
                Assert.Fail();
            }
            catch (Exception)
            {
                // required field check pass when comes here
            }

            sleep.Duration = 10000;
            Assert.AreEqual(10000, sleep.GetRequestData()["duration"]);

            Assert.AreEqual(10000, Mccc.sleep(10000).GetRequestData()["duration"]);
        }
    }
}
