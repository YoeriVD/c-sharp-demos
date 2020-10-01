using System;
using System.Linq;

namespace generics_constraints
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Students");
            Console.WriteLine("--------");
            IRepository<Student> students = new InMemoryRepository<Student>();
            AddStudent(students);
            CountAllPeople(students);
            PrintAllPeople(students);


            Console.WriteLine("\nTeachers");
            Console.WriteLine("--------");
            IRepository<Teacher> teachers = new InMemoryRepository<Teacher>();
            AddTeacher(teachers);
            CountAllPeople(teachers);
            PrintAllPeople(teachers);
        }

        private static void AddTeacher(IWriteOnlyRepository<Teacher> repo)
        {
            repo.Add(new Teacher {Name = "Yoeri", Id = 3, Subject = "IT"});
        }

        private static void AddStudent(IWriteOnlyRepository<Student> repo)
        {
            repo.Add(new Student {Name = "Tim", Id = 1, AverageScore = 3});
            repo.Add(new Student {Name = "Tom", Id = 2, AverageScore = 9});
        }

        private static void CountAllPeople(IReadOnlyRepository<Person> repo)
        {
            Console.WriteLine($"Number of people: {repo.GetAll().Count()}");
        }

        private static void PrintAllPeople(IReadOnlyRepository<Person> repo)
        {
            foreach (var person in repo.GetAll())
            {
                Console.WriteLine($"Person: {person.Name}");
            }
        }
    }
}