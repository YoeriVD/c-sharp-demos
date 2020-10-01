using generics.GenericInterfaces;
using Xunit;

namespace generics.tests
{
    public class GenericInterfaceTests
    {
        [Fact]
        public void items_should_be_sorted_from_smallest_to_largest()
        {
            IAwesomeList<int> list = new MyAwesomeList<int>();
            // IAwesomeList<int> list = new MyAwesomeSortedList<int>();
            list.Add(3);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            var previous = int.MinValue;
            for (var i = 0; i < list.Size; i++)
            {
                var current = list[i];
                Assert.True(previous <= current);
                previous = current;
            }
        }


        public void items_should_support_foreach()
        {
            IAwesomeList<int> list = new MyAwesomeList<int>();
            // IAwesomeList<int> list = new MyAwesomeSortedList<int>();
            list.Add(3);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            var previous = int.MinValue;
            foreach (var current in list)
            {
                Assert.True(previous <= current);
                previous = current;
            }
        }

        [Fact]
        public void items_should_sort_employees()
        {
            IAwesomeList<Employee> list = new MyAwesomeSortedList<Employee>();
            list.Add(new Employee {Name = "Yoeri", Age = 30});
            list.Add(new Employee {Name = "Tim", Age = 35});
            list.Add(new Employee {Name = "Ruben", Age = 25});

            var previous = new Employee {Age = int.MinValue};
            foreach (var current in list)
            {
                Assert.True(previous.Age <= current.Age);
                previous = current;
            }
        }
    }
}