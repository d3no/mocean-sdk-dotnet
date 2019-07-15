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
    public class SayTest
    {
        [Test]
        public void RequestParamsTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "language", "testing language" },
                { "text", "testing text" },
                { "barge-in", true },
                { "action", "say" }
            };
            var say = new Say(parameter);

            Assert.AreEqual(parameter, say.GetRequestData());

            say = new Say();
            say.Language = "testing language";
            say.Text = "testing text";
            say.BargeIn = true;

            Assert.AreEqual(parameter, say.GetRequestData());
        }

        [Test]
        public void IfActionAutoDefinedTest()
        {
            var parameter = new Dictionary<string, object>
            {
                { "language", "testing language" },
                { "text", "testing text" },
                { "barge-in", true }
            };
            var say = new Say(parameter);

            Assert.AreEqual("say", say.GetRequestData()["action"]);
        }

        [Test]
        public void IfRequiredFieldNotSetTest()
        {
            Assert.Throws<RequiredFieldException>(() =>
            {
                var say = new Say();
                say.GetRequestData();
            });
        }
    }
}
