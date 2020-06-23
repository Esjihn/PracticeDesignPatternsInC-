using System;

namespace Composites
{
    // Composite - A mechanism for treating individual (scalar) objects and compositions
    // of objects in a uniform (in the same) manner.
    // Treating individual and aggregate objects uniformly (in the same manner).

    // Motivations
    // 1) Objects use other objects' fields/properties/members through inheritance and composition
    // 2) Composition lets us make compound objects
    //      b) i.e. a mathematical expression composed of simple expressions; or
    //      c) A grouping of shapes that can consists of several shapes. (Many applications for drawing use this)
    // 3) Composite design pattern is used to treat both single (scalar) and composite objects uniformly.
    // (in the same manner).
    //      b) i.e. Foo and Collection<Foo> have common API's which you can call on one or the other
    //      with out knowing whether you are working with single object or collection.


    public class Program
    {
        // change to Main to run.
        static void none(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
