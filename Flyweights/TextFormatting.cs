using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweights
{
    // old append a true/false state to each character
    public class FormattedText
    {
        private readonly string _plainText;
        private readonly bool[] _capitalize;

        public FormattedText(string plainText)
        {
            _plainText = plainText;
            _capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                _capitalize[i] = true;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _plainText.Length; i++)
            {
                char c = _plainText[i];
                sb.Append(_capitalize[i] ? char.ToUpper(c) : c);
            }

            return sb.ToString();
        }
    }

    // new use a range 
    public class BetterFormattedText
    {
        private readonly string _plainText;
        private List<TextRange> formatting = new List<TextRange>();

        public BetterFormattedText(string plainText)
        {
            _plainText = plainText ?? throw new ArgumentNullException(nameof(plainText));
        }
        
        // Flyweight object
        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange {Start = start, End = end};
            formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _plainText.Length; i++)
            {
                var c = _plainText[i];
                foreach (TextRange range in formatting)
                {
                    if (range.Covers(i) && range.Capitalize)
                    {
                        c = char.ToUpper(c);
                    }

                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }
    
    public class TextFormatting
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            // old wastes memory on huge arrays for each individual character
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);
            Console.WriteLine(ft);

            Console.WriteLine();

            // new (better) saves memory by using ranges for a range (start and end point) of characters.
            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(bft.ToString());
            Console.WriteLine();
        }
    }
}
