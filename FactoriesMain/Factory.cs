using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    
    // old Point class from FactoryMethod tutorial
    public class Point2
    {
        // factory method
        private double x, y;

        private Point2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        // if you need to instantiate new via property for every call then this can be
        // a good alternative.
        public static Point2 Origin
        {
            get { return new Point2(0, 0); }
        }

        public static Point2 Origin2 = new Point2(0,0); // better than above

        // point factory method could be static but you can also create a property
        // for access like property below.
        public static PointFactory Factory
        {
            get
            {
                return new PointFactory();
            }
        }

        // Constraint to regular factories are that Constructors need to be public. 
        // internal would allow its use without being exposed but not desirable.
        // So we can add the PointFactory as an Internal (inner) Factory)
        public class PointFactory
        {
            public Point2 NewCartesianPoint(double x, double y)
            {
                return new Point2(x, y);
            }

            public Point2 NewPolarPoint(double rho, double theta)
            {
                return new Point2(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    public class Factory
    {
        // change to Main to run
        public static void none(string[] args)
        {
            // dont have rho and theta under old point.
            // var p = new Point2();

            var point = Point2.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}
