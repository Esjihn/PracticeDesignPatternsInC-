using System;
using System.Collections.Generic;
using System.Text;

namespace Decorators
{
    // Composable? Can you have nested Decorators. Yes.

    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float radius;

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }

        public string AsString()
        {
            return $"A circle with radius {radius}";
        }
    }

    public class Square : IShape
    {
        public float side;
        
        public Square(float side)
        {
            this.side = side;
        }

        public string AsString()
        {
            return $"A square with side {side}";
        }
    }

    public class ColoredShape : IShape
    {
        public IShape shape;
        public string color;

        public ColoredShape(string color, IShape shape)
        {
            this.color = color ?? throw new ArgumentNullException(nameof(color));
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
        }

        public string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape : IShape
    {
        private IShape shape;
        private float transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
            this.transparency = transparency;
        }

        public string AsString()
        {
            return $"{shape.AsString()} has {transparency * 100.00}% transparency";
        }
    }

    public class DynamicDecoratorCompositions
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var square = new Square(1.23f);
            Console.WriteLine(square.AsString());

            var redSquare = new ColoredShape("red", square);
            Console.WriteLine(redSquare.AsString());

            // Nested composed decorators, works at runtime.
            var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            Console.WriteLine(redHalfTransparentSquare.AsString());
        }
    }
}
