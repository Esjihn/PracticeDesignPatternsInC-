using System;

namespace Adapters
{
    // Structural
    // Adapters - A construct which adapts an existing interface X to conform to the
    // required interface Y. 
    // Getting the interface you want from the interface you have.
    // 1) Emulate real world Electrical devices that have different power
    // (interface) requirements.
    //      b) Voltage(small 5V, large 220V)
    //      c) Socket/plug type (Europe, UK, USA)
    // 2) We cannot modify our gadgets to support every possible interface
    //      b) Some support possible (i.e. 120V/220V)
    // 3) Thus, we use a special device (an adapter) to give us the interface 
    // we require from the interface we have.

    public class Program
    {

        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
