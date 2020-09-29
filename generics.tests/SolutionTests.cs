using System;
using generics.WithGenerics;
using Xunit;

namespace generics.tests
{
    public class SolutionTests
    {
        [Fact]
        public void It_should_add_numbers()
        {
            var list = new MyAwesomeList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(3);
            
        }
                
        [Fact]
        public void It_should_add_strings()
        {
            var list = new MyAwesomeList<string>();
            list.Add("Info Support");
            list.Add("Yoeri");
        }
        
        [Fact]
        public void It_should_not_add_different_types()
        {
            var list = new MyAwesomeList<int>();
            list.Add(1);
            // list.Add("my number");
        }
        
        [Fact]
        public void It_should_return_the_object_so_i_can_do_stuff_with_it()
        {
            var list = new MyAwesomeList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(3);

            var total = 0;
            for (var i = 0; i < list.Size; i++)
            {
                total += list[i];
            }
            Assert.Equal(9, total);
        }
        
        [Fact]
        public void It_should_be_a_different_type()
        {
            Assert.NotEqual(
                new MyAwesomeList<int>().GetType(), 
                new MyAwesomeList<string>().GetType());
        }
    }
}
