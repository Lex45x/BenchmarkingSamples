using System;
using System.Buffers;

namespace BenchmarkingSamples.RamTrick
{
    public class OptimizedArraySlidingPriceWindow : IWindow, IDisposable
    {
        protected readonly decimal[] SlidingWindow;

        public OptimizedArraySlidingPriceWindow(int size)
        {
            Size = size;
            SlidingWindow = ArrayPool<decimal>.Shared.Rent(size);

            for (var i = 0; i < Size; i++)
            {
                SlidingWindow[i] = default;
            }
        }

        public int Size { get; }

        public decimal Max { get; private set; }

        public void Append(decimal value, long globalIndex)
        {
            var oldValue = SlidingWindow[globalIndex % Size];

            SlidingWindow[globalIndex % Size] = value;

            if (value > Max)
            {
                Max = value;
                return;
            }

            if (oldValue < Max) return;
            
            var localMax = decimal.MinValue;

            for (var index = 0; index < SlidingWindow.Length; index++)
            {
                var @decimal = SlidingWindow[index];
                localMax = Math.Max(localMax, @decimal);
            }

            Max = localMax;
        }

        public void Dispose()
        {
            ArrayPool<decimal>.Shared.Return(SlidingWindow);
        }
    }
}