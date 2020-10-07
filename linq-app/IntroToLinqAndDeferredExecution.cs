using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using linq_app.entities;

namespace linq_app
{
    class IntroToLinqAndDeferredExecution
    {
        public static void Run()
        {
            var cars = Generator.ListOfCars(1000);

            static void PrintCar(Car car)
            {
                Console.WriteLine($"\t{car.Manufacturer}, {car.Model}, {car.Year}");
            }
            // foreach (var car in cars)
            // {
            //     if (car.Manufacturer == "Volvo")
            //     {
            //         PrintCar(car);
            //     }
            // }

            // foreach (var car in cars.FilterVolvos())
            // {
            //     PrintCar(car);
            // }
            
            // foreach (var car in cars.Filter(car => car.Manufacturer == "BMW"))
            // {
            //     PrintCar(car);
            // }
            
            // cars
            //     .Filter(car => car.Manufacturer == "Audi")
            //     .DoSomethingForEachCar(PrintCar);
            
            // cars
            //     .Filter(car => car.Manufacturer == "Audi")
            //     .Grab(3)
            //     .DoSomethingForEachCar(PrintCar);
            
            // cars
            //     .Grab(30)
            //     .Filter(car => car.Manufacturer == "Audi")
            //     .DoSomethingForEachCar(PrintCar);
            
            
            // // here comes the best part ... 
            // cars
            //     .Where(car => car.Manufacturer == "Audi")
            //     .Take(3)
            //     .ToList()
            //     .ForEach(PrintCar);
            
            // // even better
            // using var carCollection = new CarCsvReader();
            // carCollection.GetCars()
            //     .Where(car => car.Manufacturer == "Audi")
            //     .Take(3)
            //     .ToList()
            //     .ForEach(PrintCar);
            
            // // try again with our own operators for extra logging
            // using var carCollection = new CarCsvReader();
            // carCollection.GetCars()
            //     .Filter(car => car.Manufacturer == "Audi")
            //     .Grab(3)
            //     .ToList()
            //     .ForEach(PrintCar);
            
            // // watch out with enumerators!
            // using var carCollection = new CarCsvReader();
            // var myCars = carCollection.GetCars();
            // var part = myCars.Count() / 5000;
            // myCars.Filter(car => car.Manufacturer == "Audi")
            //     .Grab(part)
            //     .ToList()
            //     .ForEach(PrintCar);
            
            // // at least only execute it once ...
            // using var carCollection = new CarCsvReader();
            // var myCars = carCollection.GetCars()
            //     // .ToArray()
            //     // .ToDictionary()
            //     // .ToLookup()
            //     // .ToHashSet()
            //     // .ToImmutableList()
            //     .ToList(); // <- force complete execution en keep results (no more double file reads)
            // var part = myCars.Count() / 5000;
            // myCars.Filter(car => car.Manufacturer == "Audi")
            //     .Grab(part)
            //     .ToList()
            //     .ForEach(PrintCar);
            
            //other execution forcers:
            // .Last(), .Average(), .Max(), .Min(), ...
            
        }
    }

    static class CarListOperators
    {
        internal static IEnumerable<Car> FilterVolvos(this IEnumerable<Car> cars)
        {
            foreach (var car in cars)
            {
                if (car.Manufacturer == "Volvo") yield return car;
            }
        }
        
        internal static IEnumerable<Car> Filter(this IEnumerable<Car> cars, Func<Car, bool> filter)
        {
            foreach (var car in cars)
            {
                if (filter(car))
                {
                    Console.WriteLine($"\t\tSUCCES: {car.Manufacturer} {car.Model} met condition");
                    yield return car;
                } else Console.WriteLine($"{car.Manufacturer} {car.Model} did not meet the condition");
            }
        }

        internal static void DoSomethingForEachCar(this IEnumerable<Car> cars, Action<Car> action)
        {
            foreach (var car in cars)
            {
                action(car);
            }
        }
        
        internal static IEnumerable<Car> Grab(this IEnumerable<Car> cars, int amount)
        {
            var count = 0;
            foreach (var car in cars)
            {
                Console.WriteLine("\t\tGrabbed a " + car?.Manufacturer);
                if(count < amount) yield return car;
                count++;
                if(count == amount) yield break;
            }
        }
    }
}