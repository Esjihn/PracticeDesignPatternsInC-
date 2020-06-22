using System;
using System.Collections.Generic;
using System.Text;

namespace Singletons
{
    public class Monostate
    {
        // A static class with static members is a terrible idea. 
        // Wont have a constructor and you cant use Dependency injection. 

        // Introduce monostate singleton design pattern.
        // Properties they expose map to static fields but they point to the same object. 

        // Can only be on CEO in the company. 
        public class CEO
        {
            private static string name;
            private static int age;

            public string Name
            {
                get => name;
                set => name = value;
            }

            public int Age
            {
                get => age;
                set => age = value;
            }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
            }
        }

        // Change to Main to run. 
        public static void none(string[] args)
        {
            
            var ceo = new CEO();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new CEO();
            Console.WriteLine(ceo2);
        }
    }
}
