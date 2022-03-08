using NUnit.Framework;
using MCC;
using MCC.MCCLanguage;
using MCC.MCCLanguage.Infrastructure;
using System;

namespace MCC.Tests
{
    public class Parser_Function
    {
        Parser parser = new Parser();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Simplest()
        {
            parser.SetFile("test.mcc", @"function spawn_enemy
{
    summon zombie ~ ~ ~
}" );
            MCCFunction parsedFunc = null;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
            Utils.AssertTrueAndLog( "Tick", parsedFunc.Tick, false, Utils.Equals );
            Utils.AssertTrueAndLog( "Load", parsedFunc.Load, false, Utils.Equals );
            Utils.AssertTrueAndLog( "Identifier", parsedFunc.Identifier, "spawn_enemy", Utils.Equals );
        }

        [Test]
        public void WithAttribute()
        {
            // this is fucked. doesn't throw when I run it in console.
            parser.SetFile("test.mcc", @"[tick]
function spawn_enemy
{
    summon zombie ~ ~ ~
}" );
            MCCFunction parsedFunc = null;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
            Utils.AssertTrueAndLog( "Tick", parsedFunc.Tick, true, Utils.Equals );
            Utils.AssertTrueAndLog( "Load", parsedFunc.Load, false, Utils.Equals );
            Utils.AssertTrueAndLog( "Identifier", parsedFunc.Identifier, "spawn_enemy", Utils.Equals );
        }

        [Test]
        public void WithAttributes()
        {
            // this is fucked. doesn't throw when I run it in console.
            parser.SetFile( "test.mcc", @"[tick]
[load]
function spawn_enemy
{
    summon zombie ~ ~ ~
}" );
            MCCFunction parsedFunc = null;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
            Utils.AssertTrueAndLog( "Tick", parsedFunc.Tick, true, Utils.Equals );
            Utils.AssertTrueAndLog( "Load", parsedFunc.Load, true, Utils.Equals );
            Utils.AssertTrueAndLog( "Identifier", parsedFunc.Identifier, "spawn_enemy", Utils.Equals );
        }

        [Test]
        public void WithAttributes_InSameLine()
        {
            // this is fucked. doesn't throw when I run it in console.
            parser.SetFile( "test.mcc", @"[tick][load]
function spawn_enemy
{
    summon zombie ~ ~ ~
}" );
            MCCFunction parsedFunc = null;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
            Utils.AssertTrueAndLog( "Tick", parsedFunc.Tick, true, Utils.Equals );
            Utils.AssertTrueAndLog( "Load", parsedFunc.Load, true, Utils.Equals );
            Utils.AssertTrueAndLog( "Identifier", parsedFunc.Identifier, "spawn_enemy", Utils.Equals );
        }

        [Test]
        public void OneLiner()
        {
            parser.SetFile("test.mcc", @"function spawn_enemy { summon zombie ~ ~ ~ }");
            MCCFunction parsedFunc = null;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
            Utils.AssertTrueAndLog( "Tick", parsedFunc.Tick, false, Utils.Equals );
            Utils.AssertTrueAndLog( "Load", parsedFunc.Load, false, Utils.Equals );
            Utils.AssertTrueAndLog( "Identifier", parsedFunc.Identifier, "spawn_enemy", Utils.Equals );
        }

        [Test]
        public void NoBody()
        {
            parser.SetFile( "test.mcc", @"function spawn_enemy
{
}" );
            MCCFunction parsedFunc = null;

            Assert.DoesNotThrow( () => parsedFunc = parser.EatFunction() );
            Utils.AssertTrueAndLog( "Tick", parsedFunc.Tick, false, Utils.Equals );
            Utils.AssertTrueAndLog( "Load", parsedFunc.Load, false, Utils.Equals );
            Utils.AssertTrueAndLog( "Identifier", parsedFunc.Identifier, "spawn_enemy", Utils.Equals );
        }
    }
}