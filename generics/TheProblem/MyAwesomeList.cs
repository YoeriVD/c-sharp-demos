using System.Collections;
using System.Runtime.CompilerServices;

namespace generics.TheProblem
{
    
    public class MyAwesomeList
    {
        private int[] _items;
        private int _count = 0;
        public int Size => _items.Length;

        public MyAwesomeList(int initialCapacity = 5)
        {
            this._items = new int[5];
        }
        public void Add(int item)
        {
            if (_items.Length == _count)
            {
                var temp = _items;
                _items = new int [_items.Length * 2];
                temp.CopyTo(_items, 0);
            }

            _items[_count++] = item;
        }

        public int this[int i]
        {
            get => this._items[i];
            set => this._items[i] = value;
        }
    }
}