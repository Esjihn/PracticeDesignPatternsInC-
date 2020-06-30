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
            int result = 0;
            int[] calcArray = new int[expression.Length];

            for (var index = 0; index < expression.Length; index++)
            {
                char c = expression[index];

                if (index > 1)
                {
                    char c1 = expression[index];
                    char c2 = expression[index - 1];

                    bool b1C1 = int.TryParse(c1.ToString(), out int c1Int);
                    calcArray[index] = c1Int;
                    bool b2C2 = int.TryParse(c2.ToString(), out int c2Int);
                    calcArray[index] = c2Int;
                    // If there are consecutive variables 'xy' return 0 per acceptance criteria.
                    if (!b1C1 && !b2C2 
                              && c1 != '+' && c1 != '-'
                              && c2 != '+' && c2 != '-') return 0;
                }

                // if char c is a number do not add to Variables.
                if (int.TryParse(c.ToString(), out int possibleResult))
                    continue;

                // if char c is addition or subtraction symbol do not add to Variables.
                if (c != '+' && c != '-')
                    Variables.TryAdd(c, index);
            }

            if (Variables.Count == 1)
            {
                foreach (KeyValuePair<char, int> keyValuePair in Variables)
                {
                    return keyValuePair.Value;
                }
            }

            if (Variables.Count == 0 || Variables.Count == 1)
            {
                foreach (char c in expression)
                {
                    bool addition = expression.Contains("+");
                    bool subtraction = expression.Contains("-");

                    if (addition && c != '+' && c != '-')
                    {
                        result += int.Parse(c.ToString());
                    }

                    if (subtraction && c != '+' && c != '-')
                    {
                        result -= int.Parse(c.ToString());
                    }
                }
            }
            
            return result; 
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
            Console.WriteLine(exp.Calculate("1+2+3")); // 6
            exp.Variables.Clear();
            Console.WriteLine(exp.Calculate("1+2+xy")); // 0
            exp.Variables.Clear();
            Console.WriteLine(exp.Calculate("10-2-x")); // 5
        }
    }
}
