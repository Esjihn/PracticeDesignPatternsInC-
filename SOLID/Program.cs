using System;
using System.Collections.Generic;
using System.IO;

namespace SOLID
{
    // SOLID
    // Single Responsibility Principle - Classes and methods should have only one responsibility. 
    
    // Motivation
    // Easier testability
    // Code-base is easier to understand.

    // Open / Closed Principle - Classes should be closed for modification and open for extension.
    
    // Motivation
    // No need to re deploy already existing modules. 
    // Easier testability
    // Stability
    
    // Liskov substitution Principle (Barbara Liskov) - You should be able to substitute a base type for a sub type.

    // Motivation store a reference of a sub type as a base type. i.e. Square square = new Rectangle() (should be valid)
    // New derived classes just extend without replacing the functionality of old classes
    // New classes can produce undesirable affects when they are used in existing program modules. 
    // Stability. 

    // Interface segregation Principle - No client should be forced to depend on methods it does not use. 

    // Motivation 
    // Interface version of SRP, iSRP 

    // Dependency inversion Principle - High level parts should not depend on low level systems directly instead they 
    // should depend on some sort of abstraction (interface constructor injection etc)
    
    // Motivation 
    // Low level parts can define how underlying data structures are managed because they themselves are never directly
    // exposed to the high level parts. 

    public class Program
    {

    }
}
