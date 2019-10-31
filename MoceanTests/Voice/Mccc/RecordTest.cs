﻿using Mocean.Exceptions;
using Mocean.Voice;
using NUnit.Framework;
using System.Collections.Generic;

namespace MoceanTests.Voice
{
    [TestFixture]
    public class RecordTest
    {
        [Test]
        public void IfActionAutoDefinedTest()
        {
            var record = new Record();

            Assert.AreEqual("record", record.GetRequestData()["action"]);
        }
    }
}
