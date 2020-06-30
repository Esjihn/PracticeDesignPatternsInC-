using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediators
{
    public class Person
    {
        public string Name;
        public ChatRoom Room;
        private List<string> chatLog = new List<string>();

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Say(string message)
        {
            Room.BroadCast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            chatLog.Add(s);
            Console.WriteLine($"[{Name}'s chat session] {s}");
        }
    }

    // Chat room mediates the messages. 
    public class ChatRoom
    {
        private List<Person> people = new List<Person>();

        public void Join(Person p)
        {
            string joinMsg = $"{p.Name} joins the chat";
            BroadCast("room", joinMsg);

            p.Room = this;
            people.Add(p);
        }

        public void BroadCast(string source, string message)
        {
            foreach (Person p in people)
            {
                if (p.Name != source)
                {
                    p.Receive(source, message);
                }
            }
        }

        public void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)
                ?.Receive(source, message);
        }
    }

    public class MediatorChatRoom
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var room  = new ChatRoom();

            var john = new Person("john");
            var jane = new Person("jane");

            room.Join(john);
            room.Join(jane);

            john.Say("hi");
            jane.Say("oh, hey john");

            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("hi everyone!");

            jane.PrivateMessage("Simon", "glad you could join us!");
        }
    }
}
