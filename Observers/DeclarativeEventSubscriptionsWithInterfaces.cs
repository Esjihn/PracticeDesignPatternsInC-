using System;
using System.Collections.Generic;
using System.Text;

namespace Observers
{
    // observer using events, reflection, and IOC container
   
    public interface IEvent
    {
            
    }

    public interface ISend<TEvent> where TEvent : IEvent
    {

    }

    public class DeclarativeEventSubscriptionsWithInterfaces
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            
        }
    }
}
