using System;

namespace generics.GenericInterfaces
{
    public class Employee : IComparable<Employee>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int CompareTo(Employee other)
        {
            return other == null
                ? 1
                : Age.CompareTo(other.Age);
        }
    }
}