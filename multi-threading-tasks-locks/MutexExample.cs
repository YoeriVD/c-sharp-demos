using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace multi_threading_tasks_locks
{
    public class MutexExample
    {
        public static async Task Run()
        {
            // mutex allows one thread to own a resource
            var mtx = new Mutex();
            var list = new List<string>();
            var tasks = new List<Task>();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                tasks.Add(
                    Task.Run(() =>
                    {
                        mtx.WaitOne();
                        list.Add($"THE NUMBER {i}");
                        mtx.ReleaseMutex();
                    })
                );
            }

            await Task.WhenAll(tasks);
            Console.WriteLine(list.Count);
        }
    }
}