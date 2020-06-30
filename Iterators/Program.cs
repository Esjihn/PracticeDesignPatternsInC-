using System;

namespace Iterators
{
    // Iterators - An object (or, in .NET, it could be a method) that facilitates the traversal of a data structure.
    // How traversal of data structures happens and who makes it happen. 

    // Motivation
    // 1) Iteration (traversal) is a core functionality of various data structures
    // 2) An iterator is a class that facilities the traversal.
    //      b) Keeps a reference to the current element.
    //      c) Knows how to move to a different element.
    // 3) Iterator is an implicit construct
    //      b) .NET then builds a state machine around your yield return statements. (happens automatically)

    // Summary
    // 1) An iterator specified how you can traverse an object (in-order, post-order, pre-order
    // 2) An iterator object, unlike a method, cannot be recursive. (state machine) 
    // 3) Generally, an IEnumerable<T> returning method is enough. (method for each traversal mechanisms)
    // 4) Iteration works through duck typing -- you need a GetEnumerator() that yields a type that has
    //    Current and MoveNext();
    // 5) .NET framework will allow the use of said iterator as part of a foreach statement.

    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
