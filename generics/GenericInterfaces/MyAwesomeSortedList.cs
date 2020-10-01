using System;
using System.Collections;
using System.Collections.Generic;

namespace generics.GenericInterfaces
{
    public class MyAwesomeSortedList<T> : IAwesomeList<T> where T : IComparable<T>
    {
        private int _count;
        private T[] _items;

        public MyAwesomeSortedList(int initialCapacity = 5)
        {
            _items = new T[5];
        }

        public int Size => _items.Length;

        public void Add(T item)
        {
            if (_items.Length == _count)
            {
                var temp = _items;
                _items = new T [_items.Length * 2];
                temp.CopyTo(_items, 0);
            }

            _items[_count++] = item;
            Array.Sort(_items);
        }

        public T this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _items)
            {
                if (item != null)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}