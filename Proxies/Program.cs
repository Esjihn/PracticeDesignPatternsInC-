using System;

namespace Proxies
{
    // Proxy - A class that functions as an interface to a particular resource...
    // that resource may be remote, expensive to construct, or may require logging or
    // some other added functionality. 

    // layman's terms: An interface for accessing a particular resource (Surrogate)
    // Gang of four terms: Provide a surrogate or placeholder for another object to control
    // access to it.

    // Motivation
    // 1) You are calling foo.Bar()
    // 2) This assumes that foo is in the same process as Bar() invocation
    // 3) What if, later on, you want to put all Foo-related operations
    // into a separate process (i.e. marshall by reference)
    //      b) Can you avoid changing your code?
    // 4) Proxy to the rescue!
    //      b) Same interface, entirely different behavior
    // 5) This is called a communication proxy.
    //      b) Other types: logging, virtual, guarding (check access control to fields and methods)

    // Proxy vs. Decorator
    // P) provides an identical interface (to the derived type through implementing delegating members)
    // D) provides an enhanced interface (to the derived type through implementing delegating members)
    // D) typically aggregates (or has reference to) what it is decorating
    // P) proxy does not need a reference necessarily and might not even be working with a materialized object 
    // for example it could be working with a "Lazy" interface over the whole derived type.
    // layman's: Proxy is typically a decorator with modifications within the delegated members with or without a 
    // direct reference.
    // Decorator has a reference and will typically implement delegated members and then add NEW methods and NEW
    // fields to augment existing behavior.

    // Summary
    // 1) A proxy has the same interface as the underlying object.
    // 2) To create a proxy, simply replicate the existing interface of an object
    // 3) Add relevant functionality to the redefined member functions.
    //      b) so instead of blindly forwarding every single invocation of the delegated members like the 
    //         decorator pattern. In proxy we just add relevant functionality.
    // 4) Different proxies (communication, logging, caching, etc.) have completely different behaviors.
    // 5) Proxy design pattern is actual a root or core process of a wide variety of proxies for the functionality
    //    that you actually need. 

    public class Program
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            
        }
    }
}
