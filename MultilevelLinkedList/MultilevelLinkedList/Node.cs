using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilevelLinkedList
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Child { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Child = null;
        }

        public Node()
        {
            Next = null;
            Child = null;
        }

        public void AddNext(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (Next != null)
            {
                newNode.Next = Next;
            }

            Next = newNode;
        }

        public void AddNext(Node<T> newNode)
        {
            if (Next != null)
            {
                Node<T> lastNext = newNode;

                while (lastNext != null)
                {
                    if (lastNext.Next == null)
                    {
                        lastNext.Next = Next;
                        Next = newNode;
                        return;
                    }
                    lastNext = lastNext.Next;
                }
            }

            Next = newNode;
        }

        public void AddChild(Node<T> newNode)
        {
            if (Child != null)
            {
                Node<T> lastChild = newNode;

                while (lastChild != null)
                {
                    if (lastChild.Child == null)
                    {
                        lastChild.Child = Child;
                        Child = newNode;
                        return;
                    }
                    lastChild = lastChild.Child;
                }
            }

            Child = newNode;
        }

        public void AddChild(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (Child != null)
            {
                newNode.Child = Child;
            }

            Child = newNode;
        }

        public void DeleteNext()
        {
            if (Next != null)
            {
                Next = Next.Next;
            }
            else
            {
                Next = null;
            }
        }

        public void DeleteChild()
        {

            if (Child != null)
            {
                Child = Child.Child;
            }
            else
            {
                Child = null;
            }
        }
    }
}
