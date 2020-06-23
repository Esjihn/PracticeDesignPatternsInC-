using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Bridges
{
    // Connect different abstractions together.
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    // Bridge does not put limitation that the shape can be either raster or vector form. 
    // Dont let Shape decide the different ways it can be drawn. 
    public abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }


    public class Circle : Shape
    {
        private float radius;
        
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    public class Bridges
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            // bridge
            //IRenderer renderer = new RasterRenderer();
            
            //VectorRenderer renderer = new VectorRenderer();
            //IRenderer renderer = new VectorRenderer();

            // dependency injection can help avoid inserting the renderer object. 
            //var circle = new Circle(renderer, 5);
            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();

            // Dependency injection produces same result without manually inserting renderer object.
            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>()
                .SingleInstance(); // single(ton) instance.
            cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(),
                // positional lets you specify the type and then supply the type when the container is built.
                p.Positional<float>(0)));

            using (var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(
                    new PositionalParameter(0, 5.0f)); // no auto conversion be precise 5 wont work.\

                circle.Draw();
                circle.Resize(2.0f);
                circle.Draw();
            }

            // Takeaway -
            // a way of connect part of a system (Circle as a domain object) to the different implementation
            // of the Renderer objects (vector and raster) and doing so non intrusively. Instead of giving
            // the domain object Circle methods for drawing and rendering raster and vector form you give it an
            // interface IRenderer which makes a bridge between the domain object and the way the object should b
            // processed. 
        }
    }
}
