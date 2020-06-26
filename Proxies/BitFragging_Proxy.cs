using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Proxies
{
    public enum Op : byte
    {
        [Description("*")]
        Mul = 0,
        [Description("/")]
        Div = 1,
        [Description("+")]
        Add = 2,
        [Description("-")]
        Sub = 3
    }

    // Dictionary Op -> name;
    public static class OpImpl
    {
        private static readonly Dictionary<Op, char> opNames
            = new Dictionary<Op, char>();

        static OpImpl()
        {
            var type = typeof(Op);
            foreach (Op op in Enum.GetValues(type))
            {
                MemberInfo[] memInfo = type.GetMember(op.ToString());
                if (memInfo.Length > 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(
                        typeof(DescriptionAttribute), false);

                    if (attrs.Length > 0)
                    {
                        opNames[op] = ((DescriptionAttribute) attrs[0]).Description[0];
                    }
                }
            }
        }
        
        // notice the data types!
        private static readonly Dictionary<Op, Func<double, double, double>> opImpl =
            new Dictionary<Op, Func<double, double, double>>()
            {
                [Op.Mul] = ((x, y) => x * y),
                [Op.Div] = ((x, y) => x / y),
                [Op.Add] = ((x, y) => x + y),
                [Op.Sub] = ((x, y) => x - y),
            };

        public static double Call(this Op op, int x, int y)
        {
            return opImpl[op](x, y);
        }

        public static char Name(this Op op)
        {
            return opNames[op];
        }
    }

    public class Problem
    {
        // 1 3 5 7
        // Op.Mul, Op.Div, Op.Add, Op.Sub
        private readonly List<int> numbers;
        private readonly List<Op> ops;

        public Problem(IEnumerable<int> numbers, IEnumerable<Op> ops)
        {
            this.numbers = new List<int>(numbers);
            this.ops = new List<Op>(ops);
        }

        public int Eval()
        {
            var opGroups = new[]
            {
                new[] {Op.Mul, Op.Div},
                new[] {Op.Add, Op.Sub}
            };

            // typically terrible but not bad for bitwise operations
            startAgain:

            foreach (Op[] group in opGroups)
            {
                for (int idx = 0; idx < ops.Count; ++idx)
                {
                    if (group.Contains(ops[idx]))
                    {
                        var op = ops[idx];
                        double result = op.Call(numbers[idx], numbers[idx + 1]);

                        if (result != (int) result)
                        {
                            return int.MinValue;
                        }

                        // 1 3 5 7
                        // + * +
                        // destroy *
                        // 1 15 7
                        // destroy +
                        // 16 + 7
                        // destroy +
                        // folds into single value 23 other elements are empty

                        numbers[idx] = (int) result;
                        numbers.RemoveAt(idx + 1);
                        ops.RemoveAt(idx);
                        if (numbers.Count == 1) return numbers[0];

                        goto startAgain;
                    }
                }
            }

            return numbers[0];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < ops.Count; ++i)
            {
                sb.Append(numbers[i]);
                sb.Append(ops[i].Name());
            }

            sb.Append(numbers[ops.Count]);
            return sb.ToString();
        }
    }

    // the proxy construct.
    public class TwoBitSet
    {
        // 64 bits ---> 32 values;
        private readonly ulong data;

        public TwoBitSet(ulong data)
        {
            this.data = data;
        }

        // 10 10 01 01 10 01 01 01 01
        public byte this[int index]
        {
            get
            {
                // 00 10 01 01
                
                // multiplied by 2 to get 4th element
                var shift = index << 1;

                ulong mask = (0b11U << shift); // 00 11 00 00

                // 00 10 00 00 >> shift (by 4 elements)
                // 00 00 00 00 00 00 00 00 00 00 10 cast to byte gets you '10'
                return (byte) ((data & mask) >> shift);
            }
        }
    }

    public class BitFragging_Proxy
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            // bool masquerading as a single bit when its not. 
            // uint for single number but then could fragment out to 64 bools which is a spacing saving technique.

            //                          fragment
            //           0101011101    00 10 01 01
            // booleans  FTFTFTTTFT    
            // BitVector32 better space saving.

            // 1-3-5+7 = 0
            // 0 1 2 3 ... 10

            // * / + - main operators (PE)MDAS
            // 00 01 10 11

            // * * *
            // 000000000000000000 00 00
            // 000000000000000000 00 01
            // 000000000000000000 00 10
            // 000000000000000000 00 11
            // 000000000000000000 01 00
            //----------------------------------


            var numbers = new[] {1, 3, 5, 7};
            int numberOfOps = numbers.Length - 1;
            // use of proxy TwoBitSet
            for (int result = 0; result <= 10; ++result)
            {                                                       // increment and take two bit chunks
                for (ulong key = 0UL; key < (1UL << 2*numberOfOps); ++key)
                {
                    var tbs = new TwoBitSet(key);
                    var ops = Enumerable.Range(0, numberOfOps)
                        .Select(i => tbs[i]).Cast<Op>().ToArray();
                    var problem = new Problem(numbers, ops);
                    if (problem.Eval() == result)
                    {
                        Console.WriteLine($"{new Problem(numbers, ops)} = {result}");
                        break;
                    }
                }
            }

            // We cannot find the solution for 3 due to our data set but we do find the rest from 0-10
            // console output.
            // 1 - 3 - 5 + 7 = 0
            // 1 * 3 + 5 - 7 = 1
            // 1 + 3 + 5 - 7 = 2
            // 1 * 3 - 5 + 7 = 5
            // 1 + 3 - 5 + 7 = 6
            // 1 * 3 * 5 - 7 = 8
            // 1 + 3 * 5 - 7 = 9
            // 1 - 3 + 5 + 7 = 10
        }
    }
}
