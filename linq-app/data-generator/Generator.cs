using System;
using System.Collections.Generic;
using Bogus;
using linq_app.entities;


namespace linq_app
{
    public static class Generator
    {
        private static Lazy<Bogus.Faker<Car>> _carFakerLazy = new Lazy<Bogus.Faker<Car>>(() =>
            new Bogus.Faker<Car>()
                .RuleFor(c => c.Model, f => f.Vehicle.Model())
                .RuleFor(c => c.Manufacturer, f => f.Vehicle.Manufacturer())
                .RuleFor(c => c.Year, f => f.Random.Number(1960, 2020))
                .RuleFor(c => c.Usage, f => f.Random.Number(0,20))
        );

        public static Bogus.Faker<Car> CarFaker => _carFakerLazy.Value;

        public static IEnumerable<Car> ListOfCars(int count)
        {
            return CarFaker.GenerateLazy(count);
        }
        
        public static IEnumerable<Car> InfiniteListOfCars()
        {
            var count = 0;
            while (true)
            {
                Console.WriteLine($"Generating car number {++count}");
                yield return CarFaker.Generate();
            }
        }

        public static IEnumerable<Person> GetPeople()
        {
            while (true)
            {
                yield return new Bogus.Person();
            }
        }
        public static IEnumerable<string> GetComments()
        {
            while (true)
            {
                yield return new Bogus.Randomizer().Words();
            }
        }
    }

    class Blog
    {
        
    }
    
}