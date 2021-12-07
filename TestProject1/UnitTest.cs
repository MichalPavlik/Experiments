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
            var result = CreateInstallCommand().Execute();

            testOutputHelper.WriteErrorCode(result.ExitCode);
            testOutputHelper.WriteLine("A text for different logger");

            testOutputHelper.WriteErrorCode(256);
        }

        private static Command CreateInstallCommand()
        {
            string path = "powershell.exe";
            string finalArgs = "-ExecutionPolicy Bypass -NoProfile -NoLogo -Command \"try {. .\\script.ps1} catch {if ($errorCode -eq $null) { $errorCode = 1 }} finally { exit $errorCode }\"";

            return Command.Create(new CommandSpec(path, finalArgs, CommandResolutionStrategy.None));
        }
    }
}
