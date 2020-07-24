using System;

namespace States
{
    // State -
    //      1) A pattern in which the object's behavior is determined by it's state.
    //          An object transitions from one state to another (something needs to trigger a transition).
    //      2) A formalized construct which manages state and transitions is called a state machine.

    // Fun with Finite State Machines

    // Gang of Four: Allow an object to alter its behavior when its internal state changes. 
    // The object will appear to change its class. 
    
    // Motivation
    // 1) Consider an ordinary telephone
    // 2) What you do with it depends on the state of the phone/line
    //      i) If it's raining or you want to make a call, you can pick it up.
    //      ii) Phone must be off the hook to talk / make a call
    //      iii) If you try calling someone, and it's busy, you put the handset down.
    // 3) Changes in state can be explicit or in response to event (Observer pattern) 

    // Summary
    // 1) Given sufficient complexity, it pays to formally define possible status and 
    //      events/triggers
    // 2) Can define
    //      a) State entry/exit behaviors
    //      b) Action when a particular event causes a transition
    //      c) Guard conditions enabling/disabling a transition
    //      d) Default action when no transitions are found for an event.

    public class Program
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            
        }
    }
}
