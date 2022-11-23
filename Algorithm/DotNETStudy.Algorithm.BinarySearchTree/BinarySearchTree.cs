using System;
using System.Collections.Generic;

namespace DotNETStudy.Algorithm.BinarySearchTree
{
    public class BinarySearchTree<E> where E : IComparable<E>
    {
        private int size;
        private Node<E> root;

        public int Size()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Clear()
        {

        }

        public void Add(E element)
        {
            ElementNotNullCheck(element);

            if (root == null)
            {
                root = new Node<E>(element, null);
                size++;
                return;
            }

            Node<E> parent = root;
            Node<E> node = root;
            int cmp = 0;

            while (node != null)
            {
                parent = node;
                cmp = element.CompareTo(node.Element);
                if (cmp > 0)
                {
                    node = node.Right;
                }
                else if (cmp < 0)
                {
                    node = node.Left;
                }
                else
                {
                    return;
                }
            }

            Node<E> newNode = new Node<E>(element, parent);

            if (cmp > 0)
            {
                parent.Right = newNode;
            }
            else
            {
                parent.Left = newNode;
            }
            size++;
        }

        public void Remove(E element)
        {
            Remove(FindNode(element));
        }

        private void Remove(Node<E> node)
        {
            if (node == null)
            {
                return;
            }



            size--;
        }

        private Node<E> FindNode(E element)
        {
            Node<E> node = root;
            while (node != null)
            {
                int cmp = element.CompareTo(node.Element);
                if (cmp == 0)
                {
                    return node;
                }
                if (cmp > 0)
                {
                    node = node.Right;
                }
                else
                {
                    node = node.Left;
                }
            }

            return null;
        }

        public bool IsLeaf(Node<E> node)
        {
            if (node == null)
            {
                return false;
            }
            return node.Left == null && node.Right == null;
        }

        public bool HasTwoChildren(Node<E> node)
        {
            if (node == null)
            {
                return false;
            }
            return node.Left != null && node.Right != null;
        }

        public bool Contains(E element)
        {
            return false;
        }


        public void PreorderTraversal()
        {
            PreorderTraversal(root);
        }

        public void PreorderTraversal(Node<E> node)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine(node.Element);
            PreorderTraversal(node.Left);
            PreorderTraversal(node.Right);
        }

        public void InorderTraversal()
        {
            InorderTraversal(root);
        }

        public void InorderTraversal(Node<E> node)
        {
            if (node == null)
            {
                return;
            }

            InorderTraversal(node.Left);
            Console.WriteLine(node.Element);
            InorderTraversal(node.Right);
        }

        public void PostorderTraversal()
        {
            PostorderTraversal(root);
        }

        public void PostorderTraversal(Node<E> node)
        {
            PostorderTraversal(node.Left);
            PostorderTraversal(node.Right);
            Console.WriteLine(node.Element);
        }

        public void LevelOrderTraversal()
        {
            if (root == null)
            {
                return;
            }

            Queue<Node<E>> queue = new Queue<Node<E>>();
            queue.Enqueue(root);

            Node<E> current;
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                Console.WriteLine(current.Element);
                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }
        }

        private void ElementNotNullCheck(E element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element must not be null");
            }
        }
    }
}
