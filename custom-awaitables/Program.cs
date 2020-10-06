using System;
using System.Threading;
using System.Threading.Tasks;

namespace custom_awaitables
{
    class Program
    {
        // static void Main(string[] args)
        // {
        //     var bucket = new Bucket(20);
        //     var bucketNotFull = true;
        //     bucket.BucketFull += (o, eventArgs) => bucketNotFull = false;
        //     while (bucketNotFull)
        //     {
        //         bucket.Fill(1);
        //         Thread.Sleep(100);
        //     }
        //     Console.WriteLine("Hello World!");
        // }
        
        static async Task Main(string[] args)
        {
            var bucket = new Bucket(20);
            await bucket.FillBucket();
            Console.WriteLine("Hello World!");
        }
    }
}