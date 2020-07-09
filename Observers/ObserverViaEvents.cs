using System;
using System.Collections.Generic;
using System.Text;

namespace Observers
{
    public class FallsIllEventArgs
    {
        public string Address;
    }

    public class Person
    {
        // event special construct to inform listeners
        public event EventHandler<FallsIllEventArgs> FallsIll;

        public void CatchACold()
        {
            FallsIll?.Invoke(this, 
                new FallsIllEventArgs {Address = "123 London Road"});
        }
    }

    public class ObserverViaEvents
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var person = new Person();
            person.FallsIll += CallDoctor;
            person.CatchACold();
            person.FallsIll -= CallDoctor; // no longer publishing
        }

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"A doctor has been called to {e.Address}");
        }
    }
}
