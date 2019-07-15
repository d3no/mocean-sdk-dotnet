using Mocean.Exceptions;
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
    public class PlayTest
    {
        [Test]
        public void RequestParamsTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "file", "testing file" },
                { "barge-in", true },
                { "action", "play" }
            };
            var play = new Play(parameter);

            Assert.AreEqual(parameter, play.GetRequestData());

            play = new Play();
            play.File = "testing file";
            play.BargeIn = true;

            Assert.AreEqual(parameter, play.GetRequestData());
        }

        [Test]
        public void IfActionAutoDefinedTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "file", "testing file" },
                { "barge-in", true }
            };
            var play = new Play(parameter);

            Assert.AreEqual("play", play.GetRequestData()["action"]);
        }

        [Test]
        public void IfRequiredFieldNotSetTest()
        {
            Assert.Throws<RequiredFieldException>(() =>
            {
                var play = new Play();
                play.GetRequestData();
            });
        }
    }
}
