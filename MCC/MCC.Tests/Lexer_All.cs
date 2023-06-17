using MCC.MCCLanguage.Parsing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC.Tests
{
    class Lexer_All
    {
        Lexer lexer = new Lexer();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Simplest()
        {
            lexer.SetFile( "test.mcc", @"function spawn_enemy
{
    summon zombie ~ ~ ~
}" );
            List<SyntaxToken> tokens = null;

            Assert.DoesNotThrow( () => tokens = lexer.Lex() );
            Utils.AssertTrueAndLog( "Tokens", tokens, new List<SyntaxToken>()
            {
                new SyntaxToken(SyntaxTokenType.Text, "function", new LineInfo("test.mcc", 1, 0 )),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", new LineInfo("test.mcc", 1, 8 )),
                new SyntaxToken(SyntaxTokenType.Text, "spawn_enemy", new LineInfo("test.mcc", 1, 9 )),
                new SyntaxToken(SyntaxTokenType.NewLine, "\n", new LineInfo("test.mcc", 1, 12 )),
                new SyntaxToken(SyntaxTokenType.OpenCurlyBracket, "{", new LineInfo("test.mcc", 2, 0 )),
                new SyntaxToken(SyntaxTokenType.NewLine, "\n", new LineInfo("test.mcc", 2, 1 )),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, "    ", new LineInfo("test.mcc", 3, 0 )),
                new SyntaxToken(SyntaxTokenType.Text, "summon", new LineInfo("test.mcc", 3, 4 )),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", new LineInfo("test.mcc", 3, 10 )),
                new SyntaxToken(SyntaxTokenType.Text, "zombie", new LineInfo("test.mcc", 3, 11 )),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", new LineInfo("test.mcc", 3, 17 )),
                new SyntaxToken(SyntaxTokenType.Text, "~", new LineInfo("test.mcc", 3, 18 )),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", new LineInfo("test.mcc", 3, 19 )),
                new SyntaxToken(SyntaxTokenType.Text, "~", new LineInfo("test.mcc", 3, 20 )),
                new SyntaxToken(SyntaxTokenType.WhiteSpace, " ", new LineInfo("test.mcc", 3, 21 )),
                new SyntaxToken(SyntaxTokenType.Text, "~", new LineInfo("test.mcc", 3, 22 )),
                new SyntaxToken(SyntaxTokenType.NewLine, "\n", new LineInfo("test.mcc", 3, 23 )),
                new SyntaxToken(SyntaxTokenType.CloseCurlyBracket, "}", new LineInfo("test.mcc", 4, 0 )),
            }, Utils.SequenceEquals );

        }
    }
}
