using System;

namespace NullObjects
{
    // Null Object - A no-op that conforms to the required interface, satisfying a dependency requirement of 
    // some other object, but then does nothing at all. 
    // A behavioral design pattern with no behaviors :) (not part of the gang of four design pattern)
    
    // Can be Structural or Behavioral

    // Motivation
    // 1) When component A uses component B, it typically assumes that B is non-null
    //      b) You inject B, not B? or some Option<B>
    //      c) You do not check for null (?.) on every call
    // 2) There is no option of telling A not to use an instance of B
    //      b) Its use is hard-coded
    // 3) Thus, we build a no-op, non-functioning inheritor of B and pass it to A

    // Summary
    // 1) Implement the required interface
    // 2) Rewrite the methods with empty bodies
    //      b) If method is non-void, return default(T)
    //      c) If these values are ever used, you are in trouble;
    // 3) Supply an instance of Null Object in place of actual object
    // 4) Dynamic construction possible
    //      b) With associated performance implications

    public class Program
    {

        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
