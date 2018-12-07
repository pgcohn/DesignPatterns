using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SOLID
{
    // In this demo the Dependency Inversion Principle is demonstrated. This principle states
    // that all high level modules should not depend on low-level; both should depend on abstractions
    // abstractions should not depend on details; details should depend on abstractions
    // Here, a class Research (renamed _5_DIP) which has methods to query a dataset has been
    // changed so that it can be independent of the low-level structure of the dataset. Instead
    // of passing the class that represents the database, an interface is passed and the database class
    // implements that interface.

    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
        // public DateTime DateOfBirth;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class Relationships : IRelationshipBrowser // low-level
    {
        private List<(Person, Relationship, Person)> relations
          = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public List<(Person, Relationship, Person)> Relations => relations;

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations
              .Where(x => x.Item1.Name == name
                          && x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }
    }

    public class _5_DIP
    {
        //public _5_DIP(Relationships relationships)
        //{
        //    // high - level: find all of john's children
        //    var relations = relationships.Relations;
        //    foreach (var r in relations
        //      .Where(x => x.Item1.Name == "John"
        //                  && x.Item2 == Relationship.Parent))
        //    {
        //        WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}

        public _5_DIP(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                WriteLine($"John has a child called {p.Name}");
            }
        }

        public static void Run()
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Matt" };

            // low-level module
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new _5_DIP(relationships);

        }
    }
}
