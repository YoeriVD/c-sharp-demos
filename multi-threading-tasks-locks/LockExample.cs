using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace multi_threading_tasks_locks
{
    public class LockExample
    {
        public static async Task Run()
        {
            var list = new List<string>();
            var tasks = new List<Task>();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                tasks.Add(
                    Task.Run(() => list.Add($"THE NUMBER {i}"))
                );
            }

            await Task.WhenAll(tasks);
            Console.WriteLine(list.Count);
        }
        private static object _lockObject = new object();
        public static async Task RunSafe()
        {
            var list = new List<string>();
            var tasks = new List<Task>();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                tasks.Add(
                    Task.Run(() =>
                    {
                        lock(_lockObject) list.Add($"THE NUMBER {i}");
                    })
                );
            }

            await Task.WhenAll(tasks);
            Console.WriteLine(list.Count);
        }
    }
}