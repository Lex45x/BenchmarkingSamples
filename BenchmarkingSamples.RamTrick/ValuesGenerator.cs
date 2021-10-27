using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BenchmarkingSamples.RamTrick
{
    public static class ValuesGenerator
    {
        public static IEnumerable<decimal> GetSimulatedPrices(decimal initialPrice, decimal minimalStep)
        {
            var random = new Random();

            var currentPrice = initialPrice;
            yield return initialPrice;

            while (true)
            {
                // will determine direction of the price change
                var direction = (int) Math.Pow(-1, random.Next());

                var multiplier = random.Next(0, 5);

                currentPrice += multiplier * direction * minimalStep;

                yield return currentPrice;
            }
        }
    }
}