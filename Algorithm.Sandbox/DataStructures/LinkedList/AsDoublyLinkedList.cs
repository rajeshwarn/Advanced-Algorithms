﻿using System;

namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class AsDoublyLinkedListNode<T> where T : IComparable
    {
        public AsDoublyLinkedListNode<T> Previous;
        public AsDoublyLinkedListNode<T> Next;
        public T Data;

        public AsDoublyLinkedListNode(T data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// A singly linked list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsDoublyLinkedList<T> where T : IComparable
    {
        public AsDoublyLinkedListNode<T> Head;
        public AsDoublyLinkedListNode<T> Tail;
        //marks this data as the new head
        //cost O(1)
        public AsDoublyLinkedListNode<T> InsertFirst(T data)
        {
            var newNode = new AsDoublyLinkedListNode<T>(data);

            if (Head != null)
            {
                Head.Previous = newNode;
            }

            newNode.Next = Head;
            newNode.Previous = null;

            Head = newNode;

            if (Tail == null)
            {
                Tail = Head;
            }

            return newNode;
        }

        /// <summary>
        /// Inserts the given node
        /// O(1) time complexity
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="newNode"></param>
        internal void InsertFirst(AsDoublyLinkedListNode<T> newNode) 
        {
            if (Head != null)
            {
                Head.Previous = newNode;
            }

            newNode.Next = Head;
            newNode.Previous = null;

            Head = newNode;

            if (Tail == null)
            {
                Tail = Head;
            }

        }


        /// <summary>
        /// Insert right after this node
        /// </summary>
        /// <param name="node"></param>
        public AsDoublyLinkedListNode<T> InsertAfter(AsDoublyLinkedListNode<T> node, AsDoublyLinkedListNode<T> data)
        {
            if (node == null)
                throw new Exception("Empty reference node");

            if (node == Head && node == Tail)
            {
                node.Next = data;
                node.Previous = null;

                data.Previous = node;
                data.Next = null;

                Head = node;
                Tail = data;

                return data;
            }

            if (node != Tail)
            {
                data.Previous = node;
                data.Next = node.Next;

                node.Next.Previous = data;
                node.Next = data;
            }
            else
            {
                data.Previous = node;
                data.Next = null;

                node.Next = data;
                Tail = data;
            }

            return data;
        }

        /// <summary>
        /// Insert right before this node
        /// </summary>
        /// <param name="node"></param>
        public AsDoublyLinkedListNode<T> InsertBefore(AsDoublyLinkedListNode<T> node, AsDoublyLinkedListNode<T> data)
        {
            if (node == null)
                throw new Exception("Empty node");

            if (node == Head && node == Tail)
            {
                node.Previous = data;
                node.Next = null;
                Tail = node;

                data.Previous = null;
                data.Next = node;

                Head = data;

                return data;

            }

            if (node == Head)
            {
                data.Previous = null;
                data.Next = node;

                node.Previous = data;
                Head = data;

            }
            else
            {
                data.Previous = node.Previous;
                data.Next = node;

                node.Previous.Next = data;
                node.Previous = data;

            }


            return data;
        }

        //insert at the end
        //costs O(1)
        public AsDoublyLinkedListNode<T> InsertLast(T data)
        {
            if (Tail == null)
            {
                return InsertFirst(data);
            }

            var newNode = new AsDoublyLinkedListNode<T>(data);

            Tail.Next = newNode;

            newNode.Previous = Tail;
            newNode.Next = null;

            Tail = newNode;

            return newNode;
        }


        //cost O(1)
        public T DeleteFirst()
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            var headData = Head.Data;

            if (Head == Tail)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Head.Next.Previous = null;
                Head = Head.Next;
            }

            return headData;
        }

        //cost O(1)
        public T DeleteLast()
        {
            if (Tail == null)
            {
                throw new Exception("Empty list");
            }

            var tailData = Tail.Data;

            if (Tail == Head)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Tail.Previous.Next = null;
                Tail = Tail.Previous;
            }

            return tailData;
        }

        /// <summary>
        /// search for first T and delete
        /// </summary>
        /// <param name="data"></param>
        //cost O(n) in worst case O(nlog(n) average?
        public void Delete(T data)
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            //eliminate single element list possibility
            if (Head == Tail)
            {
                if (Head.Data.CompareTo(data) == 0)
                {
                    DeleteFirst();
                }

                return;
            }

            //from here logic assumes atleast two elements in list

            var current = Head;

            while (current != null)
            {
                if (current.Data.CompareTo(data) == 0)
                {
                    //current is the first element
                    if (current.Previous == null)
                    {
                        current.Next.Previous = null;
                        Head = current.Next;
                    }
                    //current is the last element
                    else if (current.Next == null)
                    {
                        current.Previous.Next = null;
                        Tail = current.Previous;
                    }
                    //current is somewhere in the middle
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                    }
                    break;
                }

                current = current.Next;
            }

        }

        /// <summary>
        /// deletes this given node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public void Delete(AsDoublyLinkedListNode<T> node)
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            //only one element
            if (node == Head && node == Tail)
            {
                Head = null;
                Tail = null;
                return;

            }

            //node is head
            if (node == Head)
            {
                node.Next.Previous = null;
                Head = node.Next;

            }
            //node is tail
            else if (node == Tail)
            {
                node.Previous.Next = null;
                Tail = node.Previous;
            }
            //current is somewhere in the middle
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
            }

        }
        //O(n) always
        public int Count()
        {
            var i = 0;
            var current = Head;
            while (current != null)
            {
                i++;
                current = current.Next;
            }

            return i;
        }

  
        //O(1) always
        public bool IsEmpty()
        {
            return Head == null;
        }

        //O(1) always
        public void DeleteAll()
        {
            if (Head == null)
            {
                throw new System.Exception("Empty list");
            }

            Head = null;
            Tail = null;
        }

        //O(n) time complexity
        public AsArrayList<T> GetAllNodes()
        {
            var result = new AsArrayList<T>();

            var current = Head;
            while (current != null)
            {
                result.AddItem(current.Data);
                current = current.Next;
            }

            return result;
        }
    }
}
