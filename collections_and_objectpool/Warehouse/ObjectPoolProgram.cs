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
            
            Console.WriteLine("Get new equipment");
            var equipment = warehouse.Get();
            Console.WriteLine("Time elapsed:" + clock.ElapsedMilliseconds);
            clock.Restart();
            
            Console.WriteLine("Get new equipment");
            var moreEquipment = warehouse.Get();
            Console.WriteLine("Time elapsed:" + clock.ElapsedMilliseconds);

            Console.WriteLine("Give equipment back");
            warehouse.Return(equipment);
            clock.Restart();

            
            Console.WriteLine("Get (reused) equipment");
            equipment = warehouse.Get();
            Console.WriteLine("Time elapsed:" + clock.ElapsedMilliseconds);
            
            Console.WriteLine("End.");

        }
    }
}