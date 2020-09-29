using System;
using generics.ObjectSolution;
using Xunit;

namespace generics.tests
{
    public class ProblemTests
    {
        [Fact]
        public void It_should_add_numbers()
        {
            var list = new MyAwesomeList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(3);
            
        }
                
        [Fact]
        public void It_should_add_strings()
        {
            var list = new MyAwesomeList();
            list.Add("Info Support");
            list.Add("Yoeri");
        }
        
        [Fact]
        public void It_should_not_add_different_types()
        {
            var list = new MyAwesomeList();
            list.Add(1);
            list.Add("my number");
        }
        
        [Fact]
        public void It_should_return_the_object_so_i_can_do_stuff_with_it()
        {
            var list = new MyAwesomeList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(3);

            var total = 0;
            for (var i = 0; i < list.Size; i++)
            {
                total += (int)list[i];
            }
            Assert.Equal(9, total);
        }
    }
}
