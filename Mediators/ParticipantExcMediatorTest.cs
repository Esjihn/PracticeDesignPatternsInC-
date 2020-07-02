using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediators
{
    // Our system has any number of instances of Participant classes. Each Participant has a Value integer
    // initially zero. A participant can Say() a particular value, which is broadcast to all other
    // participants At this point in time, every other participant is obliged to increase their
    // Value by the value being broadcast.

    // Example
    // Two participants start with values 0 and 0 respectively.
    // Participant 1 broadcasts the value 3. We now have Participant 1 value = 0, Participant 2 value = 3
    // Participant 2 broadcasts the value 2. We now have Participant 1 value = 2, Participant 2 value = 3.

    public class Participant
    {
        public string Name { get; set; }
        public int Value { get; set; }
        private Mediator _mediator;
        public Participant(string name, Mediator mediator)
        {
            // todo
            Name = name;
            _mediator = mediator;
            mediator.AddParticipant(this);
        }

        public void Say(int n)
        {
            // todo
            Console.WriteLine($"I am {this.Name} current value is: {Value}\n"
                                + $"Now saying {n}! Mediator please broadcast updated values!");
            _mediator.EchoValueAfterChange(this.Name, n);
        }
    }

    public class Mediator
    {
        List<Participant> list = new List<Participant>();

        public void AddParticipant(Participant p)
        {
            list.Add(p);
        }

        public void EchoValueAfterChange(string name, int n)
        {
            if (list != null)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    Participant participant = list[i];
                    if (participant.Name != name)
                    {
                        list[i].Value = n;
                    }
                }

                foreach (var participant in list)
                {
                    Console.WriteLine($"Mediator: {participant.Name}: {participant.Value}");
                }
            }
        }
    }

    public class ParticipantExcMediatorTest
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var mediator = new Mediator();
            var person1 = new Participant("person1", mediator);
            var person2 = new Participant("person2", mediator);
            
            person1.Say(2);
            person2.Say(4);
            // mediator should broad cast results from Participant constructor
            

        }
    }
}
