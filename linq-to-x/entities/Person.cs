using System;
using System.Collections.Generic;

namespace linq_to_x.entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}