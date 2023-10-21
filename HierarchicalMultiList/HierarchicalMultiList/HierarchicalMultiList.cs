using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalMultiList
{
    public class HierarchicalMultiList<T>
    {
        List<List<T>> Head { get; set; }

        public HierarchicalMultiList()
        {
            Head = new List<List<T>>();
        }

        public HierarchicalMultiList(List<List<T>> head)
        {
            Head = head;
        }

        public void Print()
        {
            if (Head.Count == 0)
            {
                Console.WriteLine($"Empty list!");
                return;
            }

            foreach (var tmpHead in Head)
            {
                Console.Write("{ ");

                foreach (var tmp in tmpHead)
                {
                    Console.Write($"{tmp} ");
                }

                Console.Write("}");
            }

            PrintSizes();
        }

        public void PrintSizes()
        {
            Console.WriteLine();

            Console.WriteLine($"BaseLine size: {Head.Count}");

            Console.Write($"Sizes of childrens:");

            int totalSize = 0;

            foreach (var tmpHead in Head)
            {
                totalSize += tmpHead.Count;
                Console.Write($" {tmpHead.Count}");
            }

            Console.WriteLine($"\nTotal size: {totalSize}");

            Console.WriteLine();
        }

        public void Move(T data, int position)
        {
            if (!Find(data))
            {
                Console.WriteLine("This item is not in the list!");
                return;
            }

            int sumIndex = 0;

            for (int i = 0; i < Head.Count; i++)
            {
                for (int j = 0; j < Head[i].Count; j++)
                {
                    if (position != sumIndex)
                    {
                        sumIndex++;
                    }
                    else
                    {
                        Delete(data);
                        Head[i].Insert(j, data);
                        return;
                    }
                }
            }

            Console.WriteLine("Incorrect position for moving an element!");
        }

        private bool Find(T data)
        {
            foreach (var tmpHead in Head)
            {
                foreach (var tmp in tmpHead)
                {
                    if (tmp.Equals(data))
                    {
                        Console.WriteLine("This item is already in the list!");
                        return true;
                    }
                }
            }
            return false;
        }

        public void Delete(T data)
        {
            if (!Find(data))
            {
                Console.WriteLine("This item is not in the list!");
                return;
            }

            for (int i = 0; i < Head.Count; i++)
            {
                Head[i].Remove(data);

                if (Head[i].Count == 0) DeleteBranch(i);
            }
        }

        public void DeleteBranch(int index)
        {
            if (index < 0 || index >= Head.Count)
            {
                Console.WriteLine("There is no branch with this index in the list!");
                return;
            }

            Head.RemoveAt(index);
        }

        public void DeleteLevel(int level)
        {
            switch (level)
            {
                case 0:
                    {
                        Clear();
                        break;
                    }

                case 1:
                    {
                        Head[0].RemoveRange(1, Head[0].Count - 1);
                        Head.RemoveRange(1, Head.Count - 1);
                        break;
                    }

                case 2:
                    {
                        foreach (var tmpHead in Head)
                        {
                            tmpHead.RemoveRange(1, tmpHead.Count - 1);
                        }
                        break;
                    }

                default:
                    {
                        Console.WriteLine("There is no such level on the list!");
                        break;
                    }
            }
        }

        public void AddToBaseLine(int index, T data)
        {
            if (Find(data)) return;

            List<T> newHead = new List<T>();
            newHead.Add(data);

            if (index <= Head.Count && index >= 0)
            {
                Head.Insert(index, newHead);
            }
            else
            {
                Console.WriteLine($"Incorrect element index! Will be added to the end of the list!");
                Head.Add(newHead);
            }
        }

        public void AddChild(int parentInBaseLine, int index, T data)
        {
            if (Find(data)) return;

            if (parentInBaseLine <= Head.Count - 1 && parentInBaseLine >= 0)
            {
                AddNode(Head[parentInBaseLine], index, data);
            }
            else
            {
                Console.WriteLine($"Incorrect parent element index! Will be added to the end of the list!");

                if (Head.Count == 0)
                {
                    AddToBaseLine(0, data);
                    return;
                }

                AddNode(Head.Last(), index, data);
            }
        }

        private void AddNode(List<T> parent, int index, T data)
        {
            if (index <= parent.Count && index >= 0)
            {
                parent.Insert(index, data);
            }
            else
            {
                Console.WriteLine($"Incorrect element index! Will be added to the end of the list!");
                parent.Add(data);
            }
        }

        public void Clear()
        {
            foreach (var tmpHead in Head)
            {
                tmpHead.Clear();
            }

            Head.Clear();
        }

        public HierarchicalMultiList<T> Copy()
        {
            HierarchicalMultiList<T> copy = new HierarchicalMultiList<T>();

            for (int i = 0; i < Head.Count; i++)
            {
                copy.AddToBaseLine(i, Head[i][0]);

                for (int j = 1; j < Head[i].Count; j++)
                {
                    copy.AddChild(i, j, Head[i][j]);
                }
            }

            return copy;
        }
    }
}
