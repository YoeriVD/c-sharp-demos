using System;

namespace generics_constraints
{
    internal class Student : Person
    {
        public int AverageScore { get; set; }

        public void DoWork()
        {
            Console.WriteLine("I am learning");
        }
    }
}