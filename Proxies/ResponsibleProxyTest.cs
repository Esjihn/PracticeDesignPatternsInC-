using System;
using System.Collections.Generic;
using System.Text;

namespace Proxies
{
    public class Person2
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }

        public class ResponsiblePerson
        {
            private readonly Person2 _person;

            public ResponsiblePerson(Person2 person)
            {
                _person = person;
            }

            public string Drink()
            {
                if (Age < 18) return "too young to drink";
                return _person.Drink();
            }

            public string Drive()
            {
                if (Age < 16) return "too young to drive";
                return _person.Drive();
            }

            public string DrinkAndDrive()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{_person.DrinkAndDrive()} and now dead");
                return sb.ToString();
            }

            public int Age
            {
                get
                {
                    return _person.Age;
                }
                set
                {
                    _person.Age = value;
                }
            }
        }
    }

    public class ResponsibleProxyTest
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            var rp = new Person2.ResponsiblePerson(new Person2{Age = 18});
            Console.WriteLine(rp.Drink());
            Console.WriteLine(rp.Drive());
            Console.WriteLine(rp.DrinkAndDrive()); // We never do this.
        }
    }
}
