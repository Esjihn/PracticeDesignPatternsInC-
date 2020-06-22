using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Prototypes
{
    // ICloneable not specified for deep copy, only shallow copy of reference.
    // Prototype requires deep copy to be used properly so ICloneable should not be used. 
    public class Person : ICloneable
    {
        public string[] names;
        public Address address;

        public Person(string[] names, Address address)
        {
            this.names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            this.address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public override string ToString()
        {
            return $"{nameof(names)}: {string.Join(" ", names)}, {nameof(address)}: {address}";
        }

        // approach is dangerous. Returning object and ICloneable implementation not specified.
        public object Clone()
        {
            return new Person(names, (Address)address.Clone());
        }
    }

    public class Address : ICloneable
    {
        private string streetName;
        public int houseNumber;

        public Address(string streetName, int houseNumber)
        {
            this.streetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            this.houseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(streetName)}: {streetName}, {nameof(houseNumber)}: {houseNumber}";
        }

        // approach is dangerous. Returning object and ICloneable implementation not specified.
        public object Clone()
        {
            return new Address(streetName, houseNumber);
        }
    }

    public class PrototypeICloneableIsBad
    {

        // change to Main to run.
        public static void none(string[] args)
        {
            var john = new Person(new []{"John", "Smith"}, 
                new Address("London Road", 123));

            // just copying and changing the reference.
            //var jane = john;
            //jane.names[0] = "Jane";

            var jane = (Person)john.Clone();
            jane.address.houseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
