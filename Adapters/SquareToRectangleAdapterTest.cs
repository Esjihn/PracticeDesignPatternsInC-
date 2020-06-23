using System;
using System.Collections.Generic;
using System.Text;

namespace Adapters
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public SquareToRectangleAdapter(Square square)
        {
            Width = square.Side;
            Height = square.Side;
        }
    }

    public class SquareToRectangleAdapterTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            Square square = new Square();
            square.Side = 4;

            var sr = new SquareToRectangleAdapter(square);
            Console.WriteLine(sr.Area());
        }
    }
}
