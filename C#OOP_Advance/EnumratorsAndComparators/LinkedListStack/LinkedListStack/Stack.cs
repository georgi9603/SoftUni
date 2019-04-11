using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListStack
{
    public class Stack<T> : IEnumerable<T>
    {
        private Node<T> topNode;

        public Stack()
        {
            Node<T> topNode = null;
        }

        private class Node<T>
        {

            public Node(T element)
            {
                topElement = element;
                prevElement = null;
            }

            public T topElement { get; set; }
            public Node<T> prevElement { get; set; }

        }

        public void Push(T element)
        {
            Node<T> newNode = new Node<T>(element);


            if (topNode == null)
            {
                topNode = newNode;
            }
            else
            {
                Node<T> currentTopNode = topNode;
                topNode = newNode;
                topNode.prevElement = currentTopNode;
            }
        }

        public void Pop()
        {
            if (topNode != null)
            {
                Node<T> currentTopNode = topNode;
                topNode = topNode.prevElement;
                currentTopNode = null;
            }
            else
            {
                throw new InvalidOperationException("No elements");
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = topNode;
            while (currentNode != null)
            {
                yield return currentNode.topElement;
                currentNode = currentNode.prevElement;

            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }
    }
}