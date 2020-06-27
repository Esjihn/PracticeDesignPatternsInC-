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

    public class Program
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
