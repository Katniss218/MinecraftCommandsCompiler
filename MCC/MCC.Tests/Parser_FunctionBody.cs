using NUnit.Framework;
using MCC;
using MCC.MCCLanguage;
using MCC.MCCLanguage.Infrastructure;
using System;

namespace MCC.Tests
{
    public class Parser_FunctionBody
    {
        Parser parser = new Parser();

        [Test]
        public void Simplest()
        {
            parser.SetFile( "test.mcc", @"{}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 0, Utils.Equals );
        }

        [Test]
        public void NoCommands()
        {
            parser.SetFile( "test.mcc", @"{
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 0, Utils.Equals );
        }

        [Test]
        public void NoCommands_WithEmptyLine()
        {
            parser.SetFile( "test.mcc", @"{

}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 0, Utils.Equals );
        }

        [Test]
        public void OneCommand()
        {
            parser.SetFile( "test.mcc", @"{
    summon zombie ~ ~ ~
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 1, Utils.Equals );
        }

        [Test]
        public void OneCommand_WithEmptyLine()
        {
            parser.SetFile( "test.mcc", @"{
    summon zombie ~ ~ ~

}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 1, Utils.Equals );
        }

        [Test]
        public void CommandWithNBT()
        {
            parser.SetFile( "test.mcc", @"{
    summon zombie ~ ~ ~ {Object:{Field:1b},List:[I;1,2,3,4]}
    summon zombie ~ ~ ~ {Object:{Field:1b},List:[I;1,2,3,4]}
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 2, Utils.Equals );
        }
        [Test]
        public void TwoCommands()
        {
            parser.SetFile( "test.mcc", @"{
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 2, Utils.Equals );
        }

        [Test]
        public void TwoCommands_WithEmptyLine()
        {
            parser.SetFile( "test.mcc", @"{
    summon zombie ~ ~ ~

    summon zombie ~ ~ ~
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 2, Utils.Equals );
        }

        [Test]
        public void TenCommands()
        {
            parser.SetFile( "test.mcc", @"{
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
    summon zombie ~ ~ ~
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 10, Utils.Equals);
        }

        [Test]
        public void InlineExecute()
        {
            parser.SetFile( "test.mcc", @"{
    execute as @a at @s run
    {
        summon zombie ~ ~ ~
    }
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 1, Utils.Equals );
        }

        [Test]
        public void TwoInlineExecutes()
        {
            parser.SetFile( "test.mcc", @"{
    execute as @a at @s run
    {
        summon zombie ~ ~ ~
    }
    execute as @a at @s run
    {
        summon zombie ~ ~ ~
    }
}" );
            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 2, Utils.Equals );
        }

        [Test]
        public void NestedInlineExecute()
        {
            parser.SetFile( "test.mcc", @"{
    execute as @a at @s run
    {
        summon zombie ~ ~ ~

        execute as @a at @s run
        {
            summon zombie ~ ~ ~
        }

        summon zombie ~ ~ ~
    }
}" );

            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 1, Utils.Equals );
        }

        [Test]
        public void InlineExecute_TrailingSpace()
        {
            // trailing space after 'run'
            parser.SetFile( "test.mcc", @"{
    execute as @a at @s run 
    {
        summon zombie ~ ~ ~
    }
}" );

            MCCFunction.FunctionBody parsed = null;

            Assert.DoesNotThrow( () => parsed = parser.EatFunctionBody() );
            Utils.AssertTrueAndLog( "Commands.Count", parsed.Commands.Count, 1, Utils.Equals );
        }
    }
}
