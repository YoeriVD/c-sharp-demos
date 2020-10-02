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
        private DefaultObjectPool<Equipment> pool = new DefaultObjectPool<Equipment>(new DefaultPooledObjectPolicy<Equipment>());

        private Warehouse()
        {
            
        }

        public static Warehouse Instance { get; } = new Warehouse();

        public override Equipment Get()
        {
            return pool.Get();
        }

        public override void Return(Equipment equipment)
        {
            pool.Return(equipment);
        }
    }
}