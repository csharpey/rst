using System;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace Rst.Stl.Benchmark
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var config = new ManualConfig()
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                .AddValidator(JitOptimizationsValidator.DontFailOnError)
                .AddLogger(ConsoleLogger.Default)
                .AddColumnProvider(DefaultColumnProviders.Instance);

            BenchmarkRunner.Run<MaxSubSumGenericVsIntSpec>(config);
            BenchmarkRunner.Run<MaxSubSumGenericVsDoubleSpec>(config);
        }
    }
}