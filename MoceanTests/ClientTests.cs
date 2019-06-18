using Mocean.Auth;
using Mocean.Exceptions;
using MoceanTests;
using NUnit.Framework;
using System;

namespace Mocean.Tests
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public void CreateClientWithEmptyApiKeyOrApiSecretTest()
        {
            Assert.Throws<RequiredFieldException>(() =>
            {
                new Client(new Basic("test api key", ""));
            });

            Assert.Throws<RequiredFieldException>(() =>
            {
                new Client(new Basic("", "test api secret"));
            });

            Assert.Throws<RequiredFieldException>(() =>
            {
                new Client(new Basic());
            });
        }

        [Test]
        public void CreateClientTest()
        {
            try
            {
                new Client(new Basic("test api key", "test api secret"));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void CreateClientWithUnsupportedCredentialTest()
        {
            try
            {
                new Client(new DummyCredentials());
                Assert.Fail("created client with unsupported credential");
            }
            catch (MoceanErrorException)
            {
            }
        }
    }
}