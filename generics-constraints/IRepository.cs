using System.Collections.Generic;

namespace generics_constraints
{
    internal interface IWriteOnlyRepository<in T> // contravariant: methods may take more derived objects as parameter
    {
        void Add(T entity);
        void Delete(T entity);
    }
/// <summary>
/// covariant: methods may return derived objects eg: IRepository<Student> == IReadOnlyRepository<Person> because all Students are Persons
/// </summary>
/// <typeparam name="T"></typeparam>
    internal interface IReadOnlyRepository<out T> 
    {
        T Find(int id);
        IEnumerable<T> GetAll();
    }

    internal interface IRepository<T> : IWriteOnlyRepository<T>, IReadOnlyRepository<T>
    {
        T Create();
    }
}