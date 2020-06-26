using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proxies
{
    //
    public class MasonrySettings
    {
        //// checks all checkboxes
        //public bool? All
        //{
        //    get
        //    {
        //        if (Pillars == Walls
        //            && Walls == Floors)
        //            return Pillars;

        //        return null;
        //    }
        //    set
        //    {
        //        if (!value.HasValue) return;
        //        Pillars = value.Value;
        //        Walls = value.Value;
        //        Floors = value.Value;
        //    }
        //}

        //// Tri state logic 
        //public bool Pillars, Walls, Floors; // single checkboxes

        public bool? All
        {
            get
            {
                // if all are equal to flags[0] then return first element. 
                if (flags.Skip(1).All(f => f == flags[0]))
                    return flags[0];

                return null;
            }
            set
            {
                if (!value.HasValue) return;
                
                for (int i = 0; i < flags.Length; i++)
                {
                    flags[i] = value.Value;
                }
            }
        }

        private bool[] flags = new bool[3];

        public bool Pillars
        {
            get
            {
                return flags[0];
            }
            set
            {
                flags[0] = value;
            }
        }
        public bool Walls
        {
            get
            {
                return flags[1];
            }
            set
            {
                flags[1] = value;
            }
        }

        public bool Floors
        {
            get
            {
                return flags[2];
            }
            set
            {
                flags[2] = value;
            }
        }
    }

    public class CompositeProxy_Array_BackedProperties
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            MasonrySettings ms = new MasonrySettings();
            ms.Walls = true;
            ms.Pillars = false;
            ms.Floors = true;

            Console.WriteLine(ms.Walls);
            Console.WriteLine(ms.Pillars);
            Console.WriteLine(ms.Floors);

            ms.All = false;

            Console.WriteLine();
            Console.WriteLine(ms.Walls);
            Console.WriteLine(ms.Pillars);
            Console.WriteLine(ms.Floors);

            ms.All = true;

            Console.WriteLine();
            Console.WriteLine(ms.Walls);
            Console.WriteLine(ms.Pillars);
            Console.WriteLine(ms.Floors);

            Console.WriteLine(ms.All);
        }
    }
}
