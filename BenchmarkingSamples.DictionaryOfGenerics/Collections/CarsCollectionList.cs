using System.Collections.Generic;
using System.Linq;
using BenchmarkingSamples.DictionaryOfGenerics.Models;

namespace BenchmarkingSamples.DictionaryOfGenerics.Collections
{
    public static class CarsCollectionList
    {
        private static readonly List<ICar> CarsList = new();

        public static void Add<TCar>(TCar instance)
            where TCar : ICar
        {
            CarsList.Add(instance);
        }

        public static IReadOnlyList<TCar> GetCars<TCar>()
            where TCar : ICar
        {
            return CarsList.OfType<TCar>().ToList();
        }
    }
}