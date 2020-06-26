using System;

namespace ChainOfResponsibility
{
    // Chain of Responsibility - A chain of components who all get a chance to 
    // process a command or a query, optionally having default processing implementation and an ability
    // to terminate the processing chain. 
    // Sequence of handlers processing an event one after another.

    // Motivation
    // Example: working for volkswagen and you want to fake/lie reduced emissions via software
    // 1) Unethical behavior by an employee; who takes the blame?
    //      b) Employee (directly changed software)
    //      c) Manager (did he know about the employees acts or instructed him to do so?)
    //      d) CEO (is the entire company responsible? fines, jail time for CEO etc)
    // 2) Click graphical element on a form. Who handles event?
    //      b) button handles it, stops further processing
    //      c) button apart off the group box, it can handle it in its own way
    //      d) groupbox is apart of a window, it can handle it in its own way
    // 3) Collectable Card Game computer game
    //      b) Creature has attack and defense values
    //      c) Those can be boosted by other cards.
    //      d) If you want to fight the new and improved attack and defense card
    //         you would have to walk the Chain of responsibility by not just looking
    //         at the point of origin card but also all of the other cards which
    //         boosted the point of origin card

    // Command Query Separation - separate all of the invocations one called Query and one called Command. 
    // 1) Command = asking for an action or change (i.e. please set your attack value to 2)
    // 2) Query = asking for information (i.e. please give me your attack value) without changing anything.
    // 3) CQS = having separate means of sending commands and queries to direct field access.

    public class Program
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            
        }
    }
}
