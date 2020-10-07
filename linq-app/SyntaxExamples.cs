using System;
using System.Linq;
using linq_app.entities;

namespace linq_app
{
    internal static class SyntaxExamples
    {
        static void PrintCar(Car car)
        {
            Console.WriteLine($"{car.Manufacturer,-20}{car.Model,-30}{car.Year}");
        }

        public static void Run()
        {
            var cars = Generator.ListOfCars(500);

            // // order by
            // cars
            //     .OrderBy(c => c.Manufacturer)
            //     .ToList()
            //     .ForEach(PrintCar);
            //
            // // order by multiple (wrong)
            // cars
            //     .OrderBy(c => c.Manufacturer)
            //     .OrderBy(c => c.Model)
            //     .ToList()
            //     .ForEach(PrintCar);

            // // order by multiple (right)
            // cars
            //     .OrderBy(c => c.Manufacturer)
            //     .ThenBy(c => c.Model)
            //     .ThenByDescending(c => c.Year)
            //     .ToList()
            //     .ForEach(PrintCar);

            // group by
            // cars
            //     .GroupBy(c => c.Manufacturer)
            //     .Select(group => new
            //     {
            //         Manufacturer = group.Key,
            //         Cars = group
            //             .OrderBy(c => c.Model)
            //             .ThenByDescending(c => c.Year)
            //     })
            //     .ToList()
            //     .ForEach(group =>
            //     {
            //         Console.WriteLine(
            //             $"{summary.Manufacturer}'s youngest model is from {summary.Cars.Max(car => car.Year)}"
            //         );
            //         group.Cars.ToList().ForEach(car => Console.WriteLine($"\t{car.Model, -30}{car.Year}"));
            //     });

            // maybe.. we can make this more readable?
            // intro to comprehension syntax!

            // start easy with
            // cars
            //     .OrderBy(c => c.Manufacturer)
            //     .ToList()
            //     .ForEach(PrintCar);

            // var selection = from car in cars
            //     orderby car.Manufacturer
            //     select car;
            // selection.ToList().ForEach(PrintCar);

            // var selection = from car in cars
            //     orderby car.Manufacturer, car.Model, car.Year descending 
            //     select car;
            // selection.ToList().ForEach(PrintCar);

            // var selection =
            //         from car in cars
            //         where car.Year > 1970
            //         group car by car.Manufacturer
            //         into grouping
            //         select new
            //         {
            //             Manufacturer = grouping.Key,
            //             Cars =
            //                 from carInGroup in grouping
            //                 orderby carInGroup.Model, carInGroup.Year descending
            //                 select carInGroup
            //         }
            //     ;
                        
            // selection.ToList().ForEach(summary =>
            // {
            //     Console.WriteLine(
            //         $"{summary.Manufacturer}'s youngest model is from {summary.Cars.Max(car => car.Year)}"
            //     );
            //     summary.Cars.ToList().ForEach(car => Console.WriteLine($"\t{car.Model,-30}{car.Year}"));
            // }
            
            // var selection =
            //         from car in cars
            //         where car.Year > 1970
            //         group car by car.Manufacturer
            //         into grouping
            //         let carsForManufacturer = 
            //             from carInGroup in grouping
            //             orderby carInGroup.Model, carInGroup.Year descending
            //             select carInGroup
            //         select new
            //         {
            //             Manufacturer = grouping.Key,
            //             YoungestModelYear = carsForManufacturer.Max(c => c.Year),
            //             Cars = carsForManufacturer
            //         }
            //     ;
            //
            //     selection.ToList().ForEach(summary =>
            //     {
            //         Console.WriteLine(
            //             $"{summary.Manufacturer}'s youngest model is from {summary.YoungestModelYear}"
            //             );
            //         summary.Cars.ToList().ForEach(car => Console.WriteLine($"\t{car.Model,-30}{car.Year}"));
            //     }
            // );
            
            // NOTES: GroupBy and OrderBy are lazily executed (so only when data is needed), but all at once however (you need all data to group or order)
            // ORDER OF OPERATORS MATTERS (Where first, then Count/OrderBy/GroupBy ...)
        }
    }
}