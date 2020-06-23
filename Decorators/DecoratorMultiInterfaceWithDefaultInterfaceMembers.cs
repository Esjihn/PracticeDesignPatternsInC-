using System;
using System.Collections.Generic;
using System.Text;

namespace Decorators
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird2 : ICreature
    {
        void Fly()
        {
            if (Age >= 10)
            {
                Console.WriteLine("I am flying");
            }
        }
    }

    public interface ILizard2 : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
            {
                Console.WriteLine("I am crawling");
            }
        }
    }

    public class Organism {}


    public class Dragon2 : Organism, IBird2, ILizard2
    {
        public int Age { get; set; }

    }

    // inheritance 
    // SmartDragon(Dragon) // dragon wrapper
    // extension methods (less intrusive)
    // c#8 default interface methods (more intrusive)

    public class DecoratorMultiInterfaceWithDefaultInterfaceMembers
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            Dragon2 d = new Dragon2 {Age = 5};

            // ((ILizard2)d).Crawl();

            if(d is IBird2 bird)
                bird.Fly();

            if(d is ILizard2 lizard)
                lizard.Crawl();
        }
    }
}
