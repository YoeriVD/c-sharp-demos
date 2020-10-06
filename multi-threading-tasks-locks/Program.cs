using System.Threading.Tasks;

namespace multi_threading_tasks_locks
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await LockExample.Run();
            // await LockExample.RunSafe();
            // await MutexExample.Run();
            // await SemaphoreSlimExample.Run();
            // await ConcurrentCollectionExample.Run();
            //
            // LazyExample.Run();
            // LazyExample.RunSafe();
            // LazyExample.RunSafeInitializer();
        }
    }
}