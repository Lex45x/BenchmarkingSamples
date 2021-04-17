using System.Collections.Generic;
using BenchmarkingSamples.DictionaryOfGenerics.Models;

namespace BenchmarkingSamples.DictionaryOfGenerics.Collections
{
    public static class CarsCollectionGeneric<TCar>
        where TCar : ICar
    {
        private static readonly List<TCar> CarsList = new();

        public static void Add(TCar instance)
        {
            CarsList.Add(instance);
        }

        public static IReadOnlyList<TCar> GetCars()
        {
            return CarsList;
        }
    }
}