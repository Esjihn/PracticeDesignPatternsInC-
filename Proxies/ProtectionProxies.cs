using System;
using System.Collections.Generic;
using System.Text;

namespace Proxies
{
    // Similar to decorator but you do not implement delegating members
    // You add new functionality to existing methods.

    // Checks if you have right to check a particular value or method.
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }

    public class Driver
    {
        public int Age { get; set; }

        public Driver(int age)
        {
            Age = age;
        }
    }

    public class CarProxy : ICar
    {
        private Driver driver;
        private Car car = new Car();

        public CarProxy(Driver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void Drive()
        {
            if (driver.Age >= 16)
            {
                car.Drive();
            }
            else
            {
                Console.WriteLine("Too young.");
            }
        }
    }

    public class ProtectionProxies
    {
        // change to Main to run
        public static void none(string[] args)
        {
            ICar car = new CarProxy(new Driver(22));
            car.Drive();
        }
    }
}
