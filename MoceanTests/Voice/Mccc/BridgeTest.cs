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
    public class BridgeTest
    {
        [Test]
        public void RequestParamsTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "to", "testing to" },
                { "action", "dial" }
            };
            var bridge = new Bridge(parameter);

            Assert.AreEqual(parameter, bridge.GetRequestData());

            bridge = new Bridge();
            bridge.To = "testing to";

            Assert.AreEqual(parameter, bridge.GetRequestData());
        }

        [Test]
        public void IfActionAutoDefinedTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "to", "testing to" }
            };
            var bridge = new Bridge(parameter);

            Assert.AreEqual("dial", bridge.GetRequestData()["action"]);
        }

        [Test]
        public void IfRequiredFieldNotSetTest()
        {
            Assert.Throws<RequiredFieldException>(() =>
            {
                var bridge = new Bridge();
                bridge.GetRequestData();
            });
        }
    }
}
