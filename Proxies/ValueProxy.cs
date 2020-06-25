using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Proxies
{
    // Is a proxy that is constructed over a primitive type (int or float) 
    // motivation - strongly typed.
    // Wrapper around primitive type that provides conversion to and from that value;
    
    // percentages stores and also transforms 50% of 100, convert 50 to a multiplier .50
    // and then multiply it by another value;

    // ValueProxy that masquerades as a number.
    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage : IEquatable<Percentage>
    {
        private readonly float _value;

        internal Percentage(float value)
        {
            _value = value;
        }

        public bool Equals(Percentage other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return obj is Percentage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !left.Equals(right);
        }

        public static float operator *(float f, Percentage p)
        {
            return f * p._value;
        }

        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a._value + b._value);
        }

        public override string ToString()
        {
            return $"{Math.Round(_value * 100)}%";
        }
    }

    public static class PercentageExtensions
    {
        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100.0f);
        }

        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.0f);
        }
    }

    public class ValueProxy
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            Console.WriteLine(10f * 5.Percent());
            Console.WriteLine(2.Percent() + 3.Percent()); // print 5 + '%' symbol
        }
    }
}
