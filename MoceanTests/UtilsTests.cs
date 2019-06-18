using Mocean.Account;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mocean.Tests
{
    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void ConvertClassToDictionaryTest()
        {
            var result = Utils.ConvertClassToDictionary(new BalanceRequest
            {
                mocean_resp_format = "json"
            });

            Assert.IsInstanceOf(typeof(Dictionary<string, string>), result);
            Assert.AreEqual(result["mocean-resp-format"], "json");
            Assert.IsFalse(result.ContainsKey("simple_rubish_key"));
            Assert.Throws<KeyNotFoundException>(() => _ = result["another_simple_rubish_key"]);
        }
    }
}