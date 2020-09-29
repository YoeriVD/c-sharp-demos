using System.Collections;
using System.Collections.Generic;

namespace generics.WithGenerics
{
    public class MyAwesomeList<T>
    {
        private T[] _items;
        private int _count = 0;
        public int Size => _items.Length;

        public MyAwesomeList(int initialCapacity = 5)
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
        }
        public T this[int i]
        {
            get => this._items[i];
            set => this._items[i] = value;
        }

    }
}