using System;
using System.Collections.Generic;
using System.Text;

namespace Decorators
{
    // Composable? Can you have nested Decorators. Yes.

    public interface IShape2
    {
        string AsString();
    }

    public class Circle2 : IShape
    {
        private float radius;

        public Circle2(float radius)
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

    public class Square2 : IShape
    {
        public float side;

        public Square2(float side)
        {
            this.side = side;
        }

        public string AsString()
        {
            return $"A square with side {side}";
        }
    }

    public class ColoredShape2 : IShape
    {
        public IShape shape;
        public string color;

        public ColoredShape2(string color, IShape shape)
        {
            this.color = color ?? throw new ArgumentNullException(nameof(color));
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
        }

        public string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape2 : IShape
    {
        private IShape shape;
        private float transparency;

        public TransparentShape2(IShape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
            this.transparency = transparency;
        }

        public string AsString()
        {
            return $"{shape.AsString()} has {transparency * 100.00}% transparency";
        }
    }

    public class StaticDecoratorCompositions
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            
        }
    }
}
