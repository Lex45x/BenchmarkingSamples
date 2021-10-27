using System.Collections.Generic;
using System.Linq;

namespace BenchmarkingSamples.RamTrick
{
    public class QueueSlidingPriceWindow : IWindow
    {
        protected readonly Queue<decimal> SlidingWindow;

        public QueueSlidingPriceWindow(int size)
        {
            Size = size;
            SlidingWindow = new Queue<decimal>(size);
            for (var i = 0; i < Size; i++)
            {
                SlidingWindow.Enqueue(default);
            }
        }

        public int Size { get; }

        public decimal Max => SlidingWindow.Max();

        public void Append(decimal value, long globalIndex)
        {
            SlidingWindow.Enqueue(value);
            SlidingWindow.Dequeue();
        }
    }
}