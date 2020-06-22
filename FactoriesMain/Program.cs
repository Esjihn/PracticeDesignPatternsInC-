using System;

namespace Factories
{
    public class Program
    {
        // Factory and Abstract Factory TODO
        // A component responsible solely for the wholesale (not piecewise)
        // creation of objects

        // Motivation
        // 1) Object creation logic becomes too convoluted.
        // 2) Constructor is not descriptive
        //     b) Name mandated by name of containing type. (cannot pass additional info)
        //     c) Cannot overload constructor with same sets of arguments with different
        //       names. (optional parameter hell)
        // 3) Object creation (non-piecewise, unlike Builder) can be outsourced to.
        //     b) A separate function (Factory Method)
        //     c) That may exist in a separate class (Factory)
        //     d) Can create hierarchy of factories with Abstract Factory

        // Summary
        // A factory method is a static method that creates objects
        // A factory can take care of object creation (separate factory)
        // A factory can be external or reside inside the object as an
        // inner class. Advantage is access to private fields and methods.

        // change to main to run.
        public static void none(string[] args)
        {
        }
    }
}
