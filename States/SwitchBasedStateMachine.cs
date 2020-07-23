using System;
using System.Collections.Generic;
using System.Text;

namespace States
{
    // Another academic approach. Not great to use. Doesn't scale well. 
    public enum State2
    {
        Locked, 
        Failed, 
        Unlocked
    }

    public class SwitchBasedStateMachine
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            // Failed on every keypress that is not contiguous 1-2-3-4.
            
            string code = "1234";
            var state = State2.Locked;
            var entry = new StringBuilder();

            while (true)
            {
                switch (state)
                {
                    case State2.Locked:
                        entry.Append(Console.ReadKey().KeyChar);

                        if (entry.ToString() == code)
                        {
                            state = State2.Unlocked;
                            break;
                        }

                        if (!code.StartsWith(entry.ToString()))
                        {
                            state = State2.Failed;
                        }

                        break;
                    case State2.Failed:
                        Console.CursorLeft = 0;
                        Console.WriteLine("FAILED");
                        entry.Clear();
                        state = State2.Locked;
                        break;
                    case State2.Unlocked:
                        Console.CursorLeft = 0;
                        Console.WriteLine("UNLOCKED");
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
