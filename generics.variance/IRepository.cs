using System.Collections.Generic;

namespace generics.variance
{
    internal interface IWriteOnlyRepository<in T>
    {
        void Add(T teacher);
    }
    internal interface IReadOnlyRepository<out T>
    {
        IEnumerable<T> GetAll();
    }
    
    internal interface IRepository<T> : IWriteOnlyRepository<T>, IReadOnlyRepository<T>
    {
        
    }
}