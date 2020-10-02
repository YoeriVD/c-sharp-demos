using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace collections_and_objectpool
{
    internal class CollectionsProgram
    {
        class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Profession { get; set; }

            public override string ToString()
            {
                return $"{Name} has ID {Id} and is {Age} years old. Profession: {Profession}.";
            }

            public int Age { get; set; }
        }

        class PeopleEqualityByIdComparer
            : IEqualityComparer<Person>
        {
            public bool Equals(Person x, Person y)
            {
                if (x == null && y == null) return true;
                if (x == null) return false;
                if (y == null) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(Person person)
            {
                return person.Id.GetHashCode();
            }
        }

        class PeopleEqualityByNameComparer
            : IEqualityComparer<Person>
        {
            public bool Equals(Person x, Person y)
            {
                if (x == null && y == null) return true;
                if (x == null) return false;
                if (y == null) return false;
                return x.Name.Equals(y.Name);
            }

            public int GetHashCode(Person person)
            {
                return person.Name.GetHashCode();
            }
        }

        class OrderPeopleByAge : IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                return x.Age.CompareTo(y.Age);
            }
        }

        public static void Run(string[] args)
        {
            //var collection = new List<Person>();

            // var collection = new HashSet<Person>(new PeopleEqualityByIdComparer());
            // var collection = new HashSet<Person>(new PeopleEqualityByNameComparer());
            // var collection = new SortedSet<Person>(new OrderPeopleByAge());


            // collection.Add(new Person() {Id = 1, Name = "Yoeri",  Age = 30, Profession = "Teacher"});
            // collection.Add(new Person() {Id = 1, Name = "Yoeri",  Age = 30, Profession = "Musician"});
            // collection.Add(new Person() {Id = 1, Name = "Johnny", Age = 47, Profession = "Teacher"});
            // collection.Add(new Person() {Id = 2, Name = "Tim",    Age = 33, Profession = "Accountant"});
            // collection.Add(new Person() {Id = 3, Name = "Ruben",  Age = 25, Profession = "Accountant"});
            // collection.Add(new Person() {Id = 4, Name = "Gerrit", Age = 20, Profession = "Teacher"});
            // collection.Sort(new OrderPeopleByAge());

            var collection = new Dictionary<string, IList<Person>>();

            void AddPersonToDict(IDictionary<string, IList<Person>> dictionary, Person p)
            {
                if (!dictionary.ContainsKey(p.Profession))
                {
                    dictionary.Add(p.Profession, new List<Person>());
                }

                dictionary[p.Profession].Add(p);
            }

            AddPersonToDict(collection, new Person() {Id = 1, Name = "Yoeri", Age = 30, Profession = "Teacher"});
            AddPersonToDict(collection, new Person() {Id = 1, Name = "Yoeri", Age = 30, Profession = "Musician"});
            AddPersonToDict(collection, new Person() {Id = 1, Name = "Johnny", Age = 47, Profession = "Teacher"});
            AddPersonToDict(collection, new Person() {Id = 2, Name = "Tim", Age = 33, Profession = "Accountant"});
            AddPersonToDict(collection, new Person() {Id = 3, Name = "Ruben", Age = 25, Profession = "Accountant"});
            AddPersonToDict(collection, new Person() {Id = 4, Name = "Gerrit", Age = 20, Profession = "Teacher"});

            Print(collection);
        }

        private static void Print<T, K>(IDictionary<T, K> dictionary) where K : IEnumerable
        {
            foreach (var key in dictionary.Keys)
            {
                Console.WriteLine(key);
                foreach (var person in dictionary[key])
                {
                    Console.WriteLine("\t" + person);
                }
            }
        }

        private static void Print<T>(IEnumerable<T> collection)
        {
            foreach (var person in collection)
            {
                Console.WriteLine(person);
            }
        }
    }
}