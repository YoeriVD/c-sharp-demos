namespace collections_and_objectpool.CollectionExamples
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
    
        public override string ToString()
        {
            return $"{Name} has ID {Id} and is {Age} years old. Profession: {Profession}.";
        }
    
        public int Age { get; set; }
    }
}