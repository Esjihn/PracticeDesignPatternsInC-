using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID
{
    public class DependencyInversion
    {
        public enum Relationship
        {
            Parent,
            Child,
            Sibling
        }

        public class Person
        {
            public string Name;
            //public DateTime DateOfBirth;
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        // low-level
        public class Relationships : IRelationshipBrowser
        {
            // tuple
            private List<(Person, Relationship, Person)> 
                relations = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            
            // Horrible Idea higher level dependency on this lower level module. 
            //public List<(Person, Relationship, Person)> Relations
            //{
            //    get
            //    {
            //        return relations;
            //    }
            //}
            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations.Where(x => x.Item1.Name == name
                                            && x.Item2 == Relationship.Parent)
                    .Select(r => r.Item3);
            }
        }

        public class Research
        {
            //public Research(Relationships relationships)
            //{
            //    var relations = relationships.Relations;  // <= lower level property
            //    foreach ((Person, Relationship, Person) r in relations.Where(
            //        x=> x.Item1.Name == "John"
            //        && x.Item2 == Relationship.Parent))
            //    {
            //        Console.WriteLine($"John has a child called {r.Item3.Name}");
            //    }
            //}

            // dont depend on relationships we depend on interface instead so the higher level modules
            // do not depend on the lower level modules that are actually consuming it. 
            public Research(IRelationshipBrowser browser)
            {
                foreach (Person p in browser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {p.Name}");
                }
            }

            public static void Main(string[] args)
            {
                Person parent = new Person {Name = "John"};
                Person child1 = new Person {Name = "Chris"};
                Person child2 = new Person {Name = "Mary"};

                var relationships = new Relationships();
                relationships.AddParentAndChild(parent, child1);
                relationships.AddParentAndChild(parent, child2);

                Research research = new Research(relationships);
            }
        }
    }
}
