using System;
using System.Collections.Generic;
using System.Text;

namespace Visitors
{
    public interface IExpressionVisitor2
    {
        void Visit(DoubleExpression5 de);
        void Visit(AdditionExpression5 ae);
    }

    public abstract class Expression5
    {
        public abstract void Accept(IExpressionVisitor2 visitor);
    }

    public class DoubleExpression5 : Expression5
    {
        public double Value;

        public DoubleExpression5(double value)
        {
            Value = value;
        }

        public override void Accept(IExpressionVisitor2 visitor)
        {
            // double dispatch (expression at run time, since we secure type in interface)
            visitor.Visit(this);
        }
    }

    public class AdditionExpression5 : Expression5
    {
        public Expression5 Left;
        public Expression5 Right;

        public AdditionExpression5(Expression5 left, Expression5 right)
        {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));
        }

        public override void Accept(IExpressionVisitor2 visitor)
        {
            // double dispatch (expression at run time, since we secure type in interface)
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter5 : IExpressionVisitor2
    {
        StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression5 de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression5 ae)
        {
            sb.Append("(");
            ae.Left.Accept(this);
            sb.Append("+");
            ae.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class ExpressionCalculator2 : IExpressionVisitor2
    {
        public double Result;

        public void Visit(DoubleExpression5 de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression5 ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }
    }

    public class AcyclicVisitor
    {
        // change to Main to run.
        public static void Main()
        {
            var e = new AdditionExpression5(
              left: new DoubleExpression5(1),
              right: new AdditionExpression5(
                left: new DoubleExpression5(2),
                right: new DoubleExpression5(3)));
            var ep = new ExpressionPrinter5();
            ep.Visit(e);
            Console.WriteLine(ep);
        }
    }
}
