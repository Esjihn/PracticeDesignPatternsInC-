using System;
using System.Collections.Generic;
using System.Text;

namespace Strategies
{
    // Converting Dynamic into Static pattern
    // Defined at compile time and you can no longer change it later on. 

    public enum OutputFormat2
    {
        Markdown,
        Html
    }

    // <ul><li>foo</li></ul>
    public interface IListStrategy2
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }

    public class HtmlListStrategy2 : IListStrategy2
    {
        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($"  <li>{item}</li>");
        }
    }

    public class MarkdownListStrategy2 : IListStrategy2
    {
        public void Start(StringBuilder sb)
        {

        }

        public void End(StringBuilder sb)
        {

        }

        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }
    }

    public class TextProcessor2<LS> where LS : IListStrategy2, new()
    {
        private StringBuilder _sb = new StringBuilder();
        private IListStrategy2 _listStrategy = new LS();



        public void AppendList(IEnumerable<string> items)
        {
            _listStrategy.Start(_sb);

            foreach (string item in items)
            {
                _listStrategy.AddListItem(_sb, item);
            }

            _listStrategy.End(_sb);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

        public StringBuilder Clear()
        {
            return _sb.Clear();
        }
    }

    public class StaticStrategy
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            // cb.Register<MarkdownListStrategy>().As<IListStrategy>() typically done in real world.

            var tp = new TextProcessor2<MarkdownListStrategy2>();
            tp.AppendList(new []{"foo", "bar", "baz"});
            Console.WriteLine(tp);

            var tp2 = new TextProcessor2<HtmlListStrategy2>();
            tp2.AppendList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(tp2);
        }
    }
}
