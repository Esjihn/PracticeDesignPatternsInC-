using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    // combination of composite and command
    // many commands can be treated as a single command. 

    // Composite - A mechanism for treating individual (scalar) objects and compositions
    // of objects in a uniform (in the same) manner.

    // Command - An object which represents an instruction to perform a particular action.
    // This command contains all the information necessary for the action to be taken.

    public class BankAccount2
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

    public interface ICommand2
    {
        void Call();

        void Undo();

        bool Success { get; set; }
    }

    public class BankAccountCommand2 : ICommand2
    {
        private readonly BankAccount2 _account;

        public enum Action
        {
            Deposit,
            Withdraw
        }

        private readonly Action _action;
        private readonly int _amount;
        private bool _succeeded;

        public BankAccountCommand2(BankAccount2 account, Action action, int amount)
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
                    _succeeded = true;
                    break;
                case Action.Withdraw:
                    _succeeded = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // ensure you undo properly by having a boolean flag when calling execute on the initial command.
        // this way you do not roll back a transaction for a failed deposit.
        public void Undo()
        {
            if (!_succeeded) return;
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

        public bool Success
        {
            get
            {
                return this._succeeded;
            }
            set
            {
                _succeeded = value;
            }
        }
    }

    // a --> b transfer from a to b. model as a single command when there are many. (Composite)

    public class CompositeBankAccountCommand
        :List<BankAccountCommand2>, ICommand2 // composite
    {

        public CompositeBankAccountCommand()
        {
            
        }

        public CompositeBankAccountCommand(
            IEnumerable<BankAccountCommand2> collection) : base(collection)
        {
            
        }

        public virtual void Call()
        {
            // A -> B
            // A 100
            // transfer 100

            ForEach(cmd => cmd.Call());
        }

        public virtual void Undo()
        {
            foreach (BankAccountCommand2 cmd in ((IEnumerable<BankAccountCommand2>)this).Reverse())
            {
                if(cmd.Success)
                    cmd.Undo();
            }
        }

        public virtual bool Success
        {
            get { return this.All(cmd => cmd.Success);}
            set
            {
                foreach (BankAccountCommand2 cmd in this)
                {
                    cmd.Success = value;
                }
            }
        }
    }

    public class MoneyTransferCommand : CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount2 from, BankAccount2 to, int amount)
        {
            AddRange(new[]
            {
                new BankAccountCommand2(from, BankAccountCommand2.Action.Withdraw, amount),
                new BankAccountCommand2(to, BankAccountCommand2.Action.Deposit, amount), 
            });
        }

        public override void Call()
        {
            BankAccountCommand2 last = null;
            foreach (var cmd in this)
            {
                if (last == null || last.Success)
                {
                    cmd.Call();
                    last = cmd;
                }
                else // know cmd has failed
                {
                    cmd.Undo();
                    break;
                }
            }
        }
    }

    public class CompositeCommand
    {
        // change to Main to run. 
        public static void none(string[] args)
        {
            //var ba = new BankAccount2();
            //var deposit = new BankAccountCommand2(ba, BankAccountCommand2.Action.Deposit, 100);
            //var withdraw = new BankAccountCommand2(ba, BankAccountCommand2.Action.Withdraw, 100);
            //var composite = new CompositeBankAccountCommand(new []{deposit, withdraw});

            //composite.Call();
            //Console.WriteLine(ba);
            //composite.Undo();
            //Console.WriteLine(ba);

            var from = new BankAccount2();
            from.Deposit(100);
            var to = new BankAccount2();

            var mtc = new MoneyTransferCommand(from, to, 1000);
            mtc.Call();

            Console.WriteLine(from);
            Console.WriteLine(to);

            mtc.Undo();

            Console.WriteLine(from);
            Console.WriteLine(to);
        }
    }
}
