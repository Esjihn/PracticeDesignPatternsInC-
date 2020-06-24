using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweights
{
    public class Sentence
    {
        private readonly string _plainText;

        private readonly WordToken[] _tokens;
        private string[] _strTokens;

        public Sentence(string plainText)
        {
            _plainText = plainText;
            _strTokens = _plainText.Split(' ');
        }

        public WordToken this[int index]
        {
            get
            {
                return _tokens[index];
            }
        }

        public override string ToString()
        {
            // todo return modified plain text.
            return _plainText;
        }

        public class WordToken
        {
            public bool Capitalize;

            public WordToken(bool capitalize)
            {
                Capitalize = capitalize;
            }
        }
    }

    public class FlyweightFormattingTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Console.WriteLine(sentence); // writes "hello WORLD"
        }
    }
}
