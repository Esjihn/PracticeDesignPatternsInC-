using System;

namespace Mediators
{
    // Mediator - A component that facilitates communication between other components without them
    // necessarily being aware of each other or having direct (reference) access to each other
    
    // Layman's: Facilitates communication between components.

    // Gang of Four: Define an object that encapsulates how a set of objects interact. 
    // Mediator promotes loose coupling by keeping objects from referring to each other explicitly
    // and lets you vary their interaction independently. 
    
    // Motivation
    // 1) Components may go in and out of a system at any time
    //      b) Chat room participants
    //      c) Players in an MMORPG
    // 2) It makes no sense for them to have direct references to one another. 
    //      b) Those references may go dead at anytime.
    // 3) Solution: have them all refer to some central component that facilitates communication
    
    // Summary


    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
