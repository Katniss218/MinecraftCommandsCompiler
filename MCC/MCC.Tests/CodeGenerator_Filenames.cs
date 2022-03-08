using NUnit.Framework;
using MCC;
using MCC.MCCLanguage;
using MCC.MCCLanguage.Infrastructure;
using MCC.OutputLanguage;
using System;

namespace MCC.Tests
{
    public class CodeGenerator_Filenames
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void MCFunctionIdentifier()
        {
            string inputNamespace = "katniss";
            string inputFunctionName = "test_function";

            string output = CodeGenerator.GetMinecraftNamespacedId( inputNamespace, inputFunctionName );

            Utils.AssertTrueAndLog( "Output", output, "katniss:test_function", Utils.Equals );
        }

        [Test]
        public void MCFunctionIdentifier_WithSubdirectory()
        {
            string inputNamespace = "katniss.utilities";
            string inputFunctionName = "test_function";

            string output = CodeGenerator.GetMinecraftNamespacedId( inputNamespace, inputFunctionName );

            Utils.AssertTrueAndLog( "Output", output, "katniss:utilities/test_function", Utils.Equals );
        }

        [Test]
        public void MCPath()
        {
            string inputNamespace = "katniss";
            string inputFunctionName = "test_function";

            string output = CodeGenerator.GetMinecraftPath( inputNamespace, inputFunctionName );

            Utils.AssertTrueAndLog( "Output", output, "katniss\\functions\\test_function.mcfunction", Utils.Equals );
        }

        [Test]
        public void MCPath_WithSubdirectory()
        {
            string inputNamespace = "katniss.utilities";
            string inputFunctionName = "test_function";

            string output = CodeGenerator.GetMinecraftPath( inputNamespace, inputFunctionName );

            Utils.AssertTrueAndLog( "Output", output, "katniss\\functions\\utilities\\test_function.mcfunction", Utils.Equals );
        }
    }
}