using System;
using System.Collections.Generic;
using System.Text;

namespace Decorators
{
    // Static Composition isnt good enough in c#. Stay away from this implementation of Decorator pattern.

    public abstract class Shape2
    {
        public abstract string AsString();
    }

    public class Circle2 : Shape2
    {
        private float radius;

        public Circle2() : this(0)
        {
        }

        public Circle2(float radius)
        {
            this.radius = radius;
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }

        public override string AsString()
        {
            return $"A circle with radius {radius}";
        }
    }

    public class Square2 : Shape2
    {
        public float side;

        public Square2(float side)
        {
            this.side = side;
        }

        public Square2() : this(0.0f)
        {
            
        }

        public override string AsString()
        {
            return $"A square with side {side}";
        }
    }

    public class ColoredShape2 : Shape2
    {
        public IShape shape;
        public string color;

        public ColoredShape2(string color, IShape shape)
        {
            this.color = color ?? throw new ArgumentNullException(nameof(color));
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
        }

        public ColoredShape2()
        {
            
        }

        public override string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape2 : Shape2
    {
        private IShape shape;
        private float transparency;

        public TransparentShape2(IShape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
            this.transparency = transparency;
        }

        public override string AsString()
        {
            return $"{shape.AsString()} has {transparency * 100.00}% transparency";
        }
    }

    // public class ColoredShape<T> : T // curiously recurring template pattern CRTP C# cannot do this.
    // cannot use interfaces for the workaround. instead use abstract class and aggregation
    public class ColoredShape<T> : Shape2 where T : Shape2, new()
    {
        private string color;
        T shape = new T(); // new() required

        public ColoredShape() : this("black") // default
        {
        }

        public ColoredShape(string color)
        {
            this.color = color ?? throw new ArgumentNullException(nameof(color));
        }


        public override string AsString()
        {
            return $"{shape.AsString()} has the color {color}";
        }
    }

    public class TransparentShape<T> : Shape2 where T : Shape2, new()
    {
        private float transparency;
        T shape = new T(); // new() required

        public TransparentShape() : this(0) // default
        {
        }

        public TransparentShape(float transparency)
        {
            this.transparency = transparency;
        }

        public override string AsString()
        {
            return $"{shape.AsString()} has {transparency * 100.0f}% transparency";
        }
    }

    public class StaticDecoratorCompositions
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var redSquare = new ColoredShape<Square2>();
            Console.WriteLine(redSquare.AsString());

            // use this because constructor forwarding doesn't work in c#
            // quasi static composition using aggregation many nested objects carry instantiations of other objects.
            var circle = new TransparentShape<ColoredShape<Circle2>>(0.4f);
            Console.WriteLine(circle.AsString());
        }
    }
}
