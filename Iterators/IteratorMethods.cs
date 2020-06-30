using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iterators
{
    public class Node2<T>
    {
        public T Value;
        public Node2<T> Left, Right;
        public Node2<T> Parent;

        public Node2(T value)
        {
            Value = value;
        }

        public Node2(T value, Node2<T> left, Node2<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            Left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator2<T>
    {
        // changing below from field to property gives us iteration
        public Node2<T> Current { get; set; }
        private readonly Node2<T> _root;
        private bool _yieldedStart;

        public InOrderIterator2(Node2<T> root)
        {
            this._root = root;
            Current = root;
            while (Current.Left != null)
            {
                Current = Current.Left;
            }
            //    1 <- root
            //   / \
            //  2   3
            //  ^ Current
        }

        public bool MoveNext()
        {
            if (!_yieldedStart)
            {
                _yieldedStart = true;
                return true;
            }

            if (Current.Right != null)
            {
                Current = Current.Right;
                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }

            var p = Current.Parent;
            while (p != null && Current == p.Right)
            {
                Current = p;
                p = p.Parent;
            }

            Current = p;
            return Current != null;
        }

        public void Reset()
        {
            Current = _root;
            _yieldedStart = false;
        }
    }

    // add Binary tree and IEnumerable (gives you linq)
    public class BinaryTree<T>
    {
        private Node2<T> root;

        public BinaryTree(Node2<T> root)
        {
            this.root = root;
        }

        // added after changing field to property Node<int> Current {get; set;}
        public InOrderIterator2<T> GetEnumerator()
        {
            return new InOrderIterator2<T>(root);
        }

        // recursively
        public IEnumerable<Node2<T>> InOrder
        {
            get
            {
                IEnumerable<Node2<T>> Traverse(Node2<T> current)
                {
                    if (current.Left != null)
                    {
                        foreach (var left in Traverse(current.Left))
                        {
                            yield return left;
                        }
                    }

                    yield return current;

                    if (current.Right != null)
                    {
                        foreach (var right in Traverse(current.Right))
                        {
                            yield return right;
                        }
                    }
                }

                foreach (var node in Traverse(root))
                {
                    yield return node;
                }
            }
        }
    }

    public class IteratorMethods
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var root = new Node2<int>(1, new Node2<int>(2),new Node2<int>(3));
            var tree = new BinaryTree<int>(root);
            
            //Console.WriteLine(string.Join(",",
               //tree.InOrder.Select(x => x.Value)));
               
            // Duck typing foreach doesnt care if you have IEnumerable<T>
            // asks if you have a current node, and a MoveNext()
            foreach (var node in tree)
            {
                Console.WriteLine(node.Value);
            }
        }
    }
}
