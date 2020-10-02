using System.Collections;
using System.Collections.Generic;

namespace delegates_and_events
{
    class LoggerList<T> : IList<T>
    {
        // Describe the return type and the parameters
        public delegate void LogFunction(string message);

        // declare a property of the function type we just describe
        public LogFunction Log { get; set; }

        private readonly List<T> _list;

        public LoggerList()
        {
            _list = new List<T>();
        }


        public IEnumerator<T> GetEnumerator()
        {
            Log?.Invoke(nameof(GetEnumerator));
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Log?.Invoke(nameof(IEnumerable.GetEnumerator));
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Log?.Invoke(nameof(Add));
            _list.Add(item);
        }

        public void Clear()
        {
            Log?.Invoke(nameof(Add));
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count { get; }
        public bool IsReadOnly { get; }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
    }
}