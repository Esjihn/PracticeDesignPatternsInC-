using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composites
{
    public interface IValueContainer
    {
        IEnumerable<int> Values { get; set; }
    }

    public class SingleValue : IValueContainer
    {
        public SingleValue(int value)
        {
            this.Values = new int[] {value};
        }

        public IEnumerable<int> Values { get; set; }
    }

    public class ManyValues : List<int>, IValueContainer
    {
        public ManyValues(List<int> values)
        {
            this.Values = values;
        }

        public IEnumerable<int> Values { get; set; }
    }

    public static class ExtensionMethods2
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (IValueContainer ivc in containers)
            {
                foreach (int i in ivc.Values)
                {
                    result +=  i;
                }

            }

            return result;
        }
    }

    public class CompositeValueContainerTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            List<int> intList2 = new List<int>
            {
                1,2,3,4,5,6,7 // Sum this..
            };

            List<IValueContainer> list = new List<IValueContainer>();
            IValueContainer sv = new SingleValue(1); // ..plus this.
            IValueContainer mv = new ManyValues(intList2);

            list.Add(mv);
            list.Add(sv);

            Console.WriteLine(list.Sum()); // 1 + 1 + 2 + 3 + 4 + 5 + 6 + 7 = 29
        }
    }
}
