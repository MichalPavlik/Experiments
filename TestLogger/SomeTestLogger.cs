using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TestLogger
{
    [FriendlyName("install-scripts-monitoring")]
    [ExtensionUri("logger://microsoft/install-scripts-monitoring/logger/v1")]
    public class SomeTestLogger : ITestLoggerWithParameters
    {
        private string reportFilePath;
        private StringWriter outputWriter;

        public void Initialize(TestLoggerEvents events, string testRunDirectory)
        {
            InitializeCore(events, Path.Combine(testRunDirectory, "report.txt"));
        }

        public void Initialize(TestLoggerEvents events, Dictionary<string, string> parameters)
        {
            InitializeCore(
                events, 
                parameters.TryGetValue("ReportPath", out string reportfilePath) 
                    ? reportfilePath 
                    : Path.Combine(parameters["TestRunDirectory"], "report.txt"));
        }

        private void InitializeCore(TestLoggerEvents events, string reportFilePath)
        {
            // Debugger.Launch();

            this.reportFilePath = reportFilePath;
            outputWriter = new StringWriter();
            events.TestResult += Events_TestResult;
            events.TestRunComplete += Events_TestRunComplete;
        }

        private void Events_TestRunComplete(object sender, TestRunCompleteEventArgs e)
        {
            File.WriteAllText(reportFilePath, outputWriter.ToString());
            outputWriter.Dispose();
        }

        private void Events_TestResult(object sender, TestResultEventArgs e)
        {
            // Writes output just from failed test
            if (e.Result.Outcome == TestOutcome.Failed)
            {
                outputWriter.WriteLine($"{e.Result.DisplayName}:");
                outputWriter.WriteLine(e.Result.Messages[0].Text);
            }
        }
    }
}
