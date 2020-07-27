using System;

namespace PracticeDesignPatternsC_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Gamma Categorization

            // Creational Patterns
            // Deal with the creation (construction) of objects
            // Explicit (constructor) vs implicit (DI, reflection, etc)
            // Whole sale (single statement) vs piecewise (step-by-step)

            // Creational Summary

            // Builder: Separate component for when object construction gets too complicated.
            //      a) can also have mutually cooperating sub-builders of a single object.
            //      b) Often has fluent interface e.g. sb.Append().Append().Append()
            //      c) Piecewise (individual) construction
            // Factory: 
            //      a) Factory method more expressive than constructor (name different from containing class and overloads of similar arguments)
            //      b) Factory can be an outside class or inner class; inner class has the benefit of accessing private members.
            // Prototype:
            //      a) Creation of object from an existing object.
            //      b) Requires either explicit deep copy or copy through serialization (serialize entire object graph)
            // Singleton: 
            //      a) When you need to ensure just a single instance exists.
            //      b) Made thread-safe and lazy (call by need e.g. yield) with Lazy<T> 
            //          Lazy initialization of an object means that its creation is deferred until it is first used.
            //              (For this topic, the terms lazy initialization and lazy instantiation are synonymous.)
            //              Lazy initialization is primarily used to improve performance, avoid wasteful computation,
            //              and reduce program memory requirements
            //      c) Consider extracting interface or using dependency injection (only socially acceptable way of using singleton)


            // Structural Patterns
            // Concerned with the structure (e.g., class members)
            // Many patterns are wrappers that mimic the underlying
            // class' interface.
            // Stress the importance of good API design.

            // Structural Summary

            // Adapter: Converts the interface you get to the interface you need. (Can be useful to cache data for
            //                                                                     subsequent calls to deal with extra data overhead)
            // Bridge: Decouple abstraction from implementation
            // Composite: Allows clients to treat individual objects and compositions of objects uniformly. (Can implement IEnumerable + yield return (itself))
            // Decorator: Attach additional responsibilities to object. (sealed objects can be used via field and then implement delegating members)
            //            a) consider dynamic object since you can not inherit from typed arguments. e.g. Decorator of Decorators isn't feasible in .NET.
            // Facade: Provide a single unified interface over a set of classes/systems.
            //          a) Serving two different groups of people.
            //                  1) People who just want to get things done get the high level interface
            //                  2) Power users get advanced interfaces exposed as well. 
            // Flyweight: Efficiently support very large number of similar objects (players with the same name get reference to one name)
            // Proxy: Provide a surrogate object that forwards calls to the real object while performing additional functions
            //          a) e.g. Access Control, Communication, Logging, etc.
            //          b) Protection Proxy: checks users permissions
            //          c) Remote Proxy: doesn't reside in the same system but calling the object still serializes the data and forwards it to another machine 
            //          d) Dynamic proxy creates a proxy dynamically, without the necessity of replicating the target object API.


            // Behavioral Patterns
            // They are all different; no central theme.

            // Behavioral Summary

            // Chain of Responsibility: Allows components to process information/events in a chain.
            //          a) Each chain enlist themselves or leave.
            //          b) Each element in chain refers to the next element (linked list); or
            //          c) Make a (regular) list and iterate through it.
            // Command: Encapsulate a request into a separate object (Commands and Queries exist)
            //          a) Good for audit, replay, undo/redo
            //          b) Part of Command Query Separation / Command Query Responsibility Segregation (Query is also, effectively, a command)
            // Interpreter: (Compiler Theory in CIS) Transform textual input into object-oriented structures.
            //          a) Used by interpreters, compilers, static analysis tools, etc.
            //          b) Compiler Theory is a separate branch of Computer Science.
            // Iterator: Provides an interface for accessing elements of an aggregate object. 
            //          a) lucky because .NET has IEnumberable<T> which should be used in 99% of cases.
            //          b) Can also build state machines with IE<T>
            //          c) Can also yield return or yield break for flow control. 
            // Mediator: Provides mediation services between two objects
            //          a) e.g. message passing, chat room, event brokers.
            //          b) everyone has to refer to the mediator.
            //          c) Dependency injection makes this easier by sticking mediator as a constructor parameter of the base class.
            //                  So that all child classes know of it by getting Dependency Injection Container to pass a singleton mediator when
            //                  they want event brokers.
            // Memento: Yields token representing system states. (READ-ONLY)
            //          a) Tokens do not allow direct manipulation, but can be used in appropriate APIs.
            //          b) Only think you do with the token is pass it back to the system, whose state you want to
            //              rollback to the state represented by the token. 
            // Observer: Built into C# with the event keyword.
            //          a) Additional support provided for properties, collections, and observable streams (via reactive extensions Rx nuget)
            // State: We model systems by having one of separate possible states and transitions between those states (Triggers are the key to traverse states) 
            //          a) Such a system is called a finite state machine FSM.
            //          b) Special frameworks exists to orchestrate state machines. (stateless-4.0 nuget)
            // Strategy & Template Method: Both patterns define an algorithm blueprint/placeholder.
            //          a) Strategy uses composition (here's high level but the low level must implement an explicit interface)
            //                  i) Implement interface and then pass the interface type into strategy thereby making the whole thing work.
            //          b) Template Method uses inheritance and is an abstract class that with a non abstract member.
            //                  that defines the method of the sequence of steps to be performed.
            //                  Then you have a bunch of abstract methods being called that you implement those abstract members and that is how
            //                  you get the algorithm to actually function.
            // Visitor: Adding functionality to existing classes and hierarchies of classes through double dispatch.
            //          a) Used for traversal of a hierarchy
            //          b) If you have a tree you would visit every leaf in that tree and then transform it to something else.
            //          c) DLR doesn't require 'double hop' or double dispatch but you incur major performance costs for having to
            //                  determine type of visitor at run time. 

        }
    }
}
