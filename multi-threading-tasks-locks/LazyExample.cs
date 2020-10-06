using System;
using System.Threading;
using System.Threading.Tasks;

namespace multi_threading_tasks_locks
{
    public class LazyExample
    {
        class MyDummy
        {
            public MyDummy()
            {
                Console.WriteLine("ctor called");
            }
        }
        public static void Run()
        {
            var myDummy = default(MyDummy);
            Action action = () => { myDummy ??= new MyDummy(); };
            Parallel.Invoke(action, action);
        }

        public static void RunSafe()
        {
            var lazyDummy = new Lazy<MyDummy>(() => new MyDummy());
            var myDummy = default(MyDummy);
            Action action = () => { myDummy = lazyDummy.Value ?? new MyDummy(); };
            Parallel.Invoke(action, action);
        }
        
        public static void RunSafeInitializer()
        {
            var myDummy = default(MyDummy);
            Action action = () => { myDummy = LazyInitializer.EnsureInitialized(ref myDummy, () => new MyDummy()); };
            Parallel.Invoke(action, action);
        }
    }
}