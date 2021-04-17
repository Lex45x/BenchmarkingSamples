using System;
using BenchmarkDotNet.Running;

namespace BenchmarkingSamples.DictionaryOfGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
