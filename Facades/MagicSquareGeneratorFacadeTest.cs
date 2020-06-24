using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facades
{
    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => random.Next(1, 6))
                .ToList();
        }
    }
    
    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            return array.All(t => t.Sum() == expected);
        }
    }

    /// <summary>
    /// A magic square is a square matrix whos rows, columns, and diaglonals add up to the same value.
    /// </summary>
    public class MagicSquareGenerator
    {
        readonly Generator _generator = new Generator();
        readonly Splitter _splitter = new Splitter();
        readonly Verifier _verifier = new Verifier();

        public List<List<int>> Generate(int size)
        {
            List<List<int>> resultSet = new List<List<int>>();
            List<int> result = new List<int>();

            while (size > 0)
            {
                result = _generator.Generate(size);
                size--;
            }
            
            resultSet.Add(result);
            _splitter.Split(resultSet);
            _verifier.Verify(resultSet);

            return resultSet;
        }
    }

    public class MagicSquareGeneratorFacadeTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            MagicSquareGenerator msg = new MagicSquareGenerator();
            foreach (List<int> itemList in msg.Generate(1))
            {
                foreach (int i in itemList)
                {
                    Console.Write(i + " ");
                }
            }
        }
    }
}
