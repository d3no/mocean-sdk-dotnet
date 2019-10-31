using Mocean.Exceptions;
using Mocean.Voice;
using NUnit.Framework;
using System.Collections.Generic;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class DialTest
    {
        [Test]
        public void RequestParamsTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "to", "testing to" },
                { "from", "callerid" },
                { "dial-sequentially", true },
                { "action", "dial" }
            };
            var dial = new Dial(parameter);

            Assert.AreEqual(parameter, dial.GetRequestData());

            dial = new Dial();
            dial.To = "testing to";
            dial.From = "callerid";
            dial.DialSequentially = true;

            Assert.AreEqual(parameter, dial.GetRequestData());
        }

        [Test]
        public void IfActionAutoDefinedTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "to", "testing to" }
            };
            var dial = new Dial(parameter);

            Assert.AreEqual("dial", dial.GetRequestData()["action"]);
        }

        [Test]
        public void IfRequiredFieldNotSetTest()
        {
            Assert.Throws<RequiredFieldException>(() =>
            {
                var dial = new Dial();
                dial.GetRequestData();
            });
        }
    }
}
