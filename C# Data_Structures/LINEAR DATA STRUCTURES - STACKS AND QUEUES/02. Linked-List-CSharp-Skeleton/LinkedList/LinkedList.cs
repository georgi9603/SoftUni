using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }

    public Node head;
    public Node tail;

    public void AddFirst(T item)
    {
        Node newNode = new Node(item);

        if (this.Count == 0)
        {
            this.head = newNode;
            this.tail = newNode;
        }
        else
        {
            Node oldNode = this.head;
            this.head = newNode;
            this.head.Next = oldNode;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        Node newNode = new Node(item);

        if (this.Count == 0)
        {
            this.head = newNode;
            this.tail = newNode;
        }
        else
        {
            Node oldNode = this.tail;
            this.tail = newNode;
            oldNode.Next = this.tail;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        Node oldNode = this.head;
        T value = oldNode.Value;
        oldNode = null;
        this.head = this.head.Next;
        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        else if (Count == 1)
        {
            T value = this.tail.Value;
            this.tail = null;
            Count--;
            return value;
        }
        else
        {
            Node lastSecondToLastNode = GetLastToSecondNode();
            this.tail = lastSecondToLastNode;
            T value = lastSecondToLastNode.Next.Value;
            lastSecondToLastNode.Next = null;
            Count--;
            return value;
        }
    }

    private Node GetLastToSecondNode()
    {
        Node currentHeadNode = this.head;

        while (currentHeadNode.Next.Next != null)
        {
            currentHeadNode = currentHeadNode.Next;
        }

        return currentHeadNode;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node currentNode = this.head;
        while (currentNode.Next != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }
}
