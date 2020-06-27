using System;

namespace Commands
{
    // Command - An object which represents an instruction to perform a particular action.
    // This command contains all the information necessary for the action to be taken.
    // "You shall not pass!" 

    // Motivation
    // 1) Ordinary C# statements are perishable
    //      b) Cannot undo a field/property assignment (nothing baked in)
    //      c) Cannot directly serialize a sequence of actions (calls) without using T types
    //          and lambdas to a degree (hard).
    // 2) Want an object that represents an operation
    //      b) X should change its property Y to Z
    //      c) X should do W()
    // 3) Uses: GUI commands, multi-level undo/redo, macro recording and more!

    // Summary
    // 1) Encapsulate all details of an operation in a separate object
    //    (convenient to serialize, store to memory or disk, easier to work with, and undo etc)
    // 2) Define instruction for applying the command (either in the command itself, or elsewhere)
    // 3) Optionally define instructions for undoing the command.
    // 4) Can create composite commands (aka macros) 
    
    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
