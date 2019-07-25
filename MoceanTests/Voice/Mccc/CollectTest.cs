using Mocean.Exceptions;
using Mocean.Voice;
using NUnit.Framework;
using System.Collections.Generic;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class CollectTest
    {
        [Test]
        public void RequestParamsTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "event-url", "testing event url" },
                { "min", 1 },
                { "max", 10 },
                { "terminators", "#" },
                { "timeout", 10000 },
                { "action", "collect" }
            };
            var collect = new Collect(parameter);

            Assert.AreEqual(parameter, collect.GetRequestData());

            collect = new Collect();
            collect.EventUrl = "testing event url";
            collect.Min = 1;
            collect.Max = 10;
            collect.Terminators = "#";
            collect.Timeout = 10000;

            Assert.AreEqual(parameter, collect.GetRequestData());
        }

        [Test]
        public void IfActionAutoDefinedTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "event-url", "testing event url" },
                { "min", 1 },
                { "max", 10 },
                { "terminators", "#" },
                { "timeout", 10000 }
            };
            var collect = new Collect(parameter);

            Assert.AreEqual("collect", collect.GetRequestData()["action"]);
        }

        [Test]
        public void IfRequiredFieldNotSetTest()
        {
            Assert.Throws<RequiredFieldException>(() =>
            {
                var collect = new Collect();
                collect.GetRequestData();
            });
        }
    }
}
