using NUnit.Framework;
using MCC;
using MCC.MCCLanguage;
using MCC.MCCLanguage.Infrastructure;
using System;

namespace MCC.Tests
{
    public class Parser_Identifier
    {
        Parser parser = new Parser();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Valid()
        {
            parser.SetFile("test.mcc", @"qwertyuiopasdfghjklzxcvbnm_.1234567890 " );
            string parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatIdentifier() );
            Utils.AssertTrueAndLog( "Output", parsed, "qwertyuiopasdfghjklzxcvbnm_.1234567890", Utils.Equals );
        }

        [Test]
        public void Invalid()
        {
            parser.SetFile("test.mcc", @"ABCDE-+ " );
            string parsed = null;

            Assert.Throws(typeof(Exception), () => parsed = parser.EatIdentifier() );
        }
    }
}