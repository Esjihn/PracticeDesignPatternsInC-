using System;
using System.Collections.Generic;
using System.Text;

namespace Adapters
{
    // Cannot create Vector2f, Vector3i
    // (2 component of floating or 3 component of integers) normally..enter Dimensions below.
    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value
            {
                get { return 2; }
            }
        }

        public class Three : IInteger
        {
            public int Value
            {
                get { return 3; }
            }
        }
    }

    // naive approach
    public class Vector<TSelf, T, D>
        where D : IInteger, new()
        where TSelf : Vector<TSelf, T, D>, new()
    {
        protected T[] data;

        public Vector()
        {
            // in C# you cannot put literals inside of generic types. 
            // data = new T[D]; or // class Vector2f : Vector<float, 2> '2' not allowed.
            data = new T[new D().Value];
        }

        // params solves this
        //public Vector(T x, T y, T z....)
        public Vector(params T[] values)
        {
            // size of array you will store
            var requiredSize = new D().Value;
            data = new T[requiredSize];

            // provided size, minimum size i.e. 2d will be 2d, 3 component will be 3
            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
            {
                data[i] = values[i];
            }
        }

        //naive factory and doesn't work
        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();
            // size of array you will store
            var requiredSize = new D().Value;
            result.data = new T[requiredSize];

            // provided size, minimum size i.e. 2d will be 2d, 3 component will be 3
            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
            {
                result.data[i] = values[i];
            }

            return result;
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        // dangerous y and z will still be there even if its only 2 dimensional vector
        public T X
        {
            get { return data[0]; }
            set { data[0] = value; }
        }
    }

    public class VectorOfFloat<TSelf, D>
        : Vector<TSelf, float, D>
        where D : IInteger, new()
        where TSelf : Vector<TSelf, float, D>, new()
    {

    }

    public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D>
        where D : IInteger, new()
    {
        public VectorOfInt()
        {

        }

        public VectorOfInt(params int[] values) : base(values)
        {

        }

        // operator + makes //var result = v + vv; legal
        public static VectorOfInt<D> operator +
            (VectorOfInt<D> lhs, VectorOfInt<D> rhs)
        {
            var result = new VectorOfInt<D>();
            var dim = new D().Value;
            for (int i = 0; i < dim; i++)
            {
                result[i] = lhs[i] + rhs[i];
            }

            return result;
        }
    }

    // Remember...Cannot create Vector2f, Vector3i (2 component of floating or 3 component of integers) normally.
    // Well using Dimensions like so makes it valid.
    public class Vector2i : VectorOfInt<Dimensions.Two>
    {
        public Vector2i()
        {
            
        }

        public Vector2i(params int[] values) : base(values)
        {

        }
    }

    public class Vector3f :
        VectorOfFloat<Vector3f, Dimensions.Three>
    {
        public override string ToString()
        {
            return $"{string.Join(",", data)}";
        }
    }

    public class GenericValueAdapters
    {

        // Change to Main to run.
        public static void none(string[] args)
        {
            var v = new Vector2i(1, 2);
            v[0] = 0;

            var vv = new Vector2i(3, 2);

            // operator '+' cannot operate on generic types
            //var result = v + vv;
            var result = v + vv;

            // not Vector3f, it is Vector<float, Three> just an ordinary vector
            //var u = Vector3f.Create(3.5f, 2.2f, 1);
            
            // after adding the recursive generics (introducing insane amounts of complexity through TSelf generics)
            // you do get Vector3F
            Vector3f u = Vector3f.Create(3.5f, 2.2f, 1);
        }
    }
}
