using System;

namespace Mementos
{
    // Memento - A token/handle representing the system state. Lets us roll back to the state
    // when the token was generated. May or may not directly expose state information. Typically
    // immutable and DOES NOT allow you to change the system unless the system explicitly asks
    // for the memento object to then roll itself back. 
    
    // Keep a token (memento) of an object's state to return to that state
    
    // Motivation
    // 1) An object or system goes through changes
    //      b) i.e. a bank account gets deposits and withdrawals
    // 2) There are different ways of navigating those changes
    // 3) One way is to record every change (Command) and teach a command
    //    to 'undo' itself.
    // 4) Another is to simply save snapshots of the system.

    // Summary
    // 1) Mementos are used to roll back states arbitrarily
    // 2) A memento is simply a token/handle class (typically no functions of its own) with readonly variables (get with private set)
    // 3) A memento is not required to expose directly the state(s) to which it reverts the system.
    // 4) Can be used to implement undo / redo (similar to Command Pattern)

    public class Program
    {
        // change to Main to run
        public static void none(string[] args)
        {
        }
    }
}
