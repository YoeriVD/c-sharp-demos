using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace delegates_and_events
{
    public delegate void ItemAddedDelegate(object sender, EventArgs eventArgs);
    
    public class ObservableList<T> : IList<T>
    {
        private readonly List<T> _list;
        public event ItemAddedDelegate ItemAdded;
        public ObservableList()
        {
            _list = new List<T>();
        }

        public Delegate[] GetEventListeners()
        {
            return ItemAdded?.GetInvocationList() ?? new Delegate[0];
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);
            ItemAdded?.Invoke(this, new EventArgs());
        }

        public void Clear()
        {
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