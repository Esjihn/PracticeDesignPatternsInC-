using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Bridges
{
    public interface IRenderer2
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer2 : IRenderer2
    {
        public string WhatToRenderAs { get; }

        public VectorRenderer2(string whatToRenderAs)
        {
            WhatToRenderAs = whatToRenderAs;
        }

        public override string ToString()
        {
            return $"{nameof(WhatToRenderAs)}: {WhatToRenderAs} as Vector Render";
        }
    }

    public class RasterRenderer2 : IRenderer2
    {
        public string WhatToRenderAs { get; }

        public RasterRenderer2(string whatToRenderAs)
        {
            WhatToRenderAs = whatToRenderAs;
        }
        public override string ToString()
        {
            return $"{nameof(WhatToRenderAs)}: {WhatToRenderAs} as Raster Render";
        }
    }

    public abstract class Shape2
    {
        protected IRenderer2 renderer;

        public string Name { get; set; }

        protected Shape2(IRenderer2 renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void DecideRender();
    }

    public class Triangle : Shape2
    {
        public Triangle(IRenderer2 renderer) : base(renderer)
        {
        }

        public override void DecideRender()
        {
            Console.WriteLine(renderer);
        }
    }

    public class Square : Shape2
    {
        public Square(IRenderer2 renderer) : base(renderer)
        {
        }

        public override void DecideRender()
        {
            Console.WriteLine(renderer);
        }
    }
    
    public class BridgeRendererTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            IRenderer2 renderSquare = new RasterRenderer2("Square");
            IRenderer2 renderTriangle = new VectorRenderer2("Triangle");

            var square = new Square(renderSquare);

            square.DecideRender();

            var triangle = new Triangle(renderTriangle);
            triangle.DecideRender();
        }
    }
}
