using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ChainOfResponsibility
{
    public abstract class Creature3
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public string Name { get; set; }
    }

    public class Goblin : Creature3
    {
        private readonly Game3 _game;

        public Goblin(Game3 game)
        {
            _game = game;
            this.Attack = 1;
            this.Defense = 1;
            this.Name = "Goblin";
        }
    }

    public class GoblinKing : Goblin
    {
        private readonly Game3 _game;

        public GoblinKing(Game3 game) : base(game)
        {
            _game = game;
            this.Attack = 3;
            this.Defense = 3;
            this.Name = "Goblin King";
        }
    }

    public class Game3 
    {
        public static IList<Creature3> Creatures;

        public Game3()
        {
            Creatures = new List<Creature3>();
        }

        public static void CreatureUpdate()
        {
            int gobKingCounter = 0;
            int gobCounter = 0;
            for (var i = 0; i < Creatures.Count; i++)
            {
                Creature3 c = Creatures[i];

                if (c.Name == "Goblin King")
                {
                    gobKingCounter++;
                }

                if (c.Name == "Goblin")
                {
                    gobCounter++;
                }
            }

            for (int i = 0; i < Creatures.Count; i++)
            {
                Creature3 c = Creatures[i];
                if (gobKingCounter > 0 && gobCounter > 0)
                {
                    c.Attack += 1;
                    c.Defense += gobCounter;
                }
                else
                {
                    c.Attack = 1;
                    c.Defense = 1;
                    c.Defense += gobCounter -1;
                }
            }
        }

        public void PrintGame()
        {
            foreach (Creature3 creature3 in Game3.Creatures)
            {
                Console.WriteLine($"{creature3.Name}: {creature3.Attack}: {creature3.Defense} ");
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
            Game3 game = new Game3();
            Goblin goblin = new Goblin(game);
            Goblin goblin2 = new Goblin(game);
            Goblin goblin3 = new Goblin(game);
            Game3.Creatures.Add(goblin);
            Game3.Creatures.Add(goblin2);
            Game3.Creatures.Add(goblin3);

            GoblinKing gk = new GoblinKing(game);
            //GoblinKing gk2 = new GoblinKing(game);
            //GoblinKing gk3 = new GoblinKing(game);

            Game3.Creatures.Add(gk);
            //Game3.Creatures.Add(gk2);
            //Game3.Creatures.Add(gk3);

            Game3.CreatureUpdate(); // goblin 2/4 3 goblin 1/1 + 0/2 buff + 1/0 for king
            game.PrintGame();
            Game3.Creatures.Remove(gk);
            Game3.CreatureUpdate(); // king leaves -1 attack to all
            Game3.Creatures.Remove(goblin3); // 1 goblin leaves -1 defense to remaining goblins.
            Game3.CreatureUpdate();

            game.PrintGame();

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
