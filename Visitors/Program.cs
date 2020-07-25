using System;

namespace Visitors
{
    // Visitor - A pattern where a component (visitor) is allowed to traverse the entire
    // inheritance hierarchy. Implemented by propagating a single visit() method
    // throughout the entire hierarchy. (typically only once) (uses double dispatch)
    //      a) Dispatch
    //              i) Which function to call?
    //              ii) Single dispatch: depends on name of request and type of receiver
    //              iii) Double dispatch: depends on name of request and type of two receivers
    //                      (type of visitor, type of element being visited)

    // Typically a tool for structure traversal rather than anything else. 

    // Gang of Four - Represent an operation to be performed on the elements of an object structure. Visitor
    // lets you define a new operation without changing the classes of the elements on which it operates.
    
    // Motivation
    // 1) Need to define a new operation on an entire class hierarchy
    //      a) e.g make a document model printable to HTML/Markdown
    // 2) Do not want to keep modifying every class in the hierarchy
    // 3) Need access to the non-common aspects of the classes in the hierarchy
    //      a) i.e. extension method wont do because you need access to the internals of the classes.
    // 4) Create an external component to handle rendering
    //      a) but avoid type checks (massive switch statements, error prone and difficult)

    // Summary 
    // 1)

    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {

        }
    }
}
