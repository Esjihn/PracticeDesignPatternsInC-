using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SOLID
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // Memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        public static Journal Load(string filename)
        {
            return null;
        }

        public void Load(Uri uri)
        {

        }
    }

    /// <summary>
    /// Separates persistence out of Journal class for Single Responsibility
    /// </summary>
    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, j.ToString());
            }
        }
    }

    public class SingleResponsibility
    {
        // change to Main to run
        public static void none(string[] args)
        {
            Journal j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            Console.WriteLine(j);

            Persistence p = new Persistence();
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\file.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
        }
    }
}
