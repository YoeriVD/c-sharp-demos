namespace generics.ObjectSolution
{
    public class MyAwesomeList
    {
        private int _count;
        private object[] _items;

        public MyAwesomeList(int initialCapacity = 5)
        {
            _items = new object[5];
        }

        public int Size => _items.Length;

        public object this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
        }

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
    }
}