using System.Collections.Generic;

namespace generics.GenericInterfaces
{
    public interface IAwesomeList<T> : IEnumerable<T>
    {
        int Size { get; }
        T this[int i] { get; set; }
        void Add(T item);
    }
}