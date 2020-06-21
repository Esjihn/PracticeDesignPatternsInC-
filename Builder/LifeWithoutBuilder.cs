using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class LifeWithoutBuilder
    {
        // change to main to run.
        public static void none(string[] args)
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("<p>");
            Console.WriteLine(sb);

            // too much complexity to do deeper formatting
            // an HTMLBuilder would be necessary here. 
            // So that you can build HTML objects with a good
            // understandable clear api (succinct)

            string[] words = new []{"hello", "world"};
            sb.Clear();
            sb.Append("<ul>");
            
            foreach (string word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }

            sb.Append("</ul>");
            Console.WriteLine(sb);
        }
    }
}
