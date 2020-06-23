using System;

namespace Decorators
{
    // Decorator - Facilitates the addition of behaviors to individual objects without inheriting from them.
    // Adding behavior without altering the class itself.

    // Motivation
    // 1) Want to augment an object with additional functionality.
    // 2) Do not want to rewrite or alter existing code (O.C.P.)
    // 3) Want to keep new functionality separate (S.R.P)
    // 4) Need to be able to interact with existing structures. (Has traits shared with existing structures).
    // 5) Two Options:
    //      b) Inherit from required object if possible; some objects are sealed. i.e. StringBuilder
    //      c) Build a Decorator, which simply references the decorated object(s). Since you cannot rely on inheritance
    //          you have to replicate the API and proxy the calls.

    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
