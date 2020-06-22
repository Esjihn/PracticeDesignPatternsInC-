using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Singletons
{
    public sealed class PerThread
    {
        private static ThreadLocal<PerThread> threadInstance
            = new ThreadLocal<PerThread>(
                () => new PerThread());

        public int Id;

        public PerThread()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThread Instance => threadInstance.Value;
    }

    public class PerThreadSingleton
    {
        // Lazy<T>: thread safety. Can also have one singleton per thread. 
        // [ThreadStatic] ThreadLocal<>

        // Change to Main to run. 
        public static void none(string[] args)
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("t1: " + PerThread.Instance.Id);
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("t2: " + PerThread.Instance.Id);
                Console.WriteLine("t2: " + PerThread.Instance.Id);
            });

            Task.WaitAll(t1, t2);
        }
    }
}
