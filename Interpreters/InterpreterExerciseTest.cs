using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreters
{
    // You are asked to write an expression processor for simple numeric expressions
    // with the following constraints.

    // 1) Expressions use integral values (i.e. '12'), single-letter defined variable defined
    //    in Variables, as well as + and - operators only.
    // 2) There is no need to support braces or any other operations.
    // 3) If a variable is not found in Variables (or if we encounter a variable with > 1 letter, i.e. ab), the evaluator
    //    returns 0 (zero)
    // 4) In case of any parsing failure, evaluator returns 0;

    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public int Calculate(string expression)
        {
            // todo
            for (var index = 0; index < expression.Length; index++)
            {
                char c = expression[index];
                Variables.Add(c, index);
            }




            return 0; // change to return result;
        }
    }
    
    public class InterpreterExerciseTest
    {
        // change from Main to run.
        public static void Main(string[] args)
        {
            // Example
            // Calculate("1+2+3") should return 6
            // Calculate("1+2+xy") should return 0
            // Calculate("10-2-x") when x=3 is in Variables should return 5

            ExpressionProcessor exp = new ExpressionProcessor();
            exp.Calculate("1+2+3");

        }
    }
}
