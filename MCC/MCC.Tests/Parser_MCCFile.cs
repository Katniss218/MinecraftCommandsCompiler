
using NUnit.Framework;
using MCC;
using MCC.MCCLanguage;
using MCC.MCCLanguage.Infrastructure;
using System;

namespace MCC.Tests
{
    public class Parser_MCCFile
    {
        Parser parser = new Parser();

        [Test]
        public void File()
        {
            parser.SetFile( "test.mcc", @"
namespace katniss.tests

function spawn_enemy
{
    summon zombie ~ ~ ~
}

function other_func
{
    summon zombie ~ ~ ~
    function spawn_enemy
}
" );
           // MCCFunction.FunctionBody parsed = null;

        }
    }
}
