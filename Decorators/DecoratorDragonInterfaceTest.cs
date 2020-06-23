using System;
using System.Collections.Generic;
using System.Text;

namespace Decorators
{
    public class Bird3
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard3
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon3 // no need for interfaces
    {
        Bird3 bird3 = new Bird3();
        Lizard3 lizard3 =  new Lizard3();
        private int _age;

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                bird3.Age = value;
                lizard3.Age = value;
            }
        }

        public string Fly()
        {
            return bird3.Fly();
        }

        public string Crawl()
        {
            return lizard3.Crawl();
        }
    }

    public class DecoratorDragonInterfaceTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var dragon = new Dragon3();

            dragon.Age = 12;
            Console.WriteLine(dragon.Crawl());
            Console.WriteLine(dragon.Fly());
        }
    }
}
