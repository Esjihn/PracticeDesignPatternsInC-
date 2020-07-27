using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Visitors
{
    using DictType = Dictionary<Type, Action<Expression2, StringBuilder>>;

    public abstract class Expression2
    {
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression2 : Expression2
    {
        public double Value;

        public DoubleExpression2(double value)
        {
            this.Value = value;
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append(Value);
        }
    }

    public class AdditionExpression2 : Expression2
    {
        public  Expression2 Left, Right;

        public AdditionExpression2(Expression2 left, Expression2 right)
        {
            this.Left = left ?? throw new ArgumentNullException(nameof(left));
            this.Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            Left.Print(sb);
            sb.Append("+");
            Right.Print(sb);
            sb.Append(")");
        }
    }
    
    // Not classic printer, also violates single responsibility.
    public class ExpressionPrinter
    {
        private static DictType actions = new DictType
        {
            [typeof(DoubleExpression2)] = (e, sb) =>
            {
                var de = (DoubleExpression2) e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExpression2)] = (e, sb) =>
            {
                var ae = (AdditionExpression2) e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };

        public static void Print(Expression2 e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }

        //public static void Print(Expression2 e, StringBuilder sb)
        //{
        //    if (e is DoubleExpression2 de)
        //    {
        //        sb.Append(de.Value);
        //    }
        //    else if (e is AdditionExpression2 ae)
        //    {
        //        sb.Append("(");
        //        Print(ae.Left, sb);
        //        sb.Append("+");
        //        Print(ae.Right, sb);
        //        sb.Append(")");
        //    }
        //}
    }

    public class ReflectionBasedPrintingVisitor
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var e = new AdditionExpression2(new DoubleExpression2(1), new AdditionExpression2(new DoubleExpression2(2), new DoubleExpression2(3)));
            var sb = new StringBuilder();
            //e.Print(sb);
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb);
        }
    }
}
