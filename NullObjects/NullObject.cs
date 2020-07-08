using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Autofac;
using ImpromptuInterface;
using JetBrains.Annotations;

namespace NullObjects
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    public class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Warn(string msg)
        {
            Console.WriteLine($"WARNING!!! {msg}");
        }
    }

    public class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log?.Info($"Deposited {amount}, balance is now {balance}");
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    // no-op(erations) class which allows you to pass in an null without violating the open close principle for bank account.
    public class NullLog : ILog
    {
        public void Info(string msg)
        {
            
        }

        public void Warn(string msg)
        {
            
        }
    }

    // alternative will using DLR (massive performance hit for null object)
    public class Null<TInterface> : DynamicObject where TInterface : class
    {
        public static TInterface Instance
        {
            get
            {
                return new Null<TInterface>().ActLike<TInterface>();
            }
        }

        // fake a true invocation
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // return out object default // will need default constructors // also works with void methods
            result = Activator.CreateInstance(binder.ReturnType);

            return true;
        }
    }
    
    public class NullObject
    {
        // change to Main to run.
        public static void Main(string[] args)
        {

            // Default
            //var log = new ConsoleLog();
            //var ba = new BankAccount(null); // how can you pass null if you do not want to log.
            //ba.Deposit(100);

            // Static NullObject
            //var cb = new ContainerBuilder();
            //cb.RegisterType<BankAccount>();
            //cb.RegisterType<NullLog>().As<ILog>();

            //using (var c = cb.Build())
            //{
            //    var ba = c.Resolve<BankAccount>();
            //    ba.Deposit(100);
            //    Console.WriteLine(ba);
            //}

            // Dynamic NullObject // performance hit and not great for production code.
            var log = Null<ILog>.Instance;
            // log. is completely legitimate log intellisense shows info and warn methods.
            var ba = new BankAccount(log);
            ba.Deposit(100);
            Console.WriteLine(ba);
        }
    }
}
