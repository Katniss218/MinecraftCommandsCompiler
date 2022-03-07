using NUnit.Framework;
using MCC;
using MCC.MCCLanguage;
using MCC.MCCLanguage.Infrastructure;

namespace MCC.Tests
{
    public class ParserTests_Function
    {
        Parser parser = new Parser();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ValidateFunction()
        {
            parser.SetFile("test.mcc", @"function spawn_enemy
{
    summon zombie ~ ~ ~
}");
            MCCFunction parsedFunc;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
        }

        [Test]
        public void ValidateFunction_WithLeadingAndTrailingWhitespaces()
        {
            // this is fucked. doesn't throw when I run it in console.
            parser.SetFile("test.mcc", @"
function spawn_enemy
{
    summon zombie ~ ~ ~
}
");
            MCCFunction parsedFunc;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
        }

        [Test]
        public void ValidateFunction_OneLiner()
        {
            parser.SetFile("test.mcc", @"function spawn_enemy { summon zombie ~ ~ ~ }");
            MCCFunction parsedFunc;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
        }
    }
}