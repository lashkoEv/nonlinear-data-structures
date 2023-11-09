using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipList
{
    class SkipList
    {
        private Node Head { get; set; }
        private Node Tail { get; set; }

        private int Size { get; set; }

        private int MaxLevel { get; set; }

        private int MAX_VALUE = int.MaxValue;
        private int MIN_VALUE = int.MinValue;

        private Random random;

        public SkipList()
        {
            Size = 0;
            MaxLevel = 0;

            random = new Random();
            
            Head = new Node(MIN_VALUE);
            Tail = new Node(MAX_VALUE);

            Head.Next = Tail;
            Tail.Prev = Head;
        }

        public void Clear()
        {
            Size = 0;
            MaxLevel = 0; 
            
            Head = new Node(MIN_VALUE);
            Tail = new Node(MAX_VALUE);

            Head.Next = Tail;
            Tail.Prev = Head;
        }

       /* public SkipList Copy()
        {
            SkipList copy = new SkipList();

            Node tmp = Head;
            Node tmpNext = tmp.Next;

            while(tmp != null)
            {
                while (tmpNext != null)
                {
                    Node newNode = new Node(tmpNext.Value);
                    newNode.Next = copy.Head.Next;
                    
                }

                
                tmp = tmp.Below;
            }
            return copy;
        }*/

        public void Print()
        {
            Node start = Head;
            Node highestLevel = start;

            while (highestLevel != null)
            {
                while (start != null)
                {
                    Console.Write($"{start.Value}");

                    if (start.Next != null)
                    {
                        Console.Write($" => ");
                    }

                    start = start.Next;
                }

                highestLevel = highestLevel.Below;
                start = highestLevel;

                Console.WriteLine();
            }

            PrintSize();
            PrintMaxLevel();
        }

        public void PrintSize()
        {
            Console.WriteLine($"\nSize: {Size}");
        }

        public void PrintMaxLevel()
        {
            Console.WriteLine($"MaxLevel: {MaxLevel + 1}");
        }

        public void Includes(int value)
        {
            Node current = Head;

            while (current.Below != null)
            {
                current = current.Below;
                /*Console.WriteLine(current.Value);*/

                while (value >= current.Next.Value)
                {
                    /*Console.WriteLine(current.Value);*/
                    if (value == current.Next.Value)
                    {
                        Console.WriteLine("Item in the list!");
                        return;
                    }

                    current = current.Next;
                }
            }

            Console.WriteLine("This item is not in the list!");
        }

        private Node Search(int value)
        {
            Node current = Head;
            Node tmp = current;

            while(current != null)
            {
                while (value >= current.Next.Value)
                {
                    current = current.Next;
                }

                tmp = current;
                current = current.Below;
            }

            return tmp;
        }

        public void Add(int value)
        {
            Node position = Search(value);
            Node q;

            int level = -1;
            int numberOfHeads = -1;

            if(position.Value == value)
            {
                return;
            }

            int newML = MaxLevel;

            do
            {
                numberOfHeads++;
                level++;

                CalcIncreaseLevel(level);

                q = position;

                while(position.Above == null)
                {
                    position = position.Prev;
                }

                position = position.Above;

                AddAfterAbove(position, q, value);

            } while (random.NextDouble() >= 0.5 && MaxLevel <= newML + 1);

            Size++;

            DeleteEmpty();
        }

        private void CalcIncreaseLevel(int level)
        {
            if(level >= MaxLevel)
            {
                MaxLevel++;
                AddEmptyLevel();
            }
        }

        private void AddEmptyLevel()
        {
            Node newHead = new Node(MIN_VALUE);
            Node newTail = new Node(MAX_VALUE);

            newHead.Next = newTail;
            newHead.Below = Head;
            newTail.Prev = newHead;
            newTail.Below = Tail;

            Head.Above = newHead;
            Tail.Above = newTail;

            Head = newHead;
            Tail = newTail;
        }

        private Node AddAfterAbove(Node position, Node q, int value)
        {
            Node newNode = new Node(value);
            Node beforeNew = position.Below.Below;

            SetBeforeAndAfterRef(q, newNode);
            SetAboveAndBelowRef(position, value, newNode, beforeNew);

            return newNode;
        }

        private void SetBeforeAndAfterRef(Node q, Node newNode)
        {
            newNode.Next = q.Next;
            newNode.Prev = q;
            q.Next.Prev = newNode;
            q.Next = newNode;
        }

        private void SetAboveAndBelowRef(Node position, int value, Node newNode, Node beforeNew)
        {
            if(beforeNew != null)
            {
                while(true)
                {
                    if (beforeNew.Next.Value != value)
                    {
                        beforeNew = beforeNew.Next;
                    }
                    else
                    {
                        break;
                    }
                }

                newNode.Below = beforeNew.Next;
                beforeNew.Next.Above = newNode;
            }

            if(position != null)
            {
                if(position.Next.Value == value)
                {
                    newNode.Above = position.Next;
                }
            }
        }

        public Node Delete(int value)
        {
            Node toDelete = Search(value);

            if (toDelete.Value != value)
            {
                Console.WriteLine("No such element!");
                return null;
            }

            Size--;

            DeleteRef(toDelete);

            while (toDelete != null)
            {
                DeleteRef(toDelete);

                if (toDelete.Above != null)
                {
                    toDelete = toDelete.Above;
                }
                else
                {
                    break;
                }
            }

            DeleteEmpty();

            return toDelete;
        }

        private void DeleteRef(Node toDelete)
        {
            Node afterToDelete = toDelete.Next;
            Node beforeToDelete = toDelete.Prev;

            beforeToDelete.Next = afterToDelete;
            afterToDelete.Prev = beforeToDelete;
        }

        public void DeleteEmpty()
        {
            while(Head.Next.Value == MAX_VALUE)
            {
            Node newHead = Head.Below;
            Node newTail = Tail.Below;

            newHead.Above = null;
            newTail.Above = null;

            Head = newHead;
            Tail = newTail;

                MaxLevel--;
            }
        }
    }
}
