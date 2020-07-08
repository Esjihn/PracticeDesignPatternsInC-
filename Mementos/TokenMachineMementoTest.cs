using System;
using System.Collections.Generic;
using System.Text;

namespace Mementos
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento3
    {
        public int Value { get; }

        public Memento3(int value)
        {
            Value = value;
        }
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();
        
        public Memento3 AddToken(int value)
        {
            var m = new Memento3(value);
            Tokens.Add(new Token(value));
            return m;
        }

        public Memento3 AddToken(Token token)
        {
            if (token != null)
            {
                var m = new Memento3(token.Value);
                Tokens.Add(token);
                return m;
            }
            
            return null;
        }

        public void Revert(Memento3 m)
        {
            if (m != null)
            {
                var find = Tokens.Find(t => t.Value == m.Value);

                if (find != null)
                {
                    Tokens.Clear();

                    // revert back to memento state. 
                    Tokens.Add(new Token(find.Value));
                }
            }
        }

        public void Print()
        {
            foreach (Token token in Tokens)
            {
                Console.Write(token.Value + " ");
            }
        }
    }

    public class TokenMachineMementoTest
    {
        // change to Main to run. 
        public static void Main(string[] args)
        {
            var tm = new TokenMachine();

            // revert to 3
            var m = new Memento3(3);

            var token1 = new Token(2);
            tm.AddToken(token1);
            tm.AddToken(1);
            tm.AddToken(3);
            tm.AddToken(4);
            tm.AddToken(5);
            tm.AddToken(110);
         
            tm.Print();
            Console.WriteLine();
            tm.Revert(m);

            tm.Print();
        }
    }
}
