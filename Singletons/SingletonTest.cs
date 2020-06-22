using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Singletons
{
    // non singleton test
    
    public class Person
    {
        public static string Name { get; set; }

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        private Func<object> nonStaticFunc = new Func<object>(() => new object());

        public Func<object> NonStaticFunc()
        {
            return nonStaticFunc;
        }

        private static Func<object> getFunc = new Func<object>(() => GetFunc());

        public static object GetFunc()
        {
            return getFunc;
        }
    }

    public class SingletonTest
    {

        public static bool IsSingleton(Func<object> func)
        {
            // use this to test singleton
            //var func2 = new Func<object>(Person.GetFunc);
            
            // use this to test non singleton.
            var func2 = new Func<object>(() => new object());
            
            if (func == func2)
                return true;
         
            return false;
        }

        // change to Main to run.
        public static void Main(string[] args)
        {
            Person p = new Person("Matt");
            
            // use Person.GetFunc as IsSingleton parameter for singleton
            // or create new Person object and then pass p.NonStaticFunc()
            if (IsSingleton(p.NonStaticFunc()))
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
