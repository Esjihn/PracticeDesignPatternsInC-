using System;

namespace TemplateMethodPattern
{
    // Template Method - Allows us to define the "skeleton" of the algorithm,
    // with concrete implementations defined in subclasses.

    // A high-level blueprint for an algorithm to be completed by inheritors.

    // Gang of Four: Define the skeleton of an algorithm in an operation. Deferring some steps to
    // sub classes. Template Method lets subclasses redefine certain steps of an algorithm
    // without changing the algorithms overall structure.

    // Motivation
    // 1) Algorithms can be decomposed into common parts + specifics.
    // 2) Strategy pattern does this through composition (already does it for us)
    //      a) High-level algorithm uses an interface.
    //      b) Concrete implementations implement the interface.
    // 3) Template Method does the same thing through inheritance instead (abstract base)
    //      a) Overall algorithm makes use of abstract member.
    //      b) Inheritors override the abstract members.
    //      c) Parent template method invoked.

    // Summary
    // 1) Define an algorithm at a high level.
    // 2) Define constituent parts as abstract methods / properties.
    // 3) Inherit the algorithm class, providing necessary overrides. 


    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
