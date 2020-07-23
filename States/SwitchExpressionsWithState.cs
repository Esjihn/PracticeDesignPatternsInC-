using System;
using System.Collections.Generic;
using System.Text;

namespace States
{
    // Switch expression not switch statement. Important distinction. C#8 necessary

    public enum Chest
    {
        Open, 
        Closed,
        Locked
    }

    enum Action
    {
        Open,
        Close
    }

    public class SwitchExpressionsWithState
    {
        // C# 8
        static Chest Manipulate
            (Chest chest, Action action, bool hasKey)
        {
            // limitation is that you cannot make a report but cleaner to code.
            return (chest, action, hasKey) switch
            {
                (Chest.Locked, Action.Open, true) => Chest.Open,
                (Chest.Closed, Action.Open, _) => Chest.Open,
                (Chest.Open, Action.Close, true) => Chest.Locked,
                (Chest.Open, Action.Close, false) => Chest.Closed,

                // nothing happens, "default" transition from current state to current state.
                _ => chest
            };
        }

        // C# 6 and 7 equivalent
        static Chest Manipulate2
            (Chest chest, Action action, bool hasKey)
        {
            switch (chest, action, hasKey)
            {
                case (Chest.Closed, Action.Open, _):
                    return Chest.Open;
                case (Chest.Locked, Action.Open, true):
                    return Chest.Open;
                case (Chest.Open, Action.Close, true):
                    return Chest.Locked;
                case (Chest.Open, Action.Close, false):
                    return Chest.Closed;
                default:
                    Console.WriteLine("Chest unchanged.");
                    return chest;
            }
        }

        // change to Main to run.
        public static void none(string[] args)
        {
            var chest = Chest.Locked;
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Open, true);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");
        }
    }
}
