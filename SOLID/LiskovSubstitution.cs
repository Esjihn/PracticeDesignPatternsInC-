using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SOLID
{
    public class Rectangle
    {
        // add virtual to fix
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    // Square is-a rectangle
    public class Square : Rectangle
    {
        // substitute override for new to fix
        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
    }

    public class LiskovSubstitution
    {
        public static int Area(Rectangle r)
        {
            return r.Width * r.Height;
        }

        // change to main to run.
        public static void none(string[] args)
        {
            Rectangle rc = new Rectangle(2,3);
            Console.WriteLine($"{rc} has area {Area(rc)}");
            
            //Rectangle sq = new Square(); violates the liskov substitution principle if this breaks functionality
            // should be able to "upcast" to your base type and preserve functionality. The explicit issue here is that only
            // the width is getting changed. 
            
            // with virtual and override instead of normal fields and "new" setters even with a rectangle reference and only
            // width set the functionality is preserved for square. 
            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}
