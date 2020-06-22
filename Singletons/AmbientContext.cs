using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Singletons
{
    // Not thread safe, each thread would need separate BuildingContexts
    // Alternatively use [ThreadStatic] or ThreadLocal<>
    public sealed class BuildingContext : IDisposable
    {
        public int WallHeight;
        private static Stack<BuildingContext> stack
            = new Stack<BuildingContext>();

        // defacto singleton
        static BuildingContext()
        {
            stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int wallHeight)
        {
            // we make a building context and then stick it inside of the stack above.
            WallHeight = wallHeight;
            stack.Push(this);
        }

        public static BuildingContext Current
        {
            get { return stack.Peek(); }
        }

        public void Dispose()
        {
            // its unlikely that this will ever occur but added for the sake of safety. 
            if (stack.Count > 1)
            {
                stack.Pop();
            }
        }
    }

    public class Building
    {
        public List<Wall> Walls = new List<Wall>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (Wall wall in Walls)
            {
                sb.AppendLine(wall.ToString());
            }
            return sb.ToString();
        }
    }

    public struct Point
    {
        private int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    public class Wall
    {
        public Point Start, End;
        public int Height;

        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight;
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, {nameof(Height)}: {Height}";
        }
    }

    public class AmbientContext
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            var house = new Building();

            // gnd 3000
            using (new BuildingContext(3000))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

                // 1st 3500
                using (new BuildingContext(3500))
                {
                    BuildingContext.Current.WallHeight = 3500;
                    house.Walls.Add(new Wall(new Point(0, 0), new Point(6000, 0)));
                    house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
                }

                // gnd 3000
                house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));
            }

            Console.WriteLine(house);
        }
    }
}
