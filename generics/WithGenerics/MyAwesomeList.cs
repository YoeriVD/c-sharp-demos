namespace generics.WithGenerics
{
    public class MyAwesomeList<T>
    {
        private int _count;
        private T[] _items;

        public MyAwesomeList(int initialCapacity = 5)
        {
            _items = new T[5];
        }

        public int Size => _items.Length;

        public T this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
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
    }
}