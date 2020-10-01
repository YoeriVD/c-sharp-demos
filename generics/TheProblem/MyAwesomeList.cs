namespace generics.TheProblem
{
    public class MyAwesomeList
    {
        private int _count;
        private int[] _items;

        public MyAwesomeList(int initialCapacity = 5)
        {
            _items = new int[5];
        }

        public int Size => _items.Length;

        public int this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
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
    }
}