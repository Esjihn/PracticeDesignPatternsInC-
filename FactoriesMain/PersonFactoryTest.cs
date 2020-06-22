using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        // Factory Singleton access
        public static PersonFactory Factory
        {
            get{ return new PersonFactory();}
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }

        // inner Factory
        public class PersonFactory
        {
            // default int is zero. 
            public static int Counter;
            // Factory Method
            public Person CreatePerson(string name)
            {
                return new Person(Counter++, name);
            }
        }
    }

    public class PersonFactoryTest
    {
        public static void Main(string[] args)
        {
            var c = Person.Factory.CreatePerson("Craig");
            var m = Person.Factory.CreatePerson("Matthew");
            var y = Person.Factory.CreatePerson("Yuyu");
            var s = Person.Factory.CreatePerson("Sonya");

            var newLine = Environment.NewLine;
            Console.WriteLine(c + newLine + m + newLine + y + newLine + s + newLine);
        }
    }
}
