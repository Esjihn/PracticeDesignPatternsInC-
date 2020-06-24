using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweights
{
    // String interning -In computer science, string interning is a method
    // of storing only one copy of each distinct string value, which must be immutable.
    // Interning strings makes some string processing tasks more time- or space-efficient
    // at the cost of requiring more time when the string is created or interned.

    // JetBrains.DotMemoryUnit free unit testing framework for testing memory utilization.

    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }
    }

    public class FlyweightsManualStringInterning
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            
        }
    }
}
