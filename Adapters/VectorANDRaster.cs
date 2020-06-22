using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MoreLinq.Extensions;

namespace Adapters
{
    public class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // 1. equality members are great for hashcodes.
        protected bool Equals(Point other)
        {
            return x == other.x && y == other.y;
        }

        // 2
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point) obj);
        }

        // 3
        public override int GetHashCode()
        {
            unchecked
            {
                return x * 397 ^ y;
            }

            //return HashCode.Combine(x, y);
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point end, Point start)
        {
            End = end ?? throw new ArgumentNullException(nameof(end));
            Start = start ?? throw new ArgumentNullException(nameof(start));
        }

        protected bool Equals(Line other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Line) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
            }
        }
    }

    public class VectorObject : Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            this.Add(new Line(new Point(x, y), new Point(x + width, y)));
            this.Add(new Line(new Point(x + width, y), new Point(x + width, y = height)));
            this.Add(new Line(new Point(x, y), new Point(x, y = height)));
            this.Add(new Line(new Point(x, y + height), new Point(x + width, y = height)));
        }
    }

    // Adapter to build a Line from a set of points. Since point is the exposed interface. 
    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int count;

        // Introducing caching to prevent duplicated adapter temporary information.
        private static Dictionary<int, List<Point>> cache
            = new Dictionary<int, List<Point>>();

        public LineToPointAdapter(Line line)
        {
            var hash = line.GetHashCode();

            if (cache.ContainsKey(hash)) return;
                
            Console.WriteLine($"{++count}: Generating points for line [{line.Start.x},{line.Start.y}]-[{line.End.x}, {line.End.y}]");

            var points = new List<Point>();

            // margin line
            int left = Math.Min(line.Start.x, line.End.x);
            int right = Math.Max(line.Start.x, line.End.x);
            int top = Math.Min(line.Start.y, line.End.y);
            int bottom = Math.Max(line.Start.y, line.End.y);
            int dx = right - left;
            int dy = line.End.y - line.Start.y;

            // calculate x and y change.
            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    points.Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }

            cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public class VectorANDRaster
    {
        private static readonly List<VectorObject> vectorObjects
            = new List<VectorObject>
            {
                new VectorRectangle(1, 1, 10, 10),
                new VectorRectangle(3, 4, 6, 6)
            };

        public static void DrawPoint(Point p)
        {
            Console.Write(".");
        }

        // change to Main to run.
        public static void none(string[] args)
        {
            // Side effect of adapter is that it generates a lot of temporary information.
            // Here Drawing will occur twice for information that has already been calculated.
            // Stored information should not be redrawn regardless of call amount. 
            Draw();
            Draw();
        }

        private static void Draw()
        {
            foreach (VectorObject vo in vectorObjects)
            {
                foreach (Line line in vo)
                {
                    LineToPointAdapter adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                }
            }
        }
    }
}
