using System;
using System.Collections.Generic;
using System.Text;

namespace Iterators
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            Left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T>
    {
        public Node<T> Current;
        private readonly Node<T> _root;
        private bool _yieldedStart;

        public InOrderIterator(Node<T> root)
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

    public class IteratorObject
    {
        // change to Main to run.
        public static void Main(string[] args)
        {
            //      1
            //    /    \
            //   2      3

            // in-order 213   // Left, Root, Right // clockwise
            // pre-order 123  // Root, Left, Right
            // post-order 231 // Left, Right, Root // counter-clockwise
            var root = new Node<int>(1,new Node<int>(2), new Node<int>(3));
            var it = new InOrderIterator<int>(root);
            while (it.MoveNext())
            {
                Console.Write(it.Current.Value + " ");
            }

            Console.WriteLine(); // 2 1 3

            it.Reset();

            Console.WriteLine(); // nothing
        }
    }
}
