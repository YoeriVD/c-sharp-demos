using System.Collections.Generic;

namespace generics.GenericInterfaces
{
    public interface IAwesomeList<T> : IEnumerable<T>
    {
        int Size { get; }
        void Add(T item);
        T this[int i] { get; set; }
    }
}