using System.Collections;
using System.Collections.Generic;

namespace generics.ObjectSolution
{
    public class MyAwesomeList
    {
        private object[] _items;
        private int _count = 0;

        public MyAwesomeList(int initialCapacity = 5)
        {
            this._items = new object[5];
        }

        public int Size => _items.Length;

        public void Add(object item)
        {
            if (_items.Length == _count)
            {
                var temp = _items;
                _items = new object [_items.Length * 2];
                temp.CopyTo(_items, 0);
            }

            _items[_count++] = item;
        }

        public object this[int i]
        {
            get => this._items[i];
            set => this._items[i] = value;
        }
    }
}