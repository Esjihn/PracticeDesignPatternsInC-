using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singletons
{
    public class Person
    {
        public string Name { get; set; }
    }

    public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions
        = new List<Func<Person, Person>>();

        public static PersonBuilder Called(string name)
        {
            return Do(p => p.Name = name);
        }

        public PersonBuilder Do(Action<Person> action)
        {
            return AddAction(action);
        }

        public Person Build()
        {
            return actions.Aggregate(new Person(), (p, f) => f(p));
        }

        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });

            return this;
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
