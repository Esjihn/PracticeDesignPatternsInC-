using System;

namespace Flyweights
{
    // Flyweight - A space optimization technique that lest us use less memory by storing
    // externally the data associated with similar objects.
    // Space (memory) optimization!

    // Motivation
    // 1) Avoid redundancy when storing data
    //      a) i.e. MMORPG 
    //      b) Plenty of users with identical first/last names
    //      c) No sense in storing the same first/last name over and over again.
    //      d) Store a list of names and pointers to them (separate table or store) 
    // 2) .NET performs string interning, so an identical string is stored only once. 
    //      b) Strings are immutable you cannot change it, it must be remade or use
    //         character-modifier-level construct i.e. StringBuilder
    // 3) i.e. Bold or italic text in the console
    //      b) Don't want each character to have a formatting character.
    //      c) Operate on ranges (e.g., line number, start/end positions)

    // Summary
    // 1) Store common data externally. Minimize data storage needed. 
    // 2) Define the idea of 'ranges' on homogeneous collections and
    // store data related to those ranges.
    // 3) .NET string interning is the Flyweight pattern. 
    //      b) String Interning is a method of storing only one copy of each
    //         distinct String Value, which must be immutable.

    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
