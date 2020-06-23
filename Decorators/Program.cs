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

    // Summary
    // 1) A decorator keeps the reference to the decorated object(s)
    // 2) May or may not proxy over calls. (May or may not replicate the API of the original object)
    //      b) Use Resharper to Generate Delegated Members (i.e. stringbuilder has 137 different members)
    // 3) Exists in a static variation
    //      b) Layering of decorators not by calling their constructors but by specific their type with generic parameters
    //          X<Y<Foo>>
    //          Very limited due to inability to inherit from type parameters (not up to snuff in c# but good in c++) not recommended.
    
    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
