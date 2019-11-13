using Mocean.Exceptions;
using Mocean.Voice.McObj;
using NUnit.Framework;
using System.Collections.Generic;

namespace MoceanTests.Voice.McObj
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
                { "clear-digit-cache", true },
                { "action", "play" }
            };
            var play = new Play(parameter);

            Assert.AreEqual(parameter, play.GetRequestData());

            play = new Play();
            play.File = "testing file";
            play.BargeIn = true;
            play.ClearDigitCache = true;

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
