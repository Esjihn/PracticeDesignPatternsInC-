using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Commands
{
    // Implement Account.Process() method to process different account commands.
    // 1) Rules
    //      b) Success indicates whether the operation was successful.
    //      c) You can only withdraw money if you have enough in your account

    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        public Action TheAction;
        public int Amount;
        public bool Success;
    }

    public class Account
    {
        public int Balance { get; set; }

        public void Process(Command c)
        {
            if (c.TheAction == Command.Action.Deposit)
            {
                Balance += c.Amount;
            }

            if (c.TheAction == Command.Action.Withdraw)
            {
                if (Balance >= c.Amount)
                {
                    c.Success = true;
                    Balance -= c.Amount;
                }
                else
                {
                    c.Success = false;
                }
            }
        }

        public override string ToString()
        {
            return $"{nameof(Balance)}: {Balance}";
        }
    }

    public class CommandPatternExercise
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            Command cmd = new Command();
            Account acct = new Account();
            cmd.TheAction = Command.Action.Deposit;
            acct.Balance = 0;
            cmd.Amount = 100;
            acct.Process(cmd); // 100
            Console.WriteLine(acct);

            cmd.TheAction = Command.Action.Withdraw;
            cmd.Amount = 75;
            acct.Process(cmd); // 25

            Console.WriteLine(acct);
            
            cmd.TheAction = Command.Action.Deposit;
            cmd.Amount = 50;
            acct.Process(cmd);
            
            Console.WriteLine(acct); // 75

            cmd.TheAction = Command.Action.Withdraw;
            cmd.Amount = 85;
            Console.WriteLine($"Attempting to withdraw ${cmd.Amount} from balance ${acct.Balance}");
            acct.Process(cmd); // no change not enough to withdraw

            Console.WriteLine($"Did it succeed? {cmd.Success}");
            Console.WriteLine(acct);

            cmd.TheAction = Command.Action.Withdraw;
            cmd.Amount = 70;
            acct.Process(cmd); // 5 remaining

            Console.WriteLine(acct);
        }
    }
}
