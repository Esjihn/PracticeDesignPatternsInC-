using System;
using System.Collections.Generic;
using System.Text;

namespace Visitors
{

    public abstract class Expression4
    {

    }

    public class DoubleExpression4 : Expression4
    {
        public double Value;

        public DoubleExpression4(double value)
        {
            Value = value;
        }
    }

    public class AdditionExpression4 : Expression4
    {
        public Expression4 Left;
        public Expression4 Right;

        public AdditionExpression4(Expression4 left, Expression4 right)
        {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));
        }
    }

    public class ExpressionPrinter3
    {
        public void Print(AdditionExpression4 ae, StringBuilder sb)
        {
            // Dynamic incurs a massive performance hit. 
            // May not be practical in most real world scenarios
            sb.Append("(");
            Print((dynamic)ae.Left, sb);
            sb.Append("+");
            Print((dynamic)ae.Right, sb);
            sb.Append(")");
        }

        public void Print(DoubleExpression4 de, StringBuilder sb)
        {
            sb.Append(de.Value);
        }
    }

    public class DynamicVisitorViaDLR
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            Expression4 e = new AdditionExpression4(
                left: new DoubleExpression4(1),
                right: new AdditionExpression4(
                    left: new DoubleExpression4(2),
                    right: new DoubleExpression4(3)));
            var ep = new ExpressionPrinter3();
            var sb = new StringBuilder();
            ep.Print((dynamic)e, sb);
            Console.WriteLine(sb);

        }
    }
}
