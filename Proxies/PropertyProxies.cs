using System;
using System.Collections.Generic;
using System.Text;

namespace Proxies
{
    // Using an object as a property instead of a literal value.
    // Implicit property proxy checks for identical assignments and prevents them by returning early instead of reassigning.
    // see public T Value property.

    public class Property<T> : IEquatable<Property<T>> where T : new()
    {
        private T value;

        public T Value
        {
            get { return value; }
            set
            {
                if (Equals(this.value, value)) return;
                Console.WriteLine($"Assigning value to {value}");
                this.value = value;
            }
        }

        // public Property() : this(default()) // will give you null if you have a reference type. 
        public Property() : this(Activator.CreateInstance<T>())
        {
            
        }

        public Property(T value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static implicit operator T(Property<T> property)
        {
            return property.value; // int n = p_int;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 123;
        }

        public bool Equals(Property<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Property<T>) obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !Equals(left, right);
        }
    }

    public class Creature
    {
        private Property<int> agility = new Property<int>();

        public int Agility
        {
            get { return agility.Value; }
            set { agility.Value = value; }
        }
    }

    public class PropertyProxies
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var c = new Creature();
                            // public Property<int> Agility { get; set; }
            c.Agility = 10; // c.set_Agility(10) doesnt happen. C# cannot overload the assignment operator new object instead of changing existing one
                            // c.Agility = new Property<int>(10) happens instead. 

            c.Agility = 10; // Implicit property proxy checks for identical assignments and prevents them by returning early instead of reassigning.
        }
    }
}
