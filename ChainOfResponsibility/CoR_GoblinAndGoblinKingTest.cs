using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ChainOfResponsibility
{
    public class Goblin
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
    }

    public class GoblinKing : Goblin
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
    }

    public class Game2
    {
        public class Creatures
        {
            // terrible Idea need to accept both a type of goblin or Goblin king in a single
            // add and remove method. 
            public static void Add(Goblin goblin)
            {
                throw new NotImplementedException();
            }

            public static void Add(GoblinKing goblin)
            {
                throw new NotImplementedException();
            }

            public static void Remove(Goblin goblin)
            {
                throw new NotImplementedException();
            }

            public static void Remove(GoblinKing goblin)
            {
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// You are given a game scenario with classes Goblin and GoblinKing. Please implement the following rules.
    /// 1) A goblin has base 1 attack / 1 defense (1/1), a goblin king is 3/3
    /// 2) When the Goblin King is in play, every other goblin gets +1 Attack
    /// 3) Goblins get +1 to Defense for every other Goblin in play (a Goblin King is a Goblin)
    ///
    /// Example:
    ///     Suppose you have 3 ordinary goblins in play. Each one is a 1/3 (1/1 + 0/2 defense bonus)
    ///     A goblin king comes into play. Now every goblin is a 2/4 (1/1 + 0/3 defense bonus from each other
    ///     + 1/0 from goblin king
    /// The state of all goblins has to be consistent as goblins are added and removed from the game. 
    /// </summary>
    public class CoR_GoblinAndGoblinKingTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            
        }
    }

    // Example test
    //[TestFixture]
    //public class GameTesting
    //{
    //    [Test]
    //    public void GameTest()
    //    {
    //        var game = new Game2();
    //        var goblin = new Goblin();
    //        Game2.Creatures.Add(goblin);
    //        AssertThat(goblin.Attack, Is.EqualTo(1));
    //        AssertThat(goblin.Defense, Is.EqualTo(1));
    //    }
    //}
}
