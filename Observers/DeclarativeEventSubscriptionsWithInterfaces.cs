using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Autofac;

namespace Observers
{
    // observer using events, reflection, and IOC container
   
    public interface IEvent
    {
            
    }

    public interface ISend<TEvent> where TEvent : IEvent
    {
        event EventHandler<TEvent> Sender;
    }

    public interface IHandle<TEvent> where TEvent : IEvent
    {
        void Handle(object sender, TEvent args);
    }

    public class ButtonPressedEvent : IEvent
    {
        public int NumberOfClicks;
    }

    public class Button2 : ISend<ButtonPressedEvent>
    {
        public event EventHandler<ButtonPressedEvent> Sender;

        public void Fire(int clicks)
        {
            Sender?.Invoke(this, new ButtonPressedEvent
            {
                NumberOfClicks = clicks
            });
        }
    }

    public class Logging : IHandle<ButtonPressedEvent>, IDisposable
    {
        public void Handle(object sender, ButtonPressedEvent args)
        {
            Console.WriteLine(
                $"Button clicked {args.NumberOfClicks} times");
        }

        public void Dispose()
        {
            // No information about what we are subscribed to when using automatic process. 
        }
    }

    public class DeclarativeEventSubscriptionsWithInterfaces
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var cb = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            cb.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ISend<>)) 
                .SingleInstance();

            cb.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IHandle<>)
                    ))
                .OnActivated(act =>
                {
                    // IHandle<Foo>
                    // find every ISend<Foo>.Sender += act.Instance.Handle
                    var instanceType = act.Instance.GetType();
                    var interfaces = instanceType.GetInterfaces();

                    foreach (Type i in interfaces)
                    {
                        if (i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IHandle<>))
                        {
                            // IHandle<Foo>
                            var arg0 = i.GetGenericArguments()[0];

                            // ISend<Food>, to find we need to construct first
                            var senderType = typeof(ISend<>).MakeGenericType(arg0);

                            // find every single ISend<Foo> in container
                            // IEnumberable<IFoo> -> every instance of object that implements IFoo
                            var allSenderTypes = typeof(IEnumerable<>)
                                .MakeGenericType(senderType);

                            // IEnumerable<ISend<Food>>
                            var allServices = act.Context.Resolve(allSenderTypes);
                            foreach (object service in (IEnumerable) allServices)
                            {
                                var eventInfo = service.GetType().GetEvent("Sender");
                                var handleMethod = instanceType.GetMethod("Handle");
                                if (eventInfo != null
                                    && handleMethod != null
                                    && eventInfo.EventHandlerType != null)
                                {
                                    var handler = Delegate.CreateDelegate(
                                        eventInfo.EventHandlerType, null, handleMethod);

                                    eventInfo.AddEventHandler(service, handler);
                                }
                            }
                        }
                    }
                })
                .SingleInstance()
                .AsSelf();

            var container = cb.Build();

            var button = container.Resolve<Button2>();
            
            // logging is getting automatically subscribed.
            var logging = container.Resolve<Logging>();

            button.Fire(1);
            button.Fire(2);

            // Problems
            // 1) If you create a new event the during the single instance creation process it will not
            //      automatically subscribe to those new events. 
            // 2) IDisposable - hard to implement using declarative approach,
            //      No information about what we are subscribed to when using automatic process. 
            //      Instead of event senders and a handle method you would need to use a event broker
            //      Centralized component / bus / class / broker. 
        }
    }
}