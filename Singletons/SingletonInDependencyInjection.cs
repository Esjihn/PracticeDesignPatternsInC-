using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using MoreLinq;
using NUnit.Framework;

namespace Singletons
{
    public class SingletonInDependencyInjection
    {
        public interface IDatabase
        {
            int GetPopulation(string name);
        }

        public class SingletonDatabase : Singletons.IDatabase
        {
            private Dictionary<string, int> capitals;
            private static int instanceCount; // 0 by default;
            public static int Count
            {
                get { return instanceCount; }
            }


            private SingletonDatabase()
            {
                instanceCount++;
                Console.WriteLine("Initializing database");

                capitals = File.ReadAllLines(
                        Path.Combine(new FileInfo(typeof(Singletons.IDatabase).Assembly.Location).DirectoryName,
                            "capitals.txt"))
                    .Batch(2)
                    .ToDictionary(
                        list => list.ElementAt(0).Trim(),
                        list => int.Parse(list.ElementAt(1)));
            }

            public int GetPopulation(string name)
            {
                return capitals[name];
            }

            // this prevents the construction of a than one instance of a SingletonDatabase
            // private static SingletonDatabase instance = new SingletonDatabase();

            // Lazy prevents you from paying the price of all data in capitals when you may only need a specific item. 
            // this is lazy and thread safe.
            private static Lazy<SingletonInDependencyInjection.SingletonDatabase> instance 
                = new Lazy<SingletonInDependencyInjection.SingletonDatabase>(() => new SingletonInDependencyInjection.SingletonDatabase());

            public static SingletonInDependencyInjection.SingletonDatabase Instance
            {
                get { return instance.Value; }
            }
        }

        // Implemented using a Dependency injection framework.
        public class OrdinaryDatabase : IDatabase
        {
            private Dictionary<string, int> capitals;
            
            private OrdinaryDatabase()
            {
                Console.WriteLine("Initializing database");

                capitals = File.ReadAllLines(
                        Path.Combine(new FileInfo(typeof(SingletonInDependencyInjection.IDatabase).Assembly.Location).DirectoryName,
                            "capitals.txt"))
                    .Batch(2)
                    .ToDictionary(
                        list => list.ElementAt(0).Trim(),
                        list => int.Parse(list.ElementAt(1)));
            }

            public int GetPopulation(string name)
            {
                return capitals[name];
            }
        }
        
        public class DummyDatabase : IDatabase
        {
            public int GetPopulation(string name)
            {
                return new Dictionary<string, int>
                {
                    ["alpha"] = 1,
                    ["beta"] = 2,
                    ["gamma"] = 3
                }[name];
            }
        }

        public class SingletonRecordFinder
        {
            public int GetTotalPopulation(IEnumerable<string> names)
            {
                int result = 0;
                foreach (string name in names)
                {
                    result += Singletons.SingletonDatabase.Instance.GetPopulation(name);
                }

                return result;
            }
        }

        public class ConfigurableRecordFinder
        {
            private IDatabase database;

            // Constructor injection, easily testable with dummy database. 
            public ConfigurableRecordFinder(IDatabase database)
            {
                // C# 7.0
                this.database = database ?? throw new ArgumentNullException(nameof(database));
            }

            public int GetTotalPopulation(IEnumerable<string> names)
            {
                int result = 0;
                foreach (string name in names)
                {
                    result += database.GetPopulation(name);
                }
                
                return result;
            }
        }

        // testing singleton
        //[TestFixture]
        //public class SingletonTests
        //{
        //    [Test]
        //    public void IsSingletonTest()
        //    {
        //        var db = Singletons.SingletonDatabase.Instance;
        //        var db2 = Singletons.SingletonDatabase.Instance;

        //        // Compares they reference the same object. 
        //        Assert.That(db, Is.SameAs(db2));
        //        Assert.That(Singletons.SingletonDatabase.Count, Is.EqualTo(1));
        //    }

        //    // Consequence testing live database that costs alot of resources. You want to fake database instead of the real database.
        //    // You cannot because you have a hardcoded reference to the database as a singleton.
        //    [Test]
        //    public void SingletonTotalPopulation()
        //    {
        //        var rf = new Singletons.SingletonRecordFinder();
        //        var names = new[] { "Seoul", "Mexico City" };
        //        int tp = rf.GetTotalPopulation(names);
        //        Assert.That(tp, Is.EqualTo(17500000 + 17400000));
        //    }

        //    // Dummy database test
        //    [Test]
        //    public void ConfigurablePopulationTest()
        //    {
        //        var rf = new ConfigurableRecordFinder(new DummyDatabase());
        //        var names = new[] {"alpha", "gamma"};
        //        int tp = rf.GetTotalPopulation(names);
        //        Assert.That(tp, Is.EqualTo(4));
        //    }

        //    [Test]
        //    public void DIPopulationTest()
        //    {
        //        var cb = new ContainerBuilder();

        //        // instead of making the component a singleton i.e. singleton database.
        //        // we can instead register an instance as a singleton. 
        //        // <OrdinaryDatabase> represents a single point where the type can be changed
        //        // to other IDatabase classes for testing. i.e. Dummy Database. 
        //        cb.RegisterType<OrdinaryDatabase>()
        //            // as a single(ton) instance ton. 
        //            .As<SingletonInDependencyInjection.IDatabase>()
        //            .SingleInstance();
        //        cb.RegisterType<ConfigurableRecordFinder>();

        //        using (var c = cb.Build())
        //        {
        //            var rf = c.Resolve<ConfigurableRecordFinder>();
        //        }
        //    }
        //}

        // change to Main to run. 
        public static void none(string[] args)
        {
            
        }
    }
}
