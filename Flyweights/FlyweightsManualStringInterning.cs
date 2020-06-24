using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

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

    public class User2
    {
        private static List<string> strings = new List<string>();
        private int[] names;

        public User2(string  fullName)
        {
            int getOrAdd(string s)
            {
                int index = strings.IndexOf(s);
                if (index != -1) return index; // already have strings cached if returned.
                else
                {
                    strings.Add(s);
                    return strings.Count - 1; // index of the last element.
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        // if you needed to write the full name back.
        public string FullName
        {
            get
            {
                return string.Join(" ",
                    names.Select(i => strings[i]));
            }
        }

    }


    [TestFixture]
    public class FlyweightsManualStringInterning
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }

        [Test]
        public void TestUser() // 6979786
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();

            foreach (string firstName in firstNames)
            {
                foreach (string lastName in lastNames)
                {
                    users.Add(new User($"{firstName} {lastName}"));
                }
            }

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        [Test]
        public void TestUser2() // 6979786 // 7465148 // demo showed before being more than after however, the original approach
        // was faster in my tests which means .NET could have updated its algorithms. 
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User2>();

            foreach (string firstName in firstNames)
            {
                foreach (string lastName in lastNames)
                {
                    users.Add(new User2($"{firstName} {lastName}"));
                }
            }

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                    .Select(i => (char)('a' + rand.Next(26)))
                    .ToArray());
        }
    }
}
