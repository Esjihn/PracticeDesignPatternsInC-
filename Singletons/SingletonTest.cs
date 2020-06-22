using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Singletons
{
    public class Person
    {
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

    public class SingletonTest
    {

        public static bool IsSingleton(Func<object> func)
        {
            // todo

        }

        // change to Main to run.
        public static void Main(string[] args)
        {
            
            // pb.actions or static PersonBuilder.actions
            if (IsSingleton())
            {
                Console.WriteLine("Yes, this is a singleton");
            }
            else
            {
                Console.WriteLine("No, this is not a singleton");
            }
        }
    }
}
