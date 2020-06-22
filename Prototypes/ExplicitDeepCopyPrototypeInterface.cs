using System;
using System.Collections.Generic;
using System.Text;

namespace Prototypes
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Person3 : IPrototype<Person3>
    {
        public string[] names;
        public Address3 address;

        public Person3(string[] names, Address3 address)
        {
            this.names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            this.address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public Person3 DeepCopy()
        {
            return new Person3(names, address.DeepCopy());
        }

        public override string ToString()
        {
            return $"{nameof(names)}: {string.Join(" ", names)}, {nameof(address)}: {address}";
        }
    }

    public class Address3 : IPrototype2<Address3>
    {
        private string streetName;
        public int houseNumber;

        public Address3(string streetName, int houseNumber)
        {
            this.streetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            this.houseNumber = houseNumber;
        }

        public Address3 DeepCopy()
        {
            return new Address3(streetName, houseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(streetName)}: {streetName}, {nameof(houseNumber)}: {houseNumber}";
        }
    }
    public class ExplicitDeepCopyPrototypeInterface
    {

        // change to main to run.
        public static void none(string[] args)
        {
            var john = new Person3(new []{"John", "Smith"}, 
                new Address3("London Road", 123));

            var jane = john.DeepCopy();
            jane.address.houseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
