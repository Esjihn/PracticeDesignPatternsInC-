using System;

namespace Prototypes
{
    // Prototypes (object copying)
    // A partially or fully initialized object that you copy (clone) and
    // make use of.
    // When its easier to copy and existing object than to fully initialize
    // one.

    // Motivation
    // 1) Complicated objects (e.g. cars, phone) aren't designed from scratch
    // and prototype is the same idea for software development.
    // 2) An existing (partially or fully constructed) design is a Prototype.
    // 3) We make a copy (clone) the prototype and customize it.
    //      b) Requires 'deep copy' support i.e. not just the object itself
    //          but also all of its references and state (recursively)
    // 4) We make cloning convenient (i.e. Factory) via API

    // Summary
    // To implement a prototype partially construct an object and store
    // it somewhere
    // Clone the prototype
    //      b) Implement your own deep copy functionality.
    //          i) via copy constructors and/or explicit interface (complex)
    //          ii) via baked in Serialize and Deserialize (ensures entire object graph copy)
    // Customize the resulting instance.

    public class Program
    {
        // change to Main to run.
        static void none(string[] args)
        {
            
        }
    }
}
