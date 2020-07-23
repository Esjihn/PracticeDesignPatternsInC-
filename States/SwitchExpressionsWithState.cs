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
        static Chest Manipulate
            (Chest chest, Action action, bool hasKey) =>
            (chest, action, hasKey) switch
            {
                (Chest.Locked, Action.Open, true) => Chest.Open,
                (Chest.Closed, Action.Open, _) => Chest.Open,
                (Chest.Open, Action.Close, true) => Chest.Locked,
                (Chest.Open, Action.Close, false) => Chest.Closed,

                // nothing happens, "default" transition from current state to current state.
                _ => chest
            };

        // change to Main to run.
        public static void Main(string[] args)
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
