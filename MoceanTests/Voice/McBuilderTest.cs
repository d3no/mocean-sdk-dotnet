using Mocean.Voice;
using Mocean.Voice.McObj;
using NUnit.Framework;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class McBuilderTest
    {
        [Test]
        public void AddTest()
        {
            var play = new Play
            {
                File = "testing file"
            };

            var builder = new McBuilder();
            builder.add(play);
            Assert.AreEqual(1, builder.build().Count);
            Assert.AreEqual(play.GetRequestData(), builder.build()[0]);

            play.File = "testing file2";
            builder.add(play);
            Assert.AreEqual(2, builder.build().Count);
            Assert.AreEqual(play.GetRequestData(), builder.build()[1]);
        }
    }
}
