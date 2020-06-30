using System;

namespace Interpreters
{
    // Interpreters - A component that processes structured text data.
    //      b) Does so by turning it into separate lexical tokens (lexing).
    //      c) and then interpreting sequences of said tokens (parsing).

    // Interpreters are all around us. Even now, in this very room also separate field of computer science.
    // We will approach from a high level. 

    // Motivation
    // 1) Textual input needs to be processed. 
    //      b) i.e. turned into OOP structures.
    // 2) Some examples
    //      b) Programming language compilers, interpreters, and IDE's. Resharper interprets C#
    //      c) HTML, XML, and similar (i.e. XLINQ, XML to LINQ)
    //      d) Numeric expressions (3+4/5) Computational
    //      e) Regular expressions (state machine)
    // 3) Turning strings into OOP based structures in a complicated process.

    // Summary
    // 1) Barring simple cases, an interpreter acts in two stages.
    // 2) Lexing - turns text into a set of tokens.
    //      b) i.e (3*(4+5) -> Lit[3] Star LParent Lit[4] Plus Lit[5] Rparen 'tokens'
    // 3) Parsing - tokens into meaningful constructs
    //      b) MultiplicationExpression[Integer[3], AdditionExpression[Integer[4], Integer[5]]] (pemdas)
    //      c) 'abstract syntax tree
    //      d) Parsed data can then be traversed, transformed, and interpreted. 

    public class Program
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            
        }
    }
}
