using System;

namespace collections_and_objectpool.Warehouse
{
    public class ObjectPoolProgram
    {
        public static void Run(string[] args)
        {
            var warehouse = Warehouse.Instance;
            var clock = new System.Diagnostics.Stopwatch();
            clock.Start();
            
            Console.WriteLine("Get equipment");
            var equipment = warehouse.Get();
            Console.WriteLine("Time elapsed:" + clock.Elapsed.Seconds);
            clock.Restart();
            
            Console.WriteLine("Get equipment");
            var moreEquipment = warehouse.Get();
            Console.WriteLine("Time elapsed:" + clock.Elapsed.Seconds);

            Console.WriteLine("Give equipment back");
            warehouse.Return(equipment);
            clock.Restart();

            
            Console.WriteLine("Get equipment");
            equipment = warehouse.Get();
            Console.WriteLine("Time elapsed:" + clock.Elapsed.Seconds);
            
            

        }
    }
}