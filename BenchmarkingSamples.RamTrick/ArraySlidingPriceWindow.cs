using System;
using System.Buffers;
using System.Linq;

namespace BenchmarkingSamples.RamTrick
{
    public class ArraySlidingPriceWindow : IWindow, IDisposable
    {
        protected readonly decimal[] SlidingWindow;

        public ArraySlidingPriceWindow(int size)
        {
            Size = size;
            SlidingWindow = ArrayPool<decimal>.Shared.Rent(size);

            for (var i = 0; i < Size; i++)
            {
                SlidingWindow[i] = default;
            }
        }

        public int Size { get; }

        public decimal Max
        {
            get
            {
                var max = decimal.MinValue;

                for (var index = 0; index < SlidingWindow.Length; index++)
                {
                    var @decimal = SlidingWindow[index];
                    max = Math.Max(max, @decimal);
                }

                return max;
            }
        }

        public void Append(decimal value, long globalIndex)
        {
            SlidingWindow[globalIndex % Size] = value;
        }

        public void Dispose()
        {
            ArrayPool<decimal>.Shared.Return(SlidingWindow);
        }
    }
}