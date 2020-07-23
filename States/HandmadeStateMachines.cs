using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace States
{
    // 1) set a states
    // 2) and triggers (events)

    public enum State
    {
        OffHook,
        Connecting,
        Connected,
        OnHold
    }

    // State transitions that are requires to transition to a particular state.
    public enum Trigger
    {
        CallDialed,
        HungUp,
        CallConnected,
        PlacedOnHold,
        TakenOffHold,
        LeftMessage
    }

    public class HandmadeStateMachines
    {
        private static Dictionary<State, List<(Trigger, State)>> rules
            = new Dictionary<State, List<(Trigger, State)>>
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.CallConnected, State.Connected)
                },
                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.PlacedOnHold, State.OnHold)
                },
                [State.OnHold] = new List<(Trigger, State)>
                {
                    (Trigger.TakenOffHold, State.Connected),
                    (Trigger.HungUp, State.OffHook)
                }
            };

        // change to Main to run.
        public static void none(string[] args)
        {
            var state = State.OffHook;

            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine("Select a trigger:");

                for (int i = 0; i < rules[state].Count; i++)
                {
                    // _ = ignore the second parameter
                    var (t, _) = rules[state][i];
                    Console.WriteLine($"{i}. {t}");
                }

                int input = int.Parse(Console.ReadLine());

                    var (_, s) = rules[state][input];
                    state = s;
            }
        }
    }
}
