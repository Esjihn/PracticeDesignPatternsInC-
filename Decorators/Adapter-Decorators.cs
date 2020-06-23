using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Decorators
{
    public class MyStringBuilder
    {
        StringBuilder builder = new StringBuilder();

        // custom operator implicit conversion from string
        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.builder.Append(s);
            return msb;
        }
        // +=
        public static MyStringBuilder operator +(MyStringBuilder msb, string s)
        {
            msb.Append(s);
            return msb;
        }
        
        public override string ToString()
        {
            return builder.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)builder).GetObjectData(info, context);
        }

        public MyStringBuilder Append(bool value)
        {
            builder.Append(value);
            return this;
        }

        public MyStringBuilder Append(byte value)
        {
            builder.Append(value);
            return this;
        }

        public MyStringBuilder Append(char value)
        {
            builder.Append(value);
            return this;
        }

        public unsafe MyStringBuilder Append(char* value, int valueCount)
        {
            builder.Append(value, valueCount);
            return this;
        }

        public MyStringBuilder Append(char value, int repeatCount)
        {
            builder.Append(value, repeatCount);

            return this;
        }

        public MyStringBuilder Append(char[]? value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(char[]? value, int startIndex, int charCount)
        {
            builder.Append(value, startIndex, charCount);

            return this;
        }

        public MyStringBuilder Append(decimal value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(double value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(short value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(int value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(long value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(object? value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(ReadOnlyMemory<char> value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(ReadOnlySpan<char> value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(sbyte value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(float value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(string? value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(string? value, int startIndex, int count)
        {
            builder.Append(value, startIndex, count);
            return this;
        }

        public MyStringBuilder Append(MyStringBuilder? value)
        {
            builder.Append(value);
            return this;
        }

        public MyStringBuilder Append(ushort value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(uint value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder Append(ulong value)
        {
            builder.Append(value);

            return this;
        }

        public MyStringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0)
        {
            builder.AppendFormat(provider, format, arg0);

            return this;
        }

        public MyStringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0, object? arg1)
        {
            builder.AppendFormat(provider, format, arg0, arg1);

            return this;
        }

        public MyStringBuilder AppendFormat(IFormatProvider? provider, string format, object? arg0, object? arg1, object? arg2)
        {
            builder.AppendFormat(provider, format, arg0, arg1, arg2);

            return this;
        }

        public MyStringBuilder AppendFormat(IFormatProvider? provider, string format, params object?[] args)
        {
            builder.AppendFormat(provider, format, args);

            return this;
        }

        public MyStringBuilder AppendFormat(string format, object? arg0)
        {
            builder.AppendFormat(format, arg0);

            return this;
        }

        public MyStringBuilder AppendFormat(string format, object? arg0, object? arg1)
        {
            builder.AppendFormat(format, arg0, arg1);

            return this;
        }

        public MyStringBuilder AppendFormat(string format, object? arg0, object? arg1, object? arg2)
        {
            builder.AppendFormat(format, arg0, arg1, arg2);

            return this;
        }

        public MyStringBuilder AppendFormat(string format, params object?[] args)
        {
            builder.AppendFormat(format, args);

            return this;
        }

        public MyStringBuilder AppendJoin(char separator, params object?[] values)
        {
            builder.AppendJoin(separator, values);

            return this;
        }

        public MyStringBuilder AppendJoin(char separator, params string?[] values)
        {
            builder.AppendJoin(separator, values);

            return this;
        }

        public MyStringBuilder AppendJoin(string? separator, params object?[] values)
        {
            builder.AppendJoin(separator, values);

            return this;
        }

        public MyStringBuilder AppendJoin(string? separator, params string?[] values)
        {
            builder.AppendJoin(separator, values);
            return this;
        }

        public MyStringBuilder AppendJoin<T>(char separator, IEnumerable<T> values)
        {
            builder.AppendJoin(separator, values);
            return this;
        }

        public MyStringBuilder AppendJoin<T>(string? separator, IEnumerable<T> values)
        {
            builder.AppendJoin(separator, values);
            return this;
        }

        public MyStringBuilder AppendLine()
        {
            builder.AppendLine();
            return this;
        }

        public MyStringBuilder AppendLine(string? value)
        {
            builder.AppendLine(value);
            return this;
        }

        public MyStringBuilder Clear()
        {
            builder.Clear();
            return this;
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public void CopyTo(int sourceIndex, Span<char> destination, int count)
        {
            builder.CopyTo(sourceIndex, destination, count);
        }

        public int EnsureCapacity(int capacity)
        {
            return builder.EnsureCapacity(capacity);
        }

        public bool Equals(ReadOnlySpan<char> span)
        {
            return builder.Equals(span);
        }

        public bool Equals(MyStringBuilder? sb)
        {
            return builder.Equals(sb);
        }

        public StringBuilder.ChunkEnumerator GetChunks()
        {
            return builder.GetChunks();
        }

        public MyStringBuilder Insert(int index, bool value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, byte value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, char value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, char[]? value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, char[]? value, int startIndex, int charCount)
        {
            builder.Insert(index, value, startIndex, charCount);
            return this;
        }

        public MyStringBuilder Insert(int index, decimal value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, double value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, short value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, int value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, long value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, object? value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, ReadOnlySpan<char> value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, sbyte value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, float value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, string? value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, string? value, int count)
        {
            builder.Insert(index, value, count);
            return this;
        }

        public MyStringBuilder Insert(int index, ushort value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, uint value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Insert(int index, ulong value)
        {
            builder.Insert(index, value);
            return this;
        }

        public MyStringBuilder Remove(int startIndex, int length)
        {
            builder.Remove(startIndex, length);
            return this;
        }

        public MyStringBuilder Replace(char oldChar, char newChar)
        {
            builder.Replace(oldChar, newChar);
            return this;
        }

        public MyStringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
        {
            builder.Replace(oldChar, newChar, startIndex, count);
            return this;
        }

        public MyStringBuilder Replace(string oldValue, string? newValue)
        {
            builder.Replace(oldValue, newValue);
            return this;
        }

        public MyStringBuilder Replace(string oldValue, string? newValue, int startIndex, int count)
        {
            builder.Replace(oldValue, newValue, startIndex, count);
            return this;
        }

        public string ToString(int startIndex, int length)
        {
            return builder.ToString(startIndex, length);
        }

        public int Capacity
        {
            get => builder.Capacity;
            set => builder.Capacity = value;
        }

        public char this[int index]
        {
            get => builder[index];
            set => builder[index] = value;
        }

        public int Length
        {
            get => builder.Length;
            set => builder.Length = value;
        }

        public int MaxCapacity => builder.MaxCapacity;
}

public class Adapter_Decorators
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            // Implicit conversion and += adapter plus MyStringBuilder decorator
            MyStringBuilder s = "hello ";
            s += "world";
            Console.WriteLine(s);
        }
    }
}
