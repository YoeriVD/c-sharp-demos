using System.Collections.Generic;

namespace collections_and_objectpool.CollectionExamples
{
    class PeopleEqualityByIdComparer
        : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (x == null && y == null) return true;
            if (x == null) return false;
            if (y == null) return false;
            return x.Id.Equals(y.Id);
        }
    
        public int GetHashCode(Person person)
        {
            return person.Id.GetHashCode();
        }
    }
}