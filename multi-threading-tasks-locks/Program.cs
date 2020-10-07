using System;
using System.Threading;
using System.Threading.Tasks;

namespace multi_threading_tasks_locks
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // await LockExample.Run();
            // await LockExample.RunSafe();
            // await MutexExample.Run();
            // await SemaphoreSlimExample.Run();
            
            // await ConcurrentCollectionExample.Run();
            //
            // LazyExample.Run();
            // LazyExample.RunSafe();
            // LazyExample.RunSafeInitializer();
            // await ChildAttachExample.Run();
            // Console.WriteLine("End example");
            
            DoSomething();
            Console.WriteLine("Finished.");
            Console.ReadKey();
        }

        static async void DoSomething()
        {
            Thread.Sleep(100);
            Console.WriteLine("I'm doing something");

            var tsk1 = LogSomethingElse();
            var tsk2 = LogSomethingElse2();

            await Task.WhenAll(tsk1, tsk2);

            //await LogSomethingElse();
            //await LogSomethingElse2();
        }

        private static async Task LogSomethingElse()
        {
            await Task.Delay(500);
            Console.WriteLine("Log from log method");
        }
        private static async Task LogSomethingElse2()
        {
            await Task.Run(() => Console.WriteLine("Log from second log method"));
        }
    }

    public class ChildAttachExample
    {
        public static Task Run()
        {
            return Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("End inner task");
                }, TaskCreationOptions.AttachedToParent);
                
                Console.WriteLine("End task 1");
            }, TaskCreationOptions.DenyChildAttach);
        }
    }
}