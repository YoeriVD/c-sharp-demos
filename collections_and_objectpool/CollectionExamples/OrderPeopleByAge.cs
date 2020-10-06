using System.Collections.Generic;

namespace collections_and_objectpool.CollectionExamples
{
    class OrderPeopleByAge : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Age.CompareTo(y.Age);
        }
    }
}