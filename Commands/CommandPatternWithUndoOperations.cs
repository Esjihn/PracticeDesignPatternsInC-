using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public class BankAccount
    {
        private int _balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {_balance}");
        }

        public bool Withdraw(int amount)
        {
            if (_balance - amount >= overdraftLimit)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance is now {_balance}");
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{nameof(_balance)}: {_balance}";
        }
    }

    public interface ICommand
    {
        void Call();

        void Undo();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount _account;

        public enum Action
        {
            Deposit, 
            Withdraw
        }

        private Action _action;
        private int _amount;
        private bool succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
            this._action = action;
            this._amount = amount;
        }

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    succeeded = true;
                    break;
                case Action.Withdraw:
                    succeeded = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        // ensure you undo properly by having a boolean flag when calling execute on the initial command.
        // this way you do not roll back a transaction for a failed deposit.
        public void Undo()
        {
            if (!succeeded) return;
            switch (_action)
            {
                case Action.Deposit:
                    _account.Withdraw(_amount);
                    break;
                case Action.Withdraw:
                    _account.Deposit(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class CommandPatternWithUndoOperations
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            BankAccount ba = new BankAccount();

            // Queryable audit info is not lost compared to normal methods. 
            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000)
            };

            Console.WriteLine(ba); // accounts aren't processed yet.

            foreach (BankAccountCommand c in commands)
            {
                c.Call();
            }

            Console.WriteLine(ba);

            // reverse commands
            foreach (BankAccountCommand c in Enumerable.Reverse(commands))
            {
                c.Undo();
            }

            Console.WriteLine(ba);
        }
    }
}
