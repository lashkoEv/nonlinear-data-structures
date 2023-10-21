using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilevelLinkedList
{
    class Program
    {
        static void AddNext(ref MultiLevelLinkedList<int> list)
        {
            list.Print();
            Console.Write("Enter previous element: ");
            int prev = int.Parse(Console.ReadLine());
            Console.Write("Enter element to add: ");
            int data = int.Parse(Console.ReadLine());
            list.AddNext(prev, data);
            list.Print();
        }

        static void AddChild(ref MultiLevelLinkedList<int> list)
        {
            list.Print();
            Console.Write("Enter parent element: ");
            int parent = int.Parse(Console.ReadLine());
            Console.Write("Enter element: ");
            int data = int.Parse(Console.ReadLine());
            list.AddChild(parent, data);
            list.Print();
        }

        static void CopyList(ref MultiLevelLinkedList<int> list, ref MultiLevelLinkedList<int> copy)
        {
            Console.WriteLine("Initial list: ");
            list.Print();
            copy = list.Copy();
            Console.WriteLine("List copy: ");
            copy.Print();
        }

        static void DeleteElement(ref MultiLevelLinkedList<int> list)
        {
            list.Print();
            Console.Write("Enter element to delete: ");
            int data = int.Parse(Console.ReadLine());
            list.DeleteElement(data);
            list.Print();
        }

        static void DeleteBranch(ref MultiLevelLinkedList<int> list)
        {
            list.Print();
            Console.Write("Enter element to delete branch: ");
            int data = int.Parse(Console.ReadLine());
            list.DeleteBranch(data);
            list.Print();
        }

        static void DeleteLevel(ref MultiLevelLinkedList<int> list)
        {
            list.Print();
            Console.Write("Enter level ( >= 0 ): ");
            int data = int.Parse(Console.ReadLine());
            list.DeleteLevel(data);
            list.Print();
        }

        static void MoveElement(ref MultiLevelLinkedList<int> list)
        {
            list.Print();
            Console.Write("Enter element: ");
            int data = int.Parse(Console.ReadLine());
            Console.Write("Enter position (1 - next, 2 - child): ");
            int position = int.Parse(Console.ReadLine());
            Console.Write("Enter previous/parent: ");
            int parent = int.Parse(Console.ReadLine());
            list.Move(data, parent, position);
            list.Print();
        }

        static void PrintSize(ref MultiLevelLinkedList<int> list)
        {
            list.Print();
            Console.Write("Enter element: ");
            int data = int.Parse(Console.ReadLine());
            list.PrintSizeOfSpecialNode(data);
        }

        static void Transit(ref MultiLevelLinkedList<int> list, ref MultiLevelLinkedList<int> copy)
        {
            while (true)
            {
                int key = Menu();

                Console.Clear();

                switch (key)
                {
                    case 0:
                        {
                            AddNext(ref list);
                            break;
                        }
                    case 1:
                        {
                            AddChild(ref list);
                            break;
                        }
                    case 2:
                        {
                            list.Print();
                            break;
                        }
                    case 3:
                        {
                            list.Clear();
                            list.Print();
                            break;
                        }
                    case 4:
                        {
                            CopyList(ref list, ref copy);
                            break;
                        }
                    case 5:
                        {
                            if (copy != null)
                            {
                                copy.Print();
                            }
                            break;
                        }
                    case 6:
                        {
                            DeleteElement(ref list);
                            break;
                        }
                    case 7:
                        {
                            DeleteBranch(ref list);
                            break;
                        }
                    case 8:
                        {
                            DeleteLevel(ref list);
                            break;
                        }
                    case 9:
                        {
                            MoveElement(ref list);
                            break;
                        }
                    case 10:
                        {
                            PrintSize(ref list);
                            break;
                        }
                }

                Console.ReadLine();
            }
        }

        static int Menu()
        {
            int key = 0;
            ConsoleKey code;

            do
            {
                Console.Clear();
                Console.WriteLine("                MAIN MENU");
                if (key == 0) Console.WriteLine(">    1. Add next");
                else Console.WriteLine("  1. Add next");
                if (key == 1) Console.WriteLine(">    2. Add child");
                else Console.WriteLine("  2. Add child");
                if (key == 2) Console.WriteLine(">    3. Print list");
                else Console.WriteLine("  3. Print list");
                if (key == 3) Console.WriteLine(">    4. Clear list");
                else Console.WriteLine("  4. Clear list");
                if (key == 4) Console.WriteLine(">    5. Copy list");
                else Console.WriteLine("  5. Copy list");
                if (key == 5) Console.WriteLine(">    6. Show copy");
                else Console.WriteLine("  6. Show copy");
                if (key == 6) Console.WriteLine(">    7. Delete element");
                else Console.WriteLine("  7. Delete element");
                if (key == 7) Console.WriteLine(">    8. Delete branch");
                else Console.WriteLine("  8. Delete branch");
                if (key == 8) Console.WriteLine(">    9. Delete level");
                else Console.WriteLine("  9. Delete level");
                if (key == 9) Console.WriteLine(">    10. Move element");
                else Console.WriteLine("  10. Move element");
                if (key == 10) Console.WriteLine(">    11. Print size of node");
                else Console.WriteLine("  11. Print size of node");

                Console.WriteLine("\nPress ESC to exit");

                code = Console.ReadKey().Key;

                if (code == ConsoleKey.DownArrow) key++;
                if (code == ConsoleKey.UpArrow) key--;

                if (key > 10) key = 0;
                if (key < 0) key = 10;
            } while (code != ConsoleKey.Enter && code != ConsoleKey.Escape);

            if (code == ConsoleKey.Escape) Environment.Exit(0);

            return key;
        }

        static void Main(string[] args)
        {
            MultiLevelLinkedList<int> list = new MultiLevelLinkedList<int>();

            list.AddNext(1, 1);
            list.AddNext(1, 2);
            list.AddChild(2, 5);
            list.AddNext(5, 6);
            list.AddChild(6, 8);
            list.AddChild(8, 7);
            list.AddChild(5, 4);
            list.AddNext(2, 9);
            list.AddNext(8, 3);
            list.AddNext(9, 11);
            list.AddNext(7, 10);
            list.AddChild(11, 12);

            MultiLevelLinkedList<int> copy = null;
            Transit(ref list, ref copy);
        }
    }
}
