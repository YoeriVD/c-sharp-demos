namespace generics_constraints
{
    internal class Person : IHaveAnId
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}