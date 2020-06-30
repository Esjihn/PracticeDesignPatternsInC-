using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iterators
{
    public class Node3<T>
    {
        public T Value;
        public Node3<T> Left, Right;
        public Node3<T> Parent;

        public Node3(T value)
        {
            Value = value;
        }

        public Node3(T value, Node3<T> left, Node3<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }
    }

    public class BinaryTree2<T>
    {
        private Node3<T> root;

        public BinaryTree2(Node3<T> root)
        {
            this.root = root;
        }

        // pre-order 123  // Root, Left, Right // 1, 2, 3
        public IEnumerable<Node3<T>> PreOrder
        {
            get
            {
                IEnumerable<Node3<T>> Traverse(Node3<T> current)
                {
                    yield return current;

                    if (current.Left != null)
                    {
                        foreach (var left in Traverse(current.Left))
                        {
                            yield return left;
                        }
                    }

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

    public class IteratorExerciseTest
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            // in-order 213   // Left, Root, Right // clockwise
            // pre-order 123  // Root, Left, Right
            // post-order 231 // Left, Right, Root // counter-clockwise

            var root = new Node3<int>(1, new Node3<int>(2), new Node3<int>(3));
            var tree = new BinaryTree2<int>(root);


            Console.WriteLine(string.Join(",",
                tree.PreOrder.Select(x => x.Value))); // returns 1,2,3
        }
    }
}

