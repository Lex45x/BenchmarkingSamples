using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace BenchmarkingSamples.RamTrick
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class SlidingWindowBenchmarks
    {
        [Params(5_000, 10_000, 50_000)] 
        public int WindowSize { get; set; }

        [Params(1_000_000, 5_000_000, 10_000_000)]
        public int DataSetSize { get; set; }

        public IReadOnlyList<decimal> Data { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            Data = ValuesGenerator
                .GetSimulatedPrices(60_000, 0.01m)
                .Take(DataSetSize)
                .ToList();
        }
        
        public IReadOnlyList<decimal> Queue()
        {
            var priceWindow = new QueueSlidingPriceWindow(WindowSize);

            var result = new List<decimal>(DataSetSize);

            for (var index = 0; index < Data.Count; index++)
            {
                priceWindow.Append(Data[index], index);
                result.Add(priceWindow.Max);
            }

            return result;
        }
        
        public IReadOnlyList<decimal> Array()
        {
            var priceWindow = new ArraySlidingPriceWindow(WindowSize);

            var result = new List<decimal>(DataSetSize);

            for (var index = 0; index < Data.Count; index++)
            {
                priceWindow.Append(Data[index], index);
                result.Add(priceWindow.Max);
            }

            return result;
        }

        [Benchmark(Baseline = true)]
        public IReadOnlyList<decimal> OptimizedArray()
        {
            using var priceWindow = new OptimizedArraySlidingPriceWindow(WindowSize);

            var result = new List<decimal>(DataSetSize);

            for (var index = 0; index < Data.Count; index++)
            {
                priceWindow.Append(Data[index], index);
                result.Add(priceWindow.Max);
            }

            return result;
        }

        [Benchmark]
        public IReadOnlyList<decimal> ElementTracking()
        {
            using var priceWindow = new ElementsTrackingSlidingPriceWindow(WindowSize);

            var result = new List<decimal>(DataSetSize);

            for (var index = 0; index < Data.Count; index++)
            {
                priceWindow.Append(Data[index], index);
                result.Add(priceWindow.Max);
            }

            return result;
        }
    }
}