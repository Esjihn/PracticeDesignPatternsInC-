using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder
{
    public class Person2
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
    {
        private readonly List<Func<Person2, Person2>> actions
            = new List<Func<Person2, Person2>>();

        public TSelf Do(Action<Person2> action)
        {
            return AddAction(action);
        }

        public Person2 Build()
        {
            return actions.Aggregate(new Person2(), (p, f) => f(p));
        }

        private TSelf AddAction(Action<Person2> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });

            return (TSelf) this;
        }
    }

    public sealed class PersonBuilder2 : FunctionalBuilder<Person2, PersonBuilder2>
    {
        public PersonBuilder2 Called(string name)
        {
            return Do(p => p.Name = name);
        }
    }

    //public sealed class PersonBuilder2
    //{
    //    private readonly List<Func<Person2, Person2>> actions
    //    = new List<Func<Person2, Person2>>();

    //    public PersonBuilder2 Called(string name)
    //    {
    //        return Do(p => p.Name = name);
    //    }

    //    public PersonBuilder2 Do(Action<Person2> action)
    //    {
    //        return AddAction(action);
    //    }

    //    public Person2 Build()
    //    {
    //        return actions.Aggregate(new Person2(), (p, f) => f(p));
    //    }

    //    private PersonBuilder2 AddAction(Action<Person2> action)
    //    {
    //        actions.Add(p =>
    //        {
    //            action(p);
    //            return p;
    //        });

    //        return this;
    //    }

    //}
    public static class PersonBuilderExtensions
    {
        public static PersonBuilder2 WorksAs
            (this PersonBuilder2 builder, string position)
        {
            return builder.Do(p => p.Position = position);
        }
    }

    public class FunctionalBuilderMain
    {
        // change to main to run. 
        public static void none(string[] args)
        {
            var person = new PersonBuilder2()
                .Called("Sarah")
                .WorksAs("Developer")
                .Build();
        }
    }
}
