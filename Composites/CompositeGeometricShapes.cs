using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composites
{
    // Objects composing of other objects of its own type.
    public class GraphicObject
    {
        private string _name = "Group";

        public virtual string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Color;
        private Lazy<List<GraphicObject>> children 
            = new Lazy<List<GraphicObject>>();

        public List<GraphicObject> Children
        {
            get { return children.Value; }
        }

        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color}")
                .AppendLine(Name);

            foreach (GraphicObject child in Children)
            {
                child.Print(sb, depth + 1);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }

    public class Circle : GraphicObject
    {
        public override string Name
        {
            get { return "Circle"; }
        }
    }

    public class Square : GraphicObject
    {
        public override string Name
        {
            get { return "Square"; }
        }
    }

    public class CompositeGeometricShapes
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var drawing = new GraphicObject {Name = "My Drawing"};
            drawing.Children.Add(new Square {Color = "Red"});
            drawing.Children.Add(new Circle {Color = "Yellow"});

            var group = new GraphicObject();
            group.Children.Add(new Circle {Color = "Blue"});
            group.Children.Add(new Square {Color = "Blue"});
            drawing.Children.Add(group);

            Console.WriteLine(drawing);
        }
    }
}
