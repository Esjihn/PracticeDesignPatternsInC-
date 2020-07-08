using System;
using System.Collections.Generic;
using System.Text;

namespace Mementos
{
    // Token (state) user will not have access
    public class Memento2
    {
        // private set
        public int Balance { get; }

        public Memento2(int balance)
        {
            this.Balance = balance;
        }
    }

    public class BankAccount2
    {
        private int balance;
        
        // add list and counter to roll back to initial state
        private List<Memento2> changes = new List<Memento2>();
        private int current;

        public BankAccount2(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento2(balance));
        }

        public Memento2 Deposit(int amount)
        {
            balance += amount;
            var m = new Memento2(balance);
            changes.Add(m);
            ++current;
            return m;
        }

        // add a restore state from memento
        public Memento2 Restore(Memento2 m)
        {
            // make sure there is something to undo. 
            if (m != null)
            {
                balance = m.Balance;
                changes.Add(m);
                return m;
            }

            return null;
        }

        public Memento2 Undo()
        {
            if (current > 0)
            {
                var m = changes[--current];
                balance = m.Balance;
                return m;
            }

            return null;
        }

        public Memento2 Redo()
        {
            if (current + 1 < changes.Count)
            {
                var m = changes[++current];
                balance = m.Balance;
                return m;
            }

            return null;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public class UndoRedoMemento
    {
        // change to main to run. 
        public static void none(string[] args)
        {
            var ba = new BankAccount2(100);
            ba.Deposit(50);
            ba.Deposit(25);
            Console.WriteLine(ba);

            ba.Undo();
            Console.WriteLine($"Undo 1: {ba}");
            ba.Undo();
            Console.WriteLine($"Undo 2: {ba}");
            ba.Redo();
            Console.WriteLine($"Redo: {ba}");
        }
    }
}
