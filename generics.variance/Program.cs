using System;
using System.Linq;

namespace generics.variance
{
    class Program
    {
        static void Main(string[] args)
        {
            //students
            IRepository<Student> students = new MemoryRepository<Student>();
            AddStudents(students);
            //AddTeacher(students); //compiler error
            
            //teachers
            IRepository<Teacher> teachers = new MemoryRepository<Teacher>();
            //AddStudents(teachers); //compiler error
            AddTeacher(teachers);
            
            
            //people 
            IRepository<Person> people = new MemoryRepository<Person>();
            AddStudents(people);
            AddTeacher(people);
            
            CountAll(people);
            CountAll(students);
            CountAll(teachers);
            
        }

        private static void AddPeople(IRepository<Person> people)
        {
            people.Add(new Student {Name = "Tim", Level = 1});
            people.Add(new Student {Name = "Tom", Level = 2});
        }
        private static void AddStudents(IWriteOnlyRepository<Student> people)
        {
            people.Add(new Student {Name = "Tim", Level = 1});
            people.Add(new Student {Name = "Tom", Level = 2});
        }
        private static void AddTeacher(IWriteOnlyRepository<Teacher> people)
        {
            people.Add(new Teacher {Name = "Yoeri", Subject = "IT"});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="people"></param>
        private static void CountAll(IReadOnlyRepository<Person> people)
        {
            foreach (var person in people.GetAll())
            {
                Console.WriteLine(person.Name);
            }
            Console.WriteLine($"There are {people.GetAll().Count()} people in the repository");
        }
    }
}