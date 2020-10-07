using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace custom_awaitables
{
    public static class BucketExtensions
    {
        public static Task<int> FillBucket(this Bucket bucket)
        {
            var tcs = new TaskCompletionSource<int>();
            Task.Run(async () =>
            {
                bucket.BucketFull += (o, eventArgs) => tcs.SetResult(eventArgs.Content);
                while (!tcs.Task.IsCompleted)
                {
                    Console.WriteLine("Filling bucket ...");
                    bucket.Fill(1);
                    await TimeSpan.FromSeconds(1);
                }
            });
            return tcs.Task;
        }

        public static TaskAwaiter GetAwaiter(this TimeSpan span)
        {
            return Task.Delay(span).GetAwaiter();
        }
    }
}