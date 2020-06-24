using System;

namespace Facades
{
    // FAÇADE - Provides a simple, easy to understand/user interface over a large and sophisticated
    // body of code. 
    // Exposing several components through a single interface.

    // Motivation
    // 1) Balancing complexity and presentation/usability (like a house)
    // 2) Typical home
    //      b) Many subsystems (electrical, sanitation and plumbing)
    //      c) Complex internal structure (i.e. floor built [layers])
    //      d) End user is not exposed to internals
    // 3) Same with software. (sometimes consumers just want something simple to work with.
    //      b) Many systems working to provide flexibility, but..
    //      c) API consumers want it to just work (couple simple API calls) 
    //          i) i.e Console complex internals, buffers, re-allocations, overruns, advanced settings
    //                      but Console.Write, WriteLine, Read, ReadLine are simple and easy to use.  

    // Summary
    // 1) Build a facade to provide a simplified API over a set of classes (or subsystem)
    // 2) May with to (optionally) expose internals through the facade. (Power users) (chunk of high and low level apis)
    // 3) May allow users to 'escalate' to use more complex APIs if they need to.

    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
