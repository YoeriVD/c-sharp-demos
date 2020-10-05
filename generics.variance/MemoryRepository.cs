using System.Collections.Generic;
using System.Linq;

namespace generics.variance
{
    internal class MemoryRepository<T> : IRepository<T>{
        private List<T> _list;

        public MemoryRepository()
        {
            _list = new List<T>();
        }
        public void Add(T teacher)
        {
            _list.Add(teacher);
        }

        public IEnumerable<T> GetAll()
        {
            return _list.AsEnumerable();
        }
    }
}