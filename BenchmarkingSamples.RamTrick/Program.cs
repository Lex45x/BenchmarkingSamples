using System;
using System.Globalization;
using System.IO;
using BenchmarkDotNet.Running;

namespace BenchmarkingSamples.RamTrick
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SlidingWindowBenchmarks>();
        }
    }
}