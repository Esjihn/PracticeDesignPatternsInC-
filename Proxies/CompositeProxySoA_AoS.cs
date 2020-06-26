using System;
using System.Collections.Generic;
using System.Text;

namespace Proxies
{
    //

    public class Creature2
    {
        public byte Age; // 1 byte
        public int X, Y; // 4 byte
    }

    public class Creatures
    {
        private readonly int size;
        private byte[] age;
        private int[] x, y;

        public Creatures(int size)
        {
            this.size = size;
            age = new byte[size];
            x = new int[size];
            y = new int[size];
        }

        // not going to store and significant values (reason for struct)
        public struct CreatureProxy
        {
            private readonly Creatures _creatures;
            private readonly int _index;

            public CreatureProxy(Creatures creatures, int index)
            {
                _creatures = creatures;
                _index = index;
            }

            public ref byte Age
            {
                get { return ref _creatures.age[_index]; }
            }

            public ref int X
            {
                get { return ref _creatures.x[_index]; }
            }
            public ref int Y
            {
                get { return ref _creatures.y[_index]; }
            }
        }

        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for (int pos = 0; pos < size; ++pos)
            {
                yield return new CreatureProxy(this, pos);
            }


        }
    }

    public class CompositeProxySoA_AoS
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var creatures = new Creature2[100];

            // inefficient
            // Age X.Y Age X.Y Age X.Y

            // efficient for modern cpus. Contiguous memory access to creature coordinates.
            // Age Age Age Age // array
            // X X X X // array
            // Y Y Y Y // array
            
            // old
            foreach (Creature2 c in creatures)
            {
                c.X++;
            }

            // new referencing into creatures.x. proxies for performance increase
            Creatures creature2 = new Creatures(100); // Structure of Array
            foreach (Creatures.CreatureProxy cp in creature2)
            {
                cp.X++;
            }
        }
    }
}
