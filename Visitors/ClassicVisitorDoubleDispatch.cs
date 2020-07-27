using System;
using System.Collections.Generic;
using System.Text;

namespace Visitors
{
    // This type of visitor is really hard to modify later if you have different
    // elements added to existing hierarchy.
    public interface IExpressionVisitor
    {
        void Visit(DoubleExpression3 de);
        void Visit(AdditionExpression3 ae);
    }

    public abstract class Expression3
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public class DoubleExpression3 : Expression3
    {
        public double Value;

        public DoubleExpression3(double value)
        {
            Value = value;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            // double dispatch (expression at run time, since we secure type in interface)
            visitor.Visit(this);
        }
    }

    public class AdditionExpression3 : Expression3
    {
        public Expression3 Left;
        public Expression3 Right;

        public AdditionExpression3(Expression3 left, Expression3 right)
        {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            // double dispatch (expression at run time, since we secure type in interface)
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter2 : IExpressionVisitor
    {
        StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression3 de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression3 ae)
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

    public class ExpressionCalculator : IExpressionVisitor
    {
        public double Result;

        public void Visit(DoubleExpression3 de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression3 ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }
    }

    public class ClassicVisitorDoubleDispatch
    {
        // change to Main to run.
        public static void none()
        {
            var e = new AdditionExpression3(
              left: new DoubleExpression3(1),
              right: new AdditionExpression3(
                left: new DoubleExpression3(2),
                right: new DoubleExpression3(3)));
            var ep = new ExpressionPrinter2();
            ep.Visit(e);
            Console.WriteLine(ep);

            // Recursive calculation
            var calc = new ExpressionCalculator();
            calc.Visit(e);
            Console.WriteLine($"{ep} = {calc.Result}");
        }
    }
}

