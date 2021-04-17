using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkingSamples.DictionaryOfGenerics.Collections;
using BenchmarkingSamples.DictionaryOfGenerics.Models;
using Moq;

namespace BenchmarkingSamples.DictionaryOfGenerics
{
    [SimpleJob]
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        public static int CarsCount = 10_000;

        [GlobalSetup]
        public void Setup()
        {
            for (var i = 0; i < CarsCount; i++)
            {
                CarsCollectionList.Add(Mock.Of<IElectricCar>());
                CarsCollectionList.Add(Mock.Of<IGasolineCar>());
                CarsCollectionList.Add(Mock.Of<IDieselCar>());

                CarsCollectionDictionary.Add(Mock.Of<IElectricCar>());
                CarsCollectionDictionary.Add(Mock.Of<IGasolineCar>());
                CarsCollectionDictionary.Add(Mock.Of<IDieselCar>());

                CarsCollectionGeneric<IElectricCar>.Add(Mock.Of<IElectricCar>());
                CarsCollectionGeneric<IGasolineCar>.Add(Mock.Of<IGasolineCar>());
                CarsCollectionGeneric<IDieselCar>.Add(Mock.Of<IDieselCar>());
            }
        }

        [Benchmark(Baseline = true)]
        public IReadOnlyList<IElectricCar> GetCarsFromList()
        {
            return CarsCollectionList.GetCars<IElectricCar>();
        }

        [Benchmark]
        public IReadOnlyList<IElectricCar> GetCarsFromDictionary()
        {
            return CarsCollectionDictionary.GetCars<IElectricCar>();
        }

        [Benchmark]
        public IReadOnlyList<IElectricCar> GetCarsGeneric()
        {
            return CarsCollectionGeneric<IElectricCar>.GetCars();
        }
    }
}