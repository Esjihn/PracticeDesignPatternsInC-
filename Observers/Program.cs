using System;

namespace Observers
{
    // Observers - An observer is an object that wishes to be informed about events happening in the system. Typically
    // a subscription to monitor and receive notifications about those events. 
    // The entity generating the events is an observable. 
    // Built-in right into C#/.NET, right?

    // Motivation
    // 1) We need to be informed (notified) when certain things happen in our system.
    //      b) Object's property changes
    //      c) Object does something
    //      d) Some external event occurs
    // 2) We want to listen to events (encapsulation information) and notified when they occur
    // 3) Built into C# with the event keyword
    //      b) But then what is this IObservable<T> / IObserver<T> for?
    //      c) What about INotifyPropertyChanging / Changed?
    //      d) And what are BindingList<T> / ObservableCollection<T>?

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
