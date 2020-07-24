using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States
{
    /**
     * A combination lock is a lock that opens after the right digits have been entered.
     * A lock is pre-programmed with a combination (e.g. 12345) and the user is expected
     * to enter this combination to unlock the lock.
     *
     * The lock has a Status field that indicates the state of the lock. The rules are"
     *      1) If the lock has just been locked (or at startup), the status is LOCKED.
     *      2) If a digit has been entered, the digit is shown on the screen. As the user
     *          enters more digits, they are added to Status.
     *      3) If the user has entered the correct sequence of digits, the lock status changes
     *          to OPEN
     *      4) If the user enters an incorrect sequence of digits, the lock status changes to ERROR
     */

    public enum Status
    {
        Locked,
        Failed,
        Unlocked
    }

    public class CombinationLock
    {
        public int[] Combination { get; }

        public CombinationLock(int[] combination)
        {
            Combination = combination;
        }

        // you need to be changing this on user input
        public string Status;

        public void EnterDigit(int digit)
        {
            Status += digit.ToString();
        }
    }

    public class CombinationLockTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var cl = new CombinationLock(new []{1,2,3,4,5});
            var status = Status.Locked;

            while (true)
            {
                switch (status)
                {
                    case Status.Locked:
                        var input = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                        cl.EnterDigit(input);

                        if (cl.Status.Length > cl.Combination.Length)
                        {
                            cl.Status = string.Empty;
                            Console.CursorLeft = 0;
                            Console.Write("Resetting entries\n");
                            Console.CursorLeft = 0;
                        }

                        if (cl.Status.Length == cl.Combination.Length)
                        {
                            int[] result = new int[cl.Combination.Length];
                            for (int i = 0; i < cl.Status.Length; i++)
                            {
                                var num = (int) Char.GetNumericValue(cl.Status[i]);
                                result[i] = num;
                            }
                            
                            if (result.SequenceEqual(cl.Combination))
                            {
                                status = Status.Unlocked;
                                break;
                            }
                        }

                        var combinationStr =
                            String.Join(",", cl.Combination.Select(p => p.ToString().ToArray()));
                       
                        if (!combinationStr.StartsWith(cl.Status))
                        {
                            status = Status.Failed;
                        }

                        break;
                    case Status.Failed:
                        Console.CursorLeft = 0;
                        Console.WriteLine("ERROR");
                        //cl.Status = string.Empty;
                        status = Status.Locked;
                        break;
                    case Status.Unlocked:
                        Console.CursorLeft = 0;
                        Console.WriteLine("OPEN");
                        return;
                    default:
                        Console.WriteLine("Contact system admin.");
                        break;
                }
            }

        }
    }

}
