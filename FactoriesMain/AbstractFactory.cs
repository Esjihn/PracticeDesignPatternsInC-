using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice but I'd prefer it with milk.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is sensational!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in a team bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml," +
                              $" add cream and sugar, and enjoy");

            return new Coffee();
        }
    }

    internal class HotChocolateFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Pour hot chocolate packet into {amount} ml of hot water" +
                              $" and enjoy!");

            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        // Enum violates open/closed principle. 
        //public enum AvailableDrink
        //{
        //    Coffee, Tea
        //}

        //private Dictionary<AvailableDrink, IHotDrinkFactory> factories = 
        //    new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        var type = Type.GetType("Factories." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory");
                
        //        if (type is null)
        //        {
        //            throw new InvalidOperationException();
        //        }

        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(type);

        //        factories.Add(drink, factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return factories[drink].Prepare(amount);
        //}

        private List<Tuple<string, IHotDrinkFactory>> factories
                 = new List<Tuple<string, IHotDrinkFactory>>();


        // Reflection can be used to avoid violating open closed principle.
        public HotDrinkMachine()
        {
            foreach (Type t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t)
                    && !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks:");
            for (var index = 0; index < factories.Count; index++)
            {
                Tuple<string, IHotDrinkFactory> tuple = factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s = string.Empty;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i)
                    && i >= 0
                    && i < factories.Count)
                {
                    Console.Write("Specify amount: ");
                    s = Console.ReadLine();
                    if (s != null && int.TryParse(s, out int amount)
                                  && amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }

                Console.WriteLine("Incorrect input try again.");
            }
        }
    }

    // The only real use of an Abstract Factory is to give out abstract objects
    // as opposed to concrete objects. i.e. abstract classes or interfaces.
    public class AbstractFactory
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.Consume();
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            //drink.Consume();
        }
    }
}
