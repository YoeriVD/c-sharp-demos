using System;
using System.Threading;

namespace semaphore_example
{
    public class Program
    {
        // start this process twice (in parallel)
        public static void Main(string[] args)
        {
            var semName = "TheSemaPhore";


            if (Semaphore.TryOpenExisting(semName, out var sem))
            {
                Console.WriteLine("Releasing semaphore");
                sem.Release();
            }
            else
            {
                Console.WriteLine("Creating semaphore");
                var newSem = new Semaphore(0,1, semName);
                newSem.WaitOne();
            }

            Console.WriteLine("Process exited!");
        }
    }
}