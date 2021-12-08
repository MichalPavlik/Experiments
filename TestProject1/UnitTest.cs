using Microsoft.DotNet.Cli.Utils;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestProject1
{
    public class UnitTest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public UnitTest(ITestOutputHelper output)
        {
            testOutputHelper = output;
        }

        [Fact]
        public void TestWithErrorCodeLogging()
        {
            var result = CreateInstallCommand()
                .CaptureStdOut()
                .Execute();

            testOutputHelper.WriteLine(result.StdOut);

            Assert.True(false);
        }

        private static Command CreateInstallCommand()
        {
            string path = "powershell.exe";
            string finalArgs = "-ExecutionPolicy Bypass -NoProfile -NoLogo -Command \".\\script.ps1 -DebugMessages\"";

            return Command.Create(new CommandSpec(path, finalArgs, CommandResolutionStrategy.None));
        }
    }
}
