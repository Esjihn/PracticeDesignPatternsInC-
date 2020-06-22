using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq;

namespace Singletons
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing database");

            capitals = File.ReadAllLines("capitals.txt")
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
    
    public class SingletonImplementation
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
    }
}
