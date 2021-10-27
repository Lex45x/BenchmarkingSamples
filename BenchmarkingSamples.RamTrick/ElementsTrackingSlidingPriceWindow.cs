using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace BenchmarkingSamples.RamTrick
{
    public class ElementsTrackingSlidingPriceWindow : IDisposable, IWindow
    {
        public int Size { get; }
        protected readonly decimal[] UnderlyingWindow;

        public ElementsTrackingSlidingPriceWindow(int size)
        {
            Size = size;
            UnderlyingWindow = ArrayPool<decimal>.Shared.Rent(size);

            for (var i = 0; i < Size; i++)
            {
                UnderlyingWindow[i] = default;
            }

            elementsCounter = new Dictionary<decimal, int>(size)
            {
                [0] = size
            };
        }

        public void Append(decimal value, long globalIndex)
        {
            var oldValue = UnderlyingWindow[globalIndex % Size];

            UnderlyingWindow[globalIndex % Size] = value;

            // if 'value' already present - increment
            if (!elementsCounter.TryAdd(value, 1))
            {
                elementsCounter[value] = elementsCounter[value] + 1;
            }

            var oldCount = elementsCounter[oldValue] - 1;
            elementsCounter[oldValue] = oldCount;

            // if all old values exited window - remove old value from Dictionary
            if (oldCount == 0)
            {
                elementsCounter.Remove(oldValue);
            }

            // if new value bigger then Max - no need for additional checks
            if (Max < value)
            {
                Max = value;
                return;
            }

            // if old value was Max - search for Max value again
            if (oldCount == 0 && oldValue == Max && Max > value)
            {
                Max = elementsCounter.Keys.Max();
            }
        }

        public void Dispose()
        {
            ArrayPool<decimal>.Shared.Return(UnderlyingWindow);
        }

        public decimal Max { get; private set; }

        private readonly Dictionary<decimal, int> elementsCounter;
    }
}