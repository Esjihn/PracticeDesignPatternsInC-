using System;
using System.Collections.Generic;
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

    public class CombinationLock
    {
        public CombinationLock(int[] combination)
        {
            // todo

        }

        // you need to be changing this on user input
        public string Status;

        public void EnterDigit(int digit)
        {
            // todo

        }
    }

    public class CombinationLockTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            string code = "12345";


        }
    }
}
