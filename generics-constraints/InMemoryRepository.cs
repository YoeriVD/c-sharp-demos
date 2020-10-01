using System.Collections.Generic;
using System.Linq;

namespace generics_constraints
{
    internal class InMemoryRepository<T> : IRepository<T> where T : IHaveAnId, new()
    {
        private readonly Dictionary<int, T> _dictionary;

        public InMemoryRepository()
        {
            _dictionary = new Dictionary<int, T>();
        }

        public void Add(T entity)
        {
            _dictionary.Add(entity.Id, entity);
        }

        public void Delete(T entity)
        {
            _dictionary.Remove(entity.Id);
        }

        public T Find(int id)
        {
            return _dictionary[id];
        }

        public T Create()
        {
            return new T();
        }

        public IEnumerable<T> GetAll()
        {
            return _dictionary.Values.AsEnumerable().OfType<T>();
        }
    }
}