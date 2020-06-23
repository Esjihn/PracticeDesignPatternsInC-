using System;

namespace Bridges
{
    // Structural
    // Bridge - A mechanism that decouples an interface (hierarchy) from an implementation (hierarchy)
    // Connecting components together through abstractions.

    // Motivations
    // Bridge prevents a 'Cartesian product' complexity explosion
    // Example: 
    //      a) Base class ThreadScheduler
    //      b) Can be preemptive or cooperative
    //      c) Can run on Windows or Unix
    //      d) End up with a 2x2 scenario: (4 classes) WindowsPTS, UnixPTS, WindowsCTS, UnixCTS
    //      3 would be 8 classes and so on. 
    //      as shown below the tree would become larger.
    // Life without Bridge:
    //                                  Thread Scheduler
                // PremptiveThreadScheduler         // CooperativeThreadScheduler
    // Windows PTS                                                      // Windows CTS
                // Unix PTS                         // Unix CTS

    // Life with Bridge:
    //          // ThreadScheduler -platformScheduler -----------------------> IPlatformScheduler (interface bound to ThreadScheduler, can also be abstract class)
    //                 // Preemptive ThreadScheduler                                      // Unix Scheduler (bridge)
    //                       // CooperativeThreadScheduler                                // Windows Scheduler (bridge)

    // Summary 
    // 1) Decouple abstraction from implementation essentially the core of OOP
    // 2) Both can exists as hierarchies (implementation and abstraction), then aggregation to have one reference (base)
    // 3) A stronger form of encapsulation by essentially 'compartmentalizing' instead of an all out inheritance approach.

    public class Program
    {
        // change to Main to run.
        static void none(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
