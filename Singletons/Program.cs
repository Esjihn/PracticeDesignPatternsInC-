using System;

namespace Singletons
{
    public class Program
    {
        // Creational
        // Singletons - A component which is only instantiated once. 
        // (A design pattern everyone loves to hate.. but is it really that bad?)
        // "When discussing which patterns to drop, we found that we still love them all.
        // (Not really-- I'm in favor of dropping Singleton. Its use is almost always a
        // design smell. -Erich Gamma (Gamma Categorization [structural, behavioral, creational] author)

        // Motivation
        // For some components it only makes sense to have one in the system.
        // 1) Database repository singleton (no point in having more than one).
        // 2) Object factory (factory isn't supposed to have any state, you only need one).
        // 3) e.g. the constructor call is expensive
        //      b) We only do it once.
        //      c) We provide everyone with the same instance.
        // 4) Want to prevent anyone (clients) from creating additional copies.
        // 5) Need to take care of lazy (call-by-need) instantiation and thread safety.

        // Summary
        // 1) Making a 'safe' singleton is easy:
        //    construct a static Lazy<T> and return its (Lazy<T>.) Value.
        // 2) Singletons are difficult to test.
        // 3) !!!Instead follow the dependency inversion principle. Instead of directly
        //    using a singleton, consider depending on an abstraction (i.e. an interface)!!!
        // 4) Consider defining singleton lifetime in DI container.
        // If using dependency injection pervasively it is not a big deal to call constructor to
        // instantiate singleton. 

        // change to Main to run. 
        public static void none(string[] args)
        {
            
        }
    }
}
