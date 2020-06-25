using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweights
{
    public class Sentence
    {
        private readonly WordToken[] _tokens;
        private readonly string[] _strTokens;

        public Sentence(string plainText)
        {
            _strTokens = plainText.Split(' ');
            _tokens = new WordToken[_strTokens.Length];
        }

        public WordToken this[int index]
        {
            get
            {
                if (index > _tokens.Length - 1) return new WordToken();

                _tokens[index] = new WordToken();
                _tokens[index].Capitalize = true;

                return _tokens[index];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (var i = 0; i < _strTokens.Length; i++)
            {
                if (this._tokens[i] != null && this._tokens[i].Capitalize)
                {
                    this._strTokens[i] = _strTokens[i].ToUpperInvariant();
                }

                sb.Append(_strTokens[i] + " ");
            }

            return sb.ToString();
        }

        public class WordToken
        {
            public bool Capitalize;
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