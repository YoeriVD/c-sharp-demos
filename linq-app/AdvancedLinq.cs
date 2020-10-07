using System;
using System.Linq;
using linq_app.entities;

namespace linq_app
{
    internal static class AdvancedLinq
    {
        public static void Run()
        {
            //RunJoinSample();
            //RunJoinSampleMethods();
            //SampleAggregate();
        }


        private static void RunJoinSample()
        {
            // join two collection by comparing properties

            var carsJoinedWithPeople =
                from car in Generator.ListOfCars(100)
                join person in Generator.GetPeople().Take(100)
                    on car.Year equals person.DateOfBirth.Year
                    into people
                // project into a new object for further readability
                select new
                {
                    Car = car,
                    People = people
                };

            var groupedByManufacturerAndModel =
                from pair in carsJoinedWithPeople
                group pair by pair.Car.Manufacturer
                into summary
                select new
                {
                    Manufacturer = summary.Key,
                    Models = from pair in summary
                        group pair by pair.Car.Model
                        into peoplePerModel
                        select new
                        {
                            Model = peoplePerModel.Key,
                            People =
                                from manufactur in peoplePerModel
                                select manufactur.People
                                into listOfLists // flatten the collection
                                from people in listOfLists
                                select people
                        }
                };

            foreach (var manufacturer in groupedByManufacturerAndModel)
            {
                Console.WriteLine(manufacturer.Manufacturer);
                foreach (var model in manufacturer.Models)
                {
                    Console.WriteLine("\t" + model.Model);
                    foreach (var person in model.People)
                    {
                        Console.WriteLine($"\t\t{person.FirstName} {person.LastName}");
                    }
                }
            }
        }

        private static void RunJoinSampleMethods()
        {
            // join two collection by comparing properties

            var groupedByManufacturerAndModel =
                Generator.ListOfCars(100)
                    .GroupJoin(
                        Generator.GetPeople().Take(100),
                        car => car.Year,
                        person => person.DateOfBirth.Year,
                        (car, people) => new {Car = car, People = people}
                    )
                    .GroupBy(pair => pair.Car.Manufacturer)
                    .Select(manufacturer => new
                    {
                        Manufacturer = manufacturer.Key,
                        Models = manufacturer
                            .GroupBy(m => m.Car.Model)
                            .Select(perModel => new
                            {
                                Model = perModel.Key,
                                People = perModel.SelectMany(m => m.People)
                            })
                    });


            foreach (var manufacturer in groupedByManufacturerAndModel)
            {
                Console.WriteLine(manufacturer.Manufacturer);
                foreach (var model in manufacturer.Models)
                {
                    Console.WriteLine("\t" + model.Model);
                    foreach (var person in model.People)
                    {
                        Console.WriteLine($"\t\t{person.FirstName} {person.LastName}");
                    }
                }
            }
        }

        private static void SampleAggregate()
        {
            var cars = Generator.ListOfCars(100);

            var combineUsage =
                cars
                    .GroupBy(c => c.Manufacturer)
                    .Select(group => new
                    {
                        Manufacturer = group.Key,
                        // Combined = group.Sum(g => g.Usage)
                        Combined = group
                            .Aggregate(0, (current, next) => current += next.Usage)
                    });

            foreach (var usagePerManufacturer in combineUsage)
            {
                Console.WriteLine($"{usagePerManufacturer.Manufacturer}: {usagePerManufacturer.Combined}l");
            }
        }
    }
}