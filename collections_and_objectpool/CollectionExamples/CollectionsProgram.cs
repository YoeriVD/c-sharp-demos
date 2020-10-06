using System.Collections.Generic;

namespace collections_and_objectpool.CollectionExamples
{
    internal static class CollectionsProgram
    {
        public static void Run(string[] args)
        {
            var collection = new List<Person>();

            // var collection = new HashSet<Person>(new PeopleEqualityByIdComparer());
            // var collection = new HashSet<Person>(new PeopleEqualityByNameComparer());
            // var collection = new SortedSet<Person>(new OrderPeopleByAge());


            collection.Add(new Person {Id = 1, Name = "Yoeri", Age = 30, Profession = "Teacher"});
            collection.Add(new Person {Id = 1, Name = "Yoeri", Age = 30, Profession = "Musician"});
            collection.Add(new Person {Id = 1, Name = "Johnny", Age = 47, Profession = "Teacher"});
            collection.Add(new Person {Id = 2, Name = "Tim", Age = 33, Profession = "Accountant"});
            collection.Add(new Person {Id = 3, Name = "Ruben", Age = 25, Profession = "Accountant"});
            collection.Add(new Person {Id = 4, Name = "Gerrit", Age = 20, Profession = "Teacher"});
            // collection.Sort(new OrderPeopleByAge());

            // var collection = new Dictionary<string, IList<Person>>();
            //
            // void AddPersonToDict(IDictionary<string, IList<Person>> dictionary, Person p)
            // {
            //     if (!dictionary.ContainsKey(p.Profession))
            //     {
            //         dictionary.Add(p.Profession, new List<Person>());
            //     }
            //
            //     dictionary[p.Profession].Add(p);
            // }
            //
            // AddPersonToDict(collection, new Person() {Id = 1, Name = "Yoeri", Age = 30, Profession = "Teacher"});
            // AddPersonToDict(collection, new Person() {Id = 1, Name = "Yoeri", Age = 30, Profession = "Musician"});
            // AddPersonToDict(collection, new Person() {Id = 1, Name = "Johnny", Age = 47, Profession = "Teacher"});
            // AddPersonToDict(collection, new Person() {Id = 2, Name = "Tim", Age = 33, Profession = "Accountant"});
            // AddPersonToDict(collection, new Person() {Id = 3, Name = "Ruben", Age = 25, Profession = "Accountant"});
            // AddPersonToDict(collection, new Person() {Id = 4, Name = "Gerrit", Age = 20, Profession = "Teacher"});

            collection.Print();
        }
    }
}