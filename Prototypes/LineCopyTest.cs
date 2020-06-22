using System;
using System.Collections.Generic;
using System.Text;

namespace Prototypes
{
    // Deep copy example using explicit copy generic interface
    // That uses Copy Constructors for a successful deep copy.

    public interface IPrototype2<T>
    {
        T DeepCopy();
    }

    public class Point : IPrototype2<Point>
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Copy Constructor
        public Point DeepCopy()
        {
            return new Point(X,Y);
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }

    public class Line : IPrototype2<Line>
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
        }

        // Copy Constructor
        public Line DeepCopy()
        {
            return new Line(Start, End);
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        }
    }

    public class LineCopyTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var line = new Line(new Point(1, 2), new Point(2, 3));
            var altLine = line.DeepCopy();
            altLine.End = new Point(4, 5);
            
            Console.WriteLine(line);
            Console.WriteLine(altLine);
            Console.WriteLine(line);
        }
    }
}
