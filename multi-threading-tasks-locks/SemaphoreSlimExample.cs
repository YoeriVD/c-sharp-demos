using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace multi_threading_tasks_locks
{
    public class SemaphoreSlimExample
    {
        public static async Task Run()
        {
            // semaphore allows more control than a regular lock or mutex: you can allow as many parallel threads as you like
            var sem = new SemaphoreSlim(1,1);
            var list = new List<string>();
            var tasks = new List<Task>();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                tasks.Add(AddItem(list, i, sem));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine(list.Count);
        }

        private static async Task AddItem(List<string> list, int number, SemaphoreSlim sem)
        {
            await sem.WaitAsync();
            list.Add($"THE NUMBER {number}");
            sem.Release();
        }
    }
}