using Mocean.Exceptions;
using Mocean.Voice;
using NUnit.Framework;
using System.Collections.Generic;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class SleepTest
    {
        [Test]
        public void RequestParamsTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "duration", 10000 },
                { "action", "sleep" }
            };
            var sleep = new Sleep(parameter);

            Assert.AreEqual(parameter, sleep.GetRequestData());

            sleep = new Sleep();
            sleep.Duration = 10000;

            Assert.AreEqual(parameter, sleep.GetRequestData());
        }

        [Test]
        public void IfActionAutoDefinedTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "duration", 10000 },
                { "barge-in", true }
            };
            var sleep = new Sleep(parameter);

            Assert.AreEqual("sleep", sleep.GetRequestData()["action"]);
        }

        [Test]
        public void IfRequiredFieldNotSetTest()
        {
            Assert.Throws<RequiredFieldException>(() =>
            {
                var sleep = new Sleep();
                sleep.GetRequestData();
            });
        }
    }
}
