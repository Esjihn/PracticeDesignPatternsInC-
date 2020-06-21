using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public partial class Person
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {

        }

        public static Builder New
        {
            get { return new Builder(); }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        // protected instead of private for inheritance use. 
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    // class Foo : Bar<Foo> 
    public class PersonInfoBuilder<SELF> : PersonBuilder
    where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    // type list c++ does this often
    public class PersonJobBuilder<SELF> 
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
    where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    public class FluentBuilderInheritanceWithRecursiveGenerics
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            //var builder = new PersonJobBuilder();
            //builder.Called("Matthew");
            // .WorkAsA() doesn't work problem with fluent interfaces
            // is that you aren't allowed to use containing type as the return type. 

            // fluent inherited builder api
            var me = Person.New
                .Called("Matthew")
                .WorksAsA("Quant")
                .Build();
            Console.WriteLine(me);
        }
    }
}
