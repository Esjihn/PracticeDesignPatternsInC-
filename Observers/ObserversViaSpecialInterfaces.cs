using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Observers
{
    public class Event
    {
        
    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }

    public class Person2 : IObservable<Event>
    {
        private readonly HashSet<Subscription> _subscriptions
            = new HashSet<Subscription>();

        // quasi memento
        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            _subscriptions.Add(subscription);
            return subscription;
        }

        public void FallsIll()
        {
            foreach (var s in _subscriptions)
            {
                s.Observer.OnNext(
                    new FallsIllEvent{Address = "123 London Road"});
            }
        }

        private class Subscription : IDisposable
        {
            private readonly Person2 _person;
            public readonly IObserver<Event> Observer;

            public Subscription(Person2 person, IObserver<Event> observer)
            {
                _person = person;
                Observer = observer;
            }

            public void Dispose()
            {
                _person._subscriptions.Remove(this);
            }
        }
    }

    public class ObserversViaSpecialInterfaces : IObserver<Event>
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            // Rx: Special interfaces Originated with Reactive
            // Extensions and now available in .NET
            // IObserver, IObservable

            // possible invisible subscription possible leak
            // allows manual disposing through IDisposable
            new ObserversViaSpecialInterfaces();

        }

        public ObserversViaSpecialInterfaces()
        {
            var person = new Person2();
            //IDisposable sub = person.Subscribe(this);

            // Reactive Extensions event.
            person.OfType<FallsIllEvent>().Subscribe(args =>
                Console.WriteLine($"A doctor is required at {args.Address}"));

            person.FallsIll();
        }

        // called when there are nor more events that can be generated
        public void OnCompleted() { }

        // called when there is an error in the event stream
        public void OnError(Exception error) { }

        // invoked when something happens i.e. person fallsill
        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
            {
                Console.WriteLine($"A doctor is required at {args.Address}");
            }
        }
    }
}
