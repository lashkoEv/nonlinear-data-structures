using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilevelLinkedList
{
    public class MultiLevelLinkedList<T>
    {
        private Node<T> Head { get; set; }
        private int Size { get; set; }
        private Node<T> Finded { get; set; }
        private Node<T> Prev { get; set; }

        public MultiLevelLinkedList()
        {
            Head = null;
            Size = 0;
            Finded = null;
            Prev = null;
        }

        public void Find(Node<T> node, T data)
        {
            while (node != null)
            {
                if (node.Data.Equals(data))
                {
                    Finded = node;
                }

                if (node.Child != null)
                {
                    Find(node.Child, data);
                }

                node = node.Next;
            }
        }

        public void Print()
        {
            if (Head == null)
            {
                Console.WriteLine("Empty list!");
                return;
            }

            PrintNode(Head);
            PrintSize();
        }

        public void PrintNode(Node<T> node)
        {
            while (node != null)
            {
                Console.Write($"{node.Data}");

                if (node.Child != null)
                {
                    Console.Write(" { ");
                    PrintNode(node.Child);
                    Console.Write(" } ");
                }

                node = node.Next;
                if (node != null)
                {
                    Console.Write(" => ");
                }
            }
        }

        public void PrintSize()
        {
            PrintSizeNode(Head);
            Console.WriteLine($"\nSize: {Size}");
            Size = 0;
        }

        public void PrintSizeOfSpecialNode(T data)
        {
            Find(Head, data);

            if (Finded != null)
            {
                PrintSizeNode(Finded);
                Console.WriteLine($"\nSize of {data}: {Size}");
                Size = 0;
            }
        }

        private void PrintSizeNode(Node<T> node)
        {
            while (node != null)
            {
                Size++;

                if (node.Child != null)
                {
                    PrintSizeNode(node.Child);
                }

                node = node.Next;
            }
        }

        public void AddNext(T prev, T data)
        {
            if (Head == null)
            {
                Head = new Node<T>(data);
                return;
            }

            Find(Head, prev);

            if (Finded == null)
            {
                Console.WriteLine("This item is not in the list!");
                return;
            }

            Finded.AddNext(data);
            Finded = null;
        }

        public void AddChild(T node, T data)
        {
            if (Head == null)
            {
                Head = new Node<T>(data);
                return;
            }

            Find(Head, node);

            if (Finded == null)
            {
                Console.WriteLine("This item is not in the list!");
                return;
            }

            Finded.AddChild(data);
            Finded = null;
        }

        public void Clear()
        {
            Head = null;
        }

        public MultiLevelLinkedList<T> Copy()
        {
            MultiLevelLinkedList<T> copy = new MultiLevelLinkedList<T>();
            CopyNodeNext(copy, Head, new Node<T>());
            return copy;
        }

        private void CopyNodeNext(MultiLevelLinkedList<T> copy, Node<T> node, Node<T> prev)
        {
            while (node != null)
            {
                copy.AddNext(prev.Data, node.Data);

                if (node.Child != null)
                {
                    CopyNodeChild(copy, node.Child, node);
                }

                prev = node;
                node = node.Next;
            }
        }

        private void CopyNodeChild(MultiLevelLinkedList<T> copy, Node<T> node, Node<T> prev)
        {
            while (node != null)
            {
                copy.AddChild(prev.Data, node.Data);

                if (node.Next != null)
                {
                    CopyNodeNext(copy, node.Next, node);
                }

                prev = node;
                node = node.Child;
            }
        }

        public void DeleteElement(T data)
        {
            DeleteElement(Head, data, new Node<T>());

            if (Finded == null)
            {
                Console.WriteLine("This item is not in the list!");
                return;
            }

            if (Finded.Data.Equals(Head.Data))
            {
                Clear();
                return;
            }

            if (Prev.Next != null && Prev.Next.Data.Equals(data))
            {
                Prev.Next = null;
            }

            if (Prev.Child != null && Prev.Child.Data.Equals(data))
            {
                if (Prev.Child.Next != null)
                {
                    Prev.Child = Prev.Child.Next;
                }
                else
                {
                    Prev.DeleteChild();
                }
            }

            Prev = null;
            Finded = null;
        }

        private void DeleteElement(Node<T> node, T data, Node<T> prev)
        {
            while (node != null)
            {
                if (node.Data.Equals(data))
                {
                    Finded = node;
                    Prev = prev;
                }

                if (node.Child != null)
                {
                    DeleteElement(node.Child, data, node);
                }

                prev = node;
                node = node.Next;
            }
        }

        public void DeleteBranch(T data)
        {
            Find(Head, data);

            if (Finded != null)
            {
                Finded.Child = null;
                Finded = null;
            }
        }

        public void DeleteLevel(int level)
        {
            if (level == 0)
            {
                Clear();
                return;
            }

            DeleteLevelNode(0, level - 2, Head);
        }

        public void DeleteLevelNode(int curLvl, int lvl, Node<T> node)
        {
            while (node != null)
            {
                if (curLvl == lvl)
                {
                    node.Child = null;
                }
                else
                {
                    if (node.Child != null)
                    {
                        int q = curLvl + 1;
                        DeleteLevelNode(q, lvl, node.Child);
                    }
                }

                node = node.Next;
            }
        }

        public void Move(T data, T parent, int position)
        {
            Find(Head, parent);
            Node<T> parentNode = Finded;
            Finded = null;

            Find(Head, data);
            Node<T> newNode = Finded;
            Finded = null;


            if (parentNode != null && newNode != null)
            {
                Find(newNode, parent);
                if (Finded != null)
                {
                    Console.WriteLine("Cannot move the element to this position!");
                    return;
                }

                DeleteElement(data);

                if (position == 1)
                {
                    parentNode.AddNext(newNode);
                }

                if (position == 2)
                {
                    parentNode.AddChild(newNode);
                }
            }
        }
    }
}
