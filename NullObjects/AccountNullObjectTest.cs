using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace NullObjects
{
    public interface ILog2
    {
        // maximum # of elements in the log
        int RecordLimit { get; }

        // number of elements already in the log
        int RecordCount { get; set; }

        // expected to increment RecordCount
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog2 log;

        public Account(ILog2 log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");

            if (c + 1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();
        }

        public override string ToString()
        {
            return $"{nameof(log)}: {log.RecordCount}: {log.RecordLimit}";
        }
    }

    public class NullLog2 : ILog2
    {
        public NullLog2()
        {
            RecordLimit = 10;
        }

        public int RecordLimit { get; }
        public int RecordCount { get; set; }
        public void LogInfo(string message)
        {
            RecordCount++;
            Console.WriteLine(message);
        }
    }

    public class AccountNullObjectTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<Account>();
            cb.RegisterType<NullLog2>().As<ILog2>();

            using (var c = cb.Build())
            {
                var acct = c.Resolve<Account>();
                acct.SomeOperation();
            }
        }
    }
}
