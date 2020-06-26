using System;

namespace Builder
{
    // Creational
    // Builder - When piecewise object construction is complicated provide
    // an API for doing it succinctly (clearly and briefly expressed)
    
    // Motivation
    // 1) Some objects are simple and can be created in a single
    // constructor call
    // 2) Other objects require a lot of ceremony to create. 
    // 3) Having an object with 10 constructor arguments is not productive.
    // 3b) Instead, opt for piecewise construction
    // Builder provides an API for constructing an object step-by-step.


    // Summary
    // 1) A builder is a separate component for building an object.
    // 2) Can either give builder a constructor or return it via
    // a static function
    // 3) To make builder fluent, simply return "this"
    // 4) Different facets of an object can be built with different builders
    // in tandem via a base class.
    public class Program
    {
        public static void none(string[] args)
        {
            
        }
    }
}
