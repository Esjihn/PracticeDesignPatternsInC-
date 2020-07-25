using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateMethodPattern
{
    public static class GameTemplate
    {
        // replace virtual overrides
        public static void Run(
            Action start,
            Action takeTurn,
                Func<bool> haveWinner,
                Func<int> winningPlayer)
        {
            start();
            while (!haveWinner())
            {
                takeTurn();
            }

            Console.WriteLine($"Player {winningPlayer()} wins.");
        }
    }

    public class FunctionalTemplateMethod
    {
        // change to Main to run.
        public static void none(string[] args)
        {

            // Purely functional, no extra classes. So data is represented by variables instead
            var numberOfPlayers = 2;
            int currentPlayer = 0;
            int turn = 1, maxTurns = 10;

            void Start()
            {
                Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
            }

            bool HaveWinner()
            {
                return turn == maxTurns;
            }

            void TakeTurn()
            {
                Console.WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            int WinningPlayer()
            {
                return currentPlayer;
            }

            // run Functional Template method instead of using inheritance.
            // Methods within methods, less structured, may require static methods. 
            GameTemplate.Run(Start, TakeTurn, HaveWinner, WinningPlayer);
        }
    }
}
