using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkingSamples.DictionaryOfGenerics.Models;

namespace BenchmarkingSamples.DictionaryOfGenerics.Collections
{
    public static class CarsCollectionDictionary
    {
        private static readonly Dictionary<Type, IList<ICar>> CarsDictionary = new();

        public static void Add<TCar>(TCar instance)
            where TCar : ICar
        {
            if (CarsDictionary.TryGetValue(typeof(TCar), out var cars))
            {
                cars.Add(instance);
            }
            else
            {
                CarsDictionary.Add(typeof(TCar), new List<ICar>
                {
                    instance
                });
            }
        }

        public static IReadOnlyList<TCar> GetCars<TCar>()
            where TCar : ICar
        {
            return CarsDictionary.TryGetValue(typeof(TCar), out var cars)
                ? cars.Cast<TCar>().ToList()
                : new List<TCar>();
        }
    }
}