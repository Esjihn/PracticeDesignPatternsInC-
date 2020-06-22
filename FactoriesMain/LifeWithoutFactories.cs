using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public class LifeWithoutFactories
    {
        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        public class Point
        {
            private double x, y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            // Cannot make a point that takes polar coordinates. Not allowed to overload.
            // public Point(double rho, double theta) { }
            // You could create enum (below) but there is no distinction
            // what exactly is a and b and it would need to communicate this to the user.
            // You could also implement other classes like CartesianPoint and PolarPoint
            // but factory pattern is much easier to do.
            public Point(double a, double b, 
                CoordinateSystem system = CoordinateSystem.Cartesian)
            {
                switch (system)
                {
                    case CoordinateSystem.Cartesian:
                        x = a;
                        y = b;
                        break;
                    case CoordinateSystem.Polar:
                        x = a * Math.Cos(b);
                        y = a * Math.Sin(b);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(system), system, null);
                }
            }
        }

        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
