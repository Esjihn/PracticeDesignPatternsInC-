using System;

namespace Adapters
{
    // Structural
    // Adapters - A construct which adapts an existing interface X to conform to the
    // required interface Y. 
    // Getting the interface you want from the interface you have.

    // Motivation
    // 1) Emulate real world Electrical devices that have different power
    // (interface) requirements.
    //      b) Voltage(small 5V, large 220V)
    //      c) Socket/plug type (Europe, UK, USA)
    // 2) We cannot modify our gadgets to support every possible interface
    //      b) Some support possible (i.e. 120V/220V)
    // 3) Thus, we use a special device (an adapter) to give us the interface 
    // we require from the interface we have.

    // Summary
    // Implementing an Adapter is easy
    // 1) Determine the API you have and the API you need.
    // 2) Create a component which (typically) aggregates (has reference to, ...)
    // the adaptee.
    // 3) Intermediate representations can pile up: use caching and other optimizations.
    // and clean up data when its no longer required.

    public class Program
    {

        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
