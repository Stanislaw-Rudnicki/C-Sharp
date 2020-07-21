using System;
using System.Collections;
using System.Collections.Generic;

namespace Generics.BinaryTrees
{
     public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        public BinaryTree<T> Root { get; private set; }
        public BinaryTree<T> Left { get; private set; }
        public BinaryTree<T> Right { get; private set; }
        public T Value { get; private set; }

        public void Add(T value)
        {
            if (Root == null)
            {
                Value = value;
                Root = this;
                return;
            }

            var current = Root;
            while (current != null)
            {
                if (value.CompareTo(current.Value) > 0)
                {
                    if (current.Right == null)
                    {
                        current.Right = new BinaryTree<T> { Value = value };
                        return;
                    }
                    current = current.Right;
                }
                else
                {
                    if (current.Left == null)
                    {
                        current.Left = new BinaryTree<T> { Value = value };
                        return;
                    }
                    current = current.Left;
                }
            }
        }

        IEnumerable<T> GoThroughTree()
        {
            if (Root == null) yield break;

            var stack = new Stack<BinaryTree<T>>();
            var current = Root;

            while (stack.Count > 0 || current != null)
            {
                if (current == null)
                {
                    current = stack.Pop();
                    yield return current.Value;
                    current = current.Right;
                }
                else
                {
                    stack.Push(current);
                    current = current.Left;
                }
            }
        }

        public IEnumerator<T> GetEnumerator() =>
            GoThroughTree().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    public class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] arr) where T : IComparable
        {
            var res = new BinaryTree<T>();
            foreach (var el in arr)
                res.Add(el);
            return res;
        }
    }
}