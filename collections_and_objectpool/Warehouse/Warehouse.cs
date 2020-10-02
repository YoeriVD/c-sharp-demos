using System.Threading;
using Microsoft.Extensions.ObjectPool;

namespace collections_and_objectpool.Warehouse
{
    public class Equipment
    {
        public Equipment()
        {
            // fake long creation time
            Thread.Sleep(2000);
        }
    }
    
    public class Warehouse : ObjectPool<Equipment>  // nuget package: Microsoft.Extensions.ObjectPool
    {
        private readonly DefaultObjectPool<Equipment> _pool = new DefaultObjectPool<Equipment>(new DefaultPooledObjectPolicy<Equipment>());

        private Warehouse()
        {
            
        }

        public static Warehouse Instance { get; } = new Warehouse();

        public override Equipment Get()
        {
            return _pool.Get();
        }

        public override void Return(Equipment equipment)
        {
            _pool.Return(equipment);
        }
    }
}