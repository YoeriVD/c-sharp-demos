using System;
using System.Collections.Generic;
using System.Threading;

namespace thread_examples_bis
{
    public class Buffer
    {
        private const int size = 10;
        private Queue<int> buffer = new Queue<int>(size);
        private object _lock = new object();
        public bool IsCompleted => sendCompleteInvoked && buffer.Count == 0;
        private bool sendCompleteInvoked = false;
        public void Push(int item) {
            lock (_lock)
            {
                while (buffer.Count == size) {
                    Monitor.Wait(_lock);
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Queue full. waiting");
                }
                buffer.Enqueue(item);
                
                Monitor.Pulse(_lock);
            }
        }

        public int Pull()
        {
            int result = 0;
            lock (_lock)
            {
                while (buffer.Count == 0)
                {
                    Monitor.Wait(_lock);
                    Console.WriteLine($"\t{Thread.CurrentThread.ManagedThreadId} Queue empty. waiting");
                }
                result = buffer.Dequeue();
                Monitor.Pulse(_lock);
            }
            return result;
        }
        public void SendComplete() {
            sendCompleteInvoked = true;
        }
    }
}
