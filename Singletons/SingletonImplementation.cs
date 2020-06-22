using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq;
using NUnit.Framework;

namespace Singletons
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }
    
    public class SingletonDatabase : IDatabase
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
                    Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,
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
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance
        {
            get { return instance.Value; }
        }
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (string name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
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
    //        var db = SingletonDatabase.Instance;
    //        var db2 = SingletonDatabase.Instance;

    //        // Compares they reference the same object. 
    //        Assert.That(db, Is.SameAs(db2));
    //        Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
    //    }

    //    // Consequence testing live database that costs alot of resources. You want to fake database instead of the real database.
    //    // You cannot because you have a hardcoded reference to the database as a singleton.
    //    [Test]
    //    public void SingletonTotalPopulation()
    //    {
    //        var rf = new SingletonRecordFinder();
    //        var names = new[] {"Seoul", "Mexico City"};
    //        int tp = rf.GetTotalPopulation(names);
    //        Assert.That(tp, Is.EqualTo(17500000 + 17400000));
    //    }
    //}
    
    public class SingletonImplementation
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
    }
}
