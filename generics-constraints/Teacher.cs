using System;

namespace generics_constraints
{
    internal class Teacher : Person
    {
        public string Subject { get; set; }

        public void DoWork()
        {
            Console.WriteLine("I am teaching");
        }
    }
}