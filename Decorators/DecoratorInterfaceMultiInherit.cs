using System;
using System.Collections.Generic;
using System.Text;

namespace Decorators
{
    public interface IBird
    {
        void Fly();
        int Weight { get; set; }
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }

        public void Fly()
        {
            Console.WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public interface ILizard
    {
        void Crawl();
        int Weight { get; set; }
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            Console.WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    // public class Dragon : Lizard, Bird not allowed in c#
    public class Dragon : IBird, ILizard
    {
        readonly Bird _bird = new Bird();
        readonly Lizard _lizard = new Lizard();

        public void Fly()
        {
            _bird.Fly();
        }

        private int _weight;

        public int Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                _bird.Weight = value;
                _lizard.Weight = value;
            }
        }


        public void Crawl()
        {
            _lizard.Crawl();
        }
    }

    public class DecoratorInterfaceMultiInherit
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var d = new Dragon();
            d.Fly();
            d.Crawl();
            d.Weight = 123;
        }
    }
}
