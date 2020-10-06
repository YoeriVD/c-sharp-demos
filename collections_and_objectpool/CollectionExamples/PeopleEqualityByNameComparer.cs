using System.Collections.Generic;

namespace collections_and_objectpool.CollectionExamples
{
    class PeopleEqualityByNameComparer
        : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (x == null && y == null) return true;
            if (x == null) return false;
            if (y == null) return false;
            return x.Name.Equals(y.Name);
        }
    
        public int GetHashCode(Person person)
        {
            return person.Name.GetHashCode();
        }
    }
}