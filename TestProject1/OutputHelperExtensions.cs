using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace TestProject1
{
    internal static class OutputHelperExtensions
    {
        public static void WriteErrorCode(this ITestOutputHelper output, int errorCode)
        {
            if (errorCode != 0)
            {
                output.WriteLine($"!#error-code: {errorCode}");
            }
        }
    }
}
