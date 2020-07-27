using System;
using System.Collections.Generic;
using System.Text;

namespace Visitors
{
    /**
     * You are asked to implement a double-dispatch visitor called ExpressionPrinter for printing
     * different mathematical expressions. The range of expressions covers addition and multiplication
     * - please put round brackets around addition operations (but not multiplication ones)! Also, please
     * avoid any blank spaces in output.
     *
     * Example Input: AdditionExpression(Literal(2), Literal(3)
     * Output (2+3)
     */

    public abstract class ExpressionVisitor
    { 
        public abstract void Visit(Value value);
        public abstract void Visit(AdditionExpression6 ae);
        public abstract void Visit(MultiplicationExpression me);
        
    }

    public abstract class Expression6
    {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression6
    {
        public readonly int TheValue;

        public Value(int value)
        {
            TheValue = value;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

        public override string ToString()
        {
            return TheValue.ToString();
        }
    }

    public class AdditionExpression6 : Expression6
    {
        public readonly Expression6 LHS, RHS;

        public AdditionExpression6(Expression6 lhs, Expression6 rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class MultiplicationExpression : Expression6
    {
        public readonly Expression6 LHS, RHS;

        public MultiplicationExpression(Expression6 lhs, Expression6 rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class ExpressionPrinter5 : ExpressionVisitor
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public override void Visit(Value value)
        {
            _sb.Append(value);
        }

        public override void Visit(AdditionExpression6 ae)
        {
            _sb.Append("(");
            ae.LHS.Accept(this);
            _sb.Append("+");
            ae.RHS.Accept(this);
            _sb.Append(")");
        }

        public override void Visit(MultiplicationExpression me)
        {
            _sb.Append("(");
            me.LHS.Accept(this);
            _sb.Append("*");
            me.RHS.Accept(this);
            _sb.Append(")");
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    public class ExpressionPrintingVisitorTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var simpleAddition = new AdditionExpression6(new Value(2), new Value(3));
            var ep = new ExpressionPrinter5();
            ep.Visit(simpleAddition);
            Console.WriteLine(ep.ToString());

            var simpleMultiplication = new MultiplicationExpression(new Value(7), new Value(7));
            var ep2 = new ExpressionPrinter5();
            ep2.Visit(simpleMultiplication);
            Console.WriteLine(ep2.ToString());
        }
    }
}
