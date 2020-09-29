using System;
using System.Collections;
using System.Collections.Generic;
using generics.GenericMethods;

namespace generics.GenericInterfaces
{
    public class MyAwesomeSortedList<T> : IAwesomeList<T>
    {
        private T[] _items;
        private int _count = 0;
        public int Size => _items.Length;

        public MyAwesomeSortedList(int initialCapacity = 5)
        {
            this._items = new T[5];
        }
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
            get => this._items[i];
            set => this._items[i] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.GetAwesomeEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}