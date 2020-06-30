using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iterators
{
    public class Creature : IEnumerable<int>
    {
        // single array backed field
        // sort of proxy property.
        private int[] _stats = new int[3];
        private const int strength = 0;
        private const int agility = 1;
        private const int intelligence = 2;

        public int Strength
        {
            get
            {
                return _stats[strength];
            }
            set
            {
                _stats[strength] = value;
            }
        }

        public int Agility
        {
            get
            {
                return _stats[agility];
            }
            set
            {
                _stats[agility] = value;
            }
        }

        public int Intelligence
        {
            get
            {
                return _stats[intelligence];
            }
            set
            {
                _stats[intelligence] = value;
            }
        }

        public double AverageStat
        {
            get
            {
                //return Strength + Agility + Intelligence / 3.0;
                return _stats.Average();
            }
        }

        // iteration of the different properties.
        public IEnumerator<int> GetEnumerator()
        {
            return _stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // indexer
        public int this[int index]
        {
            get
            {
                return _stats[index];
            }
            set
            {
                _stats[index] = value;
            }
        }
    }

    public class Iterator_Array_BackedProperties
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            
        }
    }
}
