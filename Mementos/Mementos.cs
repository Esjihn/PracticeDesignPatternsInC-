using System;
using System.Collections.Generic;
using System.Text;

namespace Mementos
{
    // Token (state) user will not have access
    public class Memento
    {
        // private set
        public int Balance { get; }

        public Memento(int balance)
        {
            this.Balance = balance;
        }
    }

    public class BankAccount
    {
        private int balance;

        public BankAccount(int balance)
        {
            this.balance = balance;
        }

        public Memento Deposit(int amount)
        {
            balance += amount;

            // return token (state) 
            return new Memento(balance);
        }

        // add a restore state from memento
        public void Restore(Memento m)
        {
            balance = m.Balance;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public class Mementos
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            // cannot roll back to initial state in base "vanilla" memento.

            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            Console.WriteLine(ba);

            ba.Restore(m1);
            Console.WriteLine(ba);

            ba.Restore(m2);
            Console.WriteLine(ba);
        }
    }
}
