using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    // Sometimes you need several builders instead of a single to build a particular
    // aspect of an object - FacetedBuilder

    public class Person3
    {
        // address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}," +
                   $" {nameof(Postcode)}: {Postcode}, " +
                   $"{nameof(City)}: {City}, " +
                   $"{nameof(CompanyName)}: {CompanyName}," +
                   $" {nameof(Position)}: {Position}," +
                   $" {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    // Facade. Doesn't actually do the building but does keep a reference
    // to the builder object that is being built up. Gives access to sub builders
    public class PersonBuilder3 // facade
    {
        // reference! keep class, cause issues with struct implementation. 
        protected Person3 person = new Person3();

        public PersonJobBuilder works
        {
            get{return new PersonJobBuilder(person);}
        }

        public PersonAddressBuilder Lives
        {
            get{return new PersonAddressBuilder(person);}
        }

        public static implicit operator Person3(PersonBuilder3 pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder3
    {
        // might not work with a value type!
        public PersonAddressBuilder(Person3 person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postCode)
        {
            person.Postcode = postCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder3
    {
        public PersonJobBuilder(Person3 person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class FacetedBuilderMain
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            var pb = new PersonBuilder3();
            Person3 person = pb
                .Lives.At("123 London Road")
                .In("London")
                .WithPostCode("32303")
                .works.At("Microsoft")
                .AsA("Developer")
                .Earning(123000);

            Console.WriteLine(person);
        }
    }
}
