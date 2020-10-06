using System;

namespace custom_awaitables
{
    public class Bucket
    {
        private readonly int _capacity;
        private int _contents = 0;
        public event Action<object, BucketFullEventArgs> BucketFull;

        public Bucket(int capacity)
        {
            _capacity = capacity;
        }

        public void Fill(int l)
        {
            _contents += l;
            if (_contents >= l) BucketFull?.Invoke(this, new BucketFullEventArgs(){ Content = _contents});
        }
    }
}