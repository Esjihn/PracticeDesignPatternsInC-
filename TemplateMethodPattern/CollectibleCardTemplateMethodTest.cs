using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateMethodPattern
{
    /*
     * Imagine a typical collectible card game which has cards representing creatures. Each creature has two values: Attack and Health.
     * Creatures can fight each other, dealing their Attack damage, thereby reducing their opponent's health.
     *
     * The class CardGame implements the logic for two creatures fighting one another. However, the exact mechanics of how damage is dealt
     * is different:
     *
     *      1) TemporaryCardDamage: In some games (e.g. Magic: the Gathering), unless the creature has been killed, its health returns
     *          to the original value at the end of combat.
     *      2) PermanentCardDamage: In other games (e.g. Hearthstone), health damage persists.
     *
     * You are asked to implement classes TemporaryCardDamageGame and PermanentCardDamageGame that would allow us to simulate combat between
     * creatures.
     *
     * Some examples:
     *      1) With temporary damage, creatures 1/2 and 1/3 can number kill one another. With permanent damage, the second creature will win
     *          after 2 rounds of combat.
     *      2) With either temporary or permanent damage, two 2/2 creatures kill one another.
     */

    public class Creature
    {
        public int Attack, Health;

        public Creature(int attack, int health)
        {
            Attack = attack;
            Health = health;
        }
    }

    public abstract class CardGame
    {
        public Creature[] Creatures;

        public CardGame(Creature[] creatures)
        {
            Creatures = creatures;
        }

        // returns -1 if no clear winner (both alive or both dead)
        // int is the index not the health or attack
        public int Combat(int creature1, int creature2)
        {
            Creature first = Creatures[creature1];
            Creature second = Creatures[creature2];
            Hit(first, second);
            Hit(second, first);
            Console.WriteLine("***Both rounds have ended***\n");
            bool firstAlive = first.Health > 0;
            bool secondAlive = second.Health > 0;
            if (firstAlive == secondAlive) return -1;
            return firstAlive ? creature1 : creature2;
        }

        // attacker hits other creature
        protected abstract void Hit(Creature attacker, Creature other);
    }

    public class TemporaryCardDamageGame : CardGame
    {
        public TemporaryCardDamageGame(Creature[] creatures) : base(creatures)
        {
            Console.WriteLine("Temp Damage Card Game: \n");

            // creature index 0 and 1 in the Creature[]
            UserInterface.DisplayGameResults(creatures, this.Combat(0, 1));
        }

        protected override void Hit(Creature attacker, Creature other)
        {
            Console.WriteLine($"Other creature {other.GetHashCode()} has {other.Attack} attack power, and {other.Health} health.");
            Console.WriteLine($"Attacking creature {attacker.GetHashCode()} has {attacker.Attack} attack power, and {attacker.Health} health.");
            Console.WriteLine($"Commencing battle...");
            var otherHealth = other.Health;
            var attackerHealth = attacker.Health;

            other.Health = attacker.Attack > other.Health ? attacker.Attack - other.Health : other.Health - attacker.Attack;
            attacker.Health = other.Attack > attacker.Health ? other.Attack - attacker.Health : attacker.Health - other.Attack;

            if (other.Health > 0 && attacker.Health > 0)
            {
                // return health to stored values since damage is temporary if none die. 
                other.Health = otherHealth;
                attacker.Health = attackerHealth;
                Console.WriteLine("No creature dies restoring health to max before the next round.");
            }

            Console.WriteLine($"Other Creature {other.GetHashCode()} is at {other.Health} health");
            Console.WriteLine($"Attacking Creature {attacker.GetHashCode()} is at {attacker.Health} health");
        }
    }

    public static class UserInterface
    {
        public static void DisplayGameResults(Creature[] creatures, int combatResult)
        {
            switch (combatResult)
            {
                case 0:
                    Console.WriteLine($"Creature {creatures[0].GetHashCode()} wins.");
                    Console.WriteLine($"Creature {creatures[1].GetHashCode()} loses.\n");
                    break;
                case 1:
                    Console.WriteLine($"Creature {creatures[1].GetHashCode()} wins.");
                    Console.WriteLine($"Creature {creatures[0].GetHashCode()} loses.\n");
                    break;
                default:
                    break;
            }
        }
    }

    public class PermanentCardDamage : CardGame
    {
        public PermanentCardDamage(Creature[] creatures) : base(creatures)
        {
            Console.WriteLine("Permanent Damage Card Game: \n");
            
            // creature index 0 and 1 in the Creature[]
            UserInterface.DisplayGameResults(creatures, this.Combat(0, 1));
        }

        protected override void Hit(Creature attacker, Creature other)
        {
            Console.WriteLine($"Other creature {other.GetHashCode()} has {other.Attack} attack power, and {other.Health} health.");
            Console.WriteLine($"Attacking creature {attacker.GetHashCode()} has {attacker.Attack} attack power, and {attacker.Health} health.");

            other.Health = attacker.Attack > other.Health ? attacker.Attack - other.Health : other.Health - attacker.Attack;
            attacker.Health = other.Attack > attacker.Health ? other.Attack - attacker.Health : attacker.Health - other.Attack;
            Console.WriteLine($"Commencing battle...");

            Console.WriteLine($"Other Creature {other.GetHashCode()} is at {other.Health} health");
            Console.WriteLine($"Attacking Creature {attacker.GetHashCode()} is at {attacker.Health} health");
        }
    }

    public class CollectibleCardTemplateMethodTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            var creatures = new Creature[]
            {
                new Creature(1,2),
                new Creature(1,3)
            };

            new TemporaryCardDamageGame(creatures);
            new PermanentCardDamage(creatures);

        }
    }
}
