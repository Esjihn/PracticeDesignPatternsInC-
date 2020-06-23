using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Composites
{
    public interface IValueContainer
    {
        IEnumerable<int> Value { get; set; }
    }

    public class SingleValue : IValueContainer
    {
        public SingleValue(IEnumerable<int> value)
        {
            Value = value;
        }
        
        public IEnumerable<int> Value { get; set; }
    }

    public class ManyValues : List<int>, IValueContainer
    {
        public ManyValues(List<int> values)
        {
            this.Value = values;
        }

        public IEnumerable<int> Value { get; set; }
    }

    public static class ExtensionMethods2
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (IValueContainer ivc in containers)
            {
                foreach (int i in ivc.Value)
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
            List<int> intList = new List<int>
            {
                1,2,3,4,5,6,7
            };

            List<int> intList2 = new List<int>
            {
                1,2,3,4,5,6,7
            };

            List<IValueContainer> list = new List<IValueContainer>();
            IValueContainer sv = new SingleValue(intList2);
            IValueContainer mv = new ManyValues(intList);
            list.Add(sv);
            list.Add(mv);

            Console.WriteLine(list.Sum());

        }
    }
}
