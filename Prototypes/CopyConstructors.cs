using System;
using System.Collections.Generic;
using System.Text;

namespace Prototypes
{
    public class Person2
    {
        public string[] names;
        public Address2 address;

        public Person2(string[] names, Address2 address)
        {
            this.names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            this.address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        // Copy Constructor
        public Person2(Person2 other)
        {
            names = other.names;
            address = new Address2(other.address);
        }

        public override string ToString()
        {
            return $"{nameof(names)}: {string.Join(" ", names)}, {nameof(address)}: {address}";
        }
    }

    public class Address2
    {
        private string streetName;
        public int houseNumber;

        public Address2(string streetName, int houseNumber)
        {
            this.streetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            this.houseNumber = houseNumber;
        }

        // Copy Constructor
        public Address2(Address2 other)
        {
            streetName = other.streetName;
            houseNumber = other.houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(streetName)}: {streetName}, {nameof(houseNumber)}: {houseNumber}";
        }
    }

    public class CopyConstructors
    {
        // change to main to run.
        public static void none(string[] args)
        {
            var john = new Person2(new[] { "John", "Smith" },
                new Address2("London Road", 123));

            // just copying and changing the reference.
            //var jane = john;
            //jane.names[0] = "Jane";
            Console.WriteLine(john);
            var jane = new Person2(john);
            jane.address.houseNumber = 321;

            
            Console.WriteLine(jane);
            Console.WriteLine(john);
        }
    }
}
