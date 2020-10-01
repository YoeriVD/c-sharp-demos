using System;

namespace generics_tricky_situations
{
    internal class MyItem<T>
    {
        public static int InstanceCount { get; private set; }

        public MyItem()
        {
            InstanceCount++;
        }
    }
    internal class Person{}
    internal class Teacher : Person{}

    internal class Program
    {
        private static void Main(string[] args)
        {
            new MyItem<int>();
            new MyItem<int>();
            
            new MyItem<string>();
            
            Console.WriteLine(MyItem<int>.InstanceCount);
            Console.WriteLine(MyItem<string>.InstanceCount);
            
            new MyItem<Person>();
            new MyItem<Person>();
            new MyItem<Teacher>();
            
            Console.WriteLine(MyItem<Person>.InstanceCount);
            Console.WriteLine(MyItem<Teacher>.InstanceCount);
        }
    }
}