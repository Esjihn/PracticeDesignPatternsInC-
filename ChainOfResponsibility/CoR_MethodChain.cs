using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibility
{
    // Classic implementation
    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature?? throw new ArgumentNullException(nameof(creature));
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null)
            {
                next.Add(cm);
            }
            else
            {
                next = cm;
            }
        }

        public virtual void Handle()
        {
            if(next != null)
                next.Handle();
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreasedDefenseModifer : CreatureModifier
    {
        public IncreasedDefenseModifer(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
            base.Handle();
        }
    }

    public class SilenceModifier : CreatureModifier
    {
        public SilenceModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Casting silence on {creature.Name}, {creature.Name} cannot be buffed.");
        }
    }

    public class CoR_MethodChain
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);
            var root = new CreatureModifier(goblin);

            // casts protection spell where it cannot be buffed (silenced) COOL!!!!!!!!!!!!!
            // prevents walk of linked list aka no chain of responsibility traversal.  
            root.Add(new SilenceModifier(goblin)); 

            Console.WriteLine("Let's double the goblin's attack");
            // adding another modifier to the chain of responsibility
            root.Add(new DoubleAttackModifier(goblin));
            Console.WriteLine("Let's increase the goblin's defense");
            root.Add(new IncreasedDefenseModifer(goblin));
            root.Handle();
            Console.WriteLine(goblin);

            // Downside - method chain implemented this way permanently changes the creature
            // so that modifiers cannot be removed. We would have to follow the entire linked list
            // and then recalculate original state of creature. 
            // See CoR_BrokerChain for better implementation.
        }
    }
}
