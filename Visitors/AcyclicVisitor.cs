using System;
using System.Collections.Generic;
using System.Text;

namespace Visitors
{
    public interface IVisitor<TVisitable>
    {
        void Visit(TVisitable obj);
    }

    // Degenerate / Marker interface (used for simply indicating that a method type is a visitor
    // Can be attached to any type of visitor
    public interface IVisitor { }

    // 3 -  DoubleExpression
    // (1 + 2) (1+(2+3)) AdditionExpression
    public abstract class Expression5
    {
        public virtual void Accept(IVisitor visitor)
        {
            // verifies that the type of generic visitor passed in is an Expression type visitor
            // checking type before dispatching has performance cost. 
            if (visitor is IVisitor<Expression5> typed)
            {
                typed.Visit(this);
            }
        }
    }

    public class DoubleExpression5 : Expression5
    {
        public double Value;

        public DoubleExpression5(double value)
        {
            Value = value;
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<DoubleExpression5> typed)
            {
                typed.Visit(this);
            }
        }
    }

    public class AdditionExpression5 : Expression5
    {
        public Expression5 Left, Right;

        public AdditionExpression5(Expression5 left, Expression5 right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<AdditionExpression5> typed)
            {
                typed.Visit(this);
            }
        }
    }

    // Visitor
    public class ExpressionPrinter4 : IVisitor,
        IVisitor<Expression5>,
        // still compiles even when specific IVisitors are not implemented.
        //IVisitor<DoubleExpression5>,
        IVisitor<AdditionExpression5>
    {
        private StringBuilder sb = new StringBuilder();

        public void Visit(Expression5 obj)
        {
            // error handling
            // non abstract base type
            // can go here. 
        }

        public void Visit(DoubleExpression5 obj)
        {
            sb.Append(obj.Value);
        }

        public void Visit(AdditionExpression5 obj)
        {
            sb.Append("(");
            obj.Left.Accept(this);
            sb.Append("+");
            obj.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class AcyclicVisitor
    {
        // change to Main to run.
        public static void none()
        {
            var e = new AdditionExpression5(
                left: new DoubleExpression5(1),
                right: new AdditionExpression5(
                    left: new DoubleExpression5(2),
                    right: new DoubleExpression5(3)));
            var ep = new ExpressionPrinter4();
            ep.Visit(e);
            Console.WriteLine(ep.ToString());
        }
    }
}
