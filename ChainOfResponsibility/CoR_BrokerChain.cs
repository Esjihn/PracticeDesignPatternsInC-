using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChainOfResponsibility
{
    // changed all class definitions to "ClassName2" to avoid logic conflicts with MethodChain implementation.
    
    // Combine CoR with Mediator for a better implementation of our Game from CoR_MethodChain. 

    // Mediator / Query API
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender, q);
        }
    }

    public class Query
    {
        public string CreatureName;

        public enum Argument
        {
            Attack, 
            Defense
        }

        public Argument WhatToQuery;

        public int Value;

        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName ?? throw new ArgumentNullException(nameof(creatureName));
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public class Creature2
    {
        public Game _game;
        public string Name;
        public int attack, defense;

        public Creature2(Game game, string name, int attack, int defense)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.attack = attack;
            this.defense = defense;
        }

        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, attack);
                _game.PerformQuery(this, q); // q.Value contains actual attack value.
                return q.Value;
            }
        }

        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, defense);
                _game.PerformQuery(this, q); // q.Value contains actual defense value.
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public abstract class CreatureModifier2 : IDisposable
    {
        protected Game game;
        protected Creature2 creature;

        protected CreatureModifier2(Game game, Creature2 creature)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            this.creature = creature ?? throw new ArgumentNullException(nameof(creature));
            game.Queries += Handle;
        }

        // event handler for events above
        protected abstract void Handle(object sender, Query q);

        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifer2 : CreatureModifier2
    {
        public DoubleAttackModifer2(Game game, Creature2 creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name
            && q.WhatToQuery == Query.Argument.Attack)
            {
                q.Value *= 2;
            }
        }
    }

    public class IncreasedDefenseModifer2 : CreatureModifier2
    {
        public IncreasedDefenseModifer2(Game game, Creature2 creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name
                && q.WhatToQuery == Query.Argument.Defense)
            {
                q.Value += 2;
            }
        }
    }

    public class CoR_BrokerChain
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var game = new Game();
            // real world would use constructor injection from your DI container
            var goblin = new Creature2(game, "Strong Goblin", 3, 3);
            Console.WriteLine(goblin);

            using (new DoubleAttackModifer2(game, goblin))
            {
                Console.WriteLine(goblin);
                using (new IncreasedDefenseModifer2(game, goblin))
                {
                    Console.WriteLine(goblin);
                }
            }
            // reverts spells (expire) after using event broker. 
            Console.WriteLine(goblin);
        }
    }
}
