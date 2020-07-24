using System;

namespace Strategies
{
    // Strategy -
    // System behavior partially specified at runtime.
    // Gang of Four: Define a family of algorithms, encapsulate each one, and make them interchangeable.
    // Strategy lets the algorithm vary independently from the clients that use it. 

    // Motivation
    // 1) Many algorithms can be decomposed into higher- and lower- level parts.
    // 2) Making tea can be decomposed into
    //      a) The process of making a hot beverage (boil water, pour into cup); and 
    //      b) Tea-specific things (put teabag into water)
    // 3) The high-level algorithm can then be reused for making coffee or hot chocolate.
    //      a) Supported by beverage-specific strategies.

    // Summary
    // 1) Enables the exact behavior of a system to be selected either at run-time (dynamic)
    //      or at compile-time (static)
    // 2) Also known as a policy (esp. in the C++ world).

    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
