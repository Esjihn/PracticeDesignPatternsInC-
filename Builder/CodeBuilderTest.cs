using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Builder
{
    public class CodeElement
    {
        public string Class, Name, Text;
        public List<CodeElement> Elements = new List<CodeElement>();

        public const int indentTab = 2;

        public CodeElement() { }

        public CodeElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ',indentTab * indent);

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.AppendLine($"public class {Class}")
                    .Append("{" + Environment.NewLine);
                Class = string.Empty;
            }
            else
            {
                sb.AppendLine($"{i} public {Text} {Name}");
            }

            for(int j = 0; j < Elements.Count; j++)
            {
                sb.Append(Elements[j].ToStringImpl(indent + 1));
                
                if (j == Elements.Count - 1)
                    sb.Append("}" + Environment.NewLine);
            }
            
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class CodeBuilder
    {
        private readonly string _rootName;
        CodeElement _root = new CodeElement();

        public CodeBuilder(string rootName)
        {
            _rootName = rootName;
            _root.Class = rootName;
        }

        public CodeBuilder AddField(string childName, string childText)
        {
            var e = new CodeElement(childName, childText);
            _root.Elements.Add(e);

            return this;
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        public void Clear()
        {
            _root = new CodeElement{Name = _rootName};
        }
    }

    public class CodeBuilderTest
    {
        public static void Main(string[] args)
        {
            var codeBuilder = new CodeBuilder("Person")
                .AddField("Name", "string")
                .AddField("Age", "int");

            Console.WriteLine(codeBuilder);

            var codeBuilder2 = new CodeBuilder("People")
                .AddField("Name", "string")
                .AddField("Age", "int");

            Console.WriteLine(codeBuilder2);

            var playstation5 = new CodeBuilder("Sony")
                .AddField("IsDigitalEdition", "bool")
                .AddField("ModelNumber", "int");

            Console.WriteLine(playstation5);
        }
    }
}
