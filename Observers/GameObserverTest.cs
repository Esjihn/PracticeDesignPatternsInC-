using System;
using System.Collections.Generic;
using System.Text;

namespace Observers
{
    /**
     * Imagine a game where one or more rats can attack a player. Each individual rat has an Attack
     * value of 1. However, rats attack as a swarm, so each rat's Attack value is equal to the
     * total number of rats in play.
     *
     * Given that a rat enters play through the constructor and leaves play (dies) via its Dispose method,
     * please implement the Game and Rat classes so that, at any point in the game the attack value of a rat
     * is always consistent
     *
     * The exercise has two simple rules.
     *      a) The Game class cannot have properties or fields. It can only contain events and methods
     *      b) The Rat class' Attack field is strictly a field, not a property.
     */
    public class Game
    {
        // remember - no fields or properties!
        public event EventHandler<EventArgs> RatAttackEvent;

        public void OnRatAttackEvent()
        {
            RatAttackEvent?.Invoke(this, EventArgs.Empty);
        }

        public int RatSubscriberCount()
        {
            if(RatAttackEvent != null)
                return RatAttackEvent.GetInvocationList().Length;
        
            return 0;
        }
    }
    public class Rat : IDisposable
    {
        public int Attack = 1;
        private readonly Game _game;

        public Rat(Game game)
        {
            this._game = game;
            _game.RatAttackEvent += GameOnRatAttackEvent;
        }

        private void GameOnRatAttackEvent(object? sender, EventArgs e)
        {
            if (sender is Game game)
            {
                if (game.RatSubscriberCount() > 1)
                    Attack++;
                else
                {
                    Attack = 1;
                }
            }

            Console.WriteLine($"{this}: {this.GetHashCode()} is now {this.Attack}");
        }


        public void Dispose()
        {
            this._game.RatAttackEvent -= GameOnRatAttackEvent;
        }
    }

    public class GameObserverTest
    {
        // Change to Main to run.
        public static void Main(string[] args)
        {
            var game = new Game();
           
            var rat = new Rat(game); // rat = 1
            game.OnRatAttackEvent();
            rat.Dispose(); // rat goes away
            var rat2 = new Rat(game); // rat 2 = 1
            game.OnRatAttackEvent(); // rat 2 not disposed (does not go away)
            var rat3 = new Rat(game); // rat 2's attack is now ++ 1 + 1 = 2
            game.OnRatAttackEvent(); // rat 3 and rat 2 in pot so both Rat2 and Rat 3 attack = 2 (1 + 1)
            rat3.Dispose(); // rat 3 goes away but rat 2 remains which means only one rat in pot

            
            // Everything has been unsubscribed at this point. 
            game.OnRatAttackEvent(); // Since only 1 rat (rat 2) remains. Then rat2 attack is reduced
                                     // back to 1.
        }
    }
}