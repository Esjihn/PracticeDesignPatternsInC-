using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Prototypes
{
    public static class ExtensionMethods
    {
        // 'this T self' is just an extension on any type. 
        // BinaryFormatter is very fast.
        public static T DeepCopy<T>(this T self)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                object copy = formatter.Deserialize(stream);
                return (T)copy;
            }
        }

        // alternative to above and does not need [Serializable] attribute
        // added to all classes that need deep copy.
        // However, it does require parameterless constructors to be added.
        public static T DeepCopyXml<T>(this T self)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
        }
    }

    public class SerializationCopy
    {
        public class Person4 
        {
            public string[] names;
            public Address4 address;

            // required for xml serialization
            public Person4() { }

            public Person4(string[] names, Address4 address)
            {
                this.names = names ?? throw new ArgumentNullException(paramName: nameof(names));
                this.address = address ?? throw new ArgumentNullException(paramName: nameof(address));
            }

            public override string ToString()
            {
                return $"{nameof(names)}: {string.Join(" ", names)}, {nameof(address)}: {address}";
            }
        }

        public class Address4
        {
            private string streetName;
            public int houseNumber;

            // required for xml serialization
            public Address4() { }

            public Address4(string streetName, int houseNumber)
            {
                this.streetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
                this.houseNumber = houseNumber;
            }

            public override string ToString()
            {
                return $"{nameof(streetName)}: {streetName}, {nameof(houseNumber)}: {houseNumber}";
            }
        }

        // change to Main to run.
        public static void none(string[] args)
        {
            var john = new Person4(new[] {"John", "Smith"},
                new Address4("London Road", 123));

            //var jane = john.DeepCopy();
            var jane = john.DeepCopyXml();
            jane.names[0] = "Jane";
            jane.address.houseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
