using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalMultiList
{
    class Program
    {
        static void AddToBaseLine(ref HierarchicalMultiList<int> list)
        {
            list.Print();
            Console.Write("Enter element: ");
            int data = int.Parse(Console.ReadLine());
            Console.Write("Enter index: ");
            int index = int.Parse(Console.ReadLine());
            list.AddToBaseLine(index, data);
            list.Print();
        }

        static void AddChild(ref HierarchicalMultiList<int> list)
        {
            list.Print();
            Console.Write("Enter parent index in baseline: ");
            int parent = int.Parse(Console.ReadLine());
            Console.Write("Enter element: ");
            int data = int.Parse(Console.ReadLine());
            Console.Write("Enter index: ");
            int index = int.Parse(Console.ReadLine());
            list.AddChild(parent, index, data);
            list.Print();
        }

        static void CopyList(ref HierarchicalMultiList<int> list, ref HierarchicalMultiList<int> copy)
        {
            Console.WriteLine("Initial list: ");
            list.Print();
            copy = list.Copy();
            Console.WriteLine("List copy: ");
            copy.Print();
        }

        static void DeleteElement(ref HierarchicalMultiList<int> list)
        {
            list.Print();
            Console.Write("Enter element to delete: ");
            int data = int.Parse(Console.ReadLine());
            list.Delete(data);
            list.Print();
        }

        static void DeleteBranch(ref HierarchicalMultiList<int> list)
        {
            list.Print();
            Console.Write("Enter branch index: ");
            int data = int.Parse(Console.ReadLine());
            list.DeleteBranch(data);
            list.Print();
        }

        static void DeleteLevel(ref HierarchicalMultiList<int> list)
        {
            list.Print();
            Console.Write("Enter level (0-2): ");
            int data = int.Parse(Console.ReadLine());
            list.DeleteLevel(data);
            list.Print();
        }

        static void MoveElement(ref HierarchicalMultiList<int> list)
        {
            list.Print();
            Console.Write("Enter element: ");
            int data = int.Parse(Console.ReadLine());
            Console.Write("Enter position to move: ");
            int position = int.Parse(Console.ReadLine());
            list.Move(data, position);
            list.Print();
        }

        static void Transit(ref HierarchicalMultiList<int> list, ref HierarchicalMultiList<int> copy)
        {
            while (true)
            {
                int key = Menu();

                Console.Clear();

                switch (key)
                {
                    case 0:
                        {
                            AddToBaseLine(ref list);
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
                            copy.Print();
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
                if (key == 0) Console.WriteLine(">    1. Add element to baseline");
                else Console.WriteLine("  1. Add element to baseline");
                if (key == 1) Console.WriteLine(">    2. Add child element");
                else Console.WriteLine("  2. Add child element");
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
                else Console.WriteLine("  8. Copy branch");
                if (key == 8) Console.WriteLine(">    9. Delete level");
                else Console.WriteLine("  9. Delete level");
                if (key == 9) Console.WriteLine(">    10. Move element");
                else Console.WriteLine("  10. Move element");

                Console.WriteLine("\nPress ESC to exit");

                code = Console.ReadKey().Key;

                if (code == ConsoleKey.DownArrow) key++;
                if (code == ConsoleKey.UpArrow) key--;

                if (key > 9) key = 0;
                if (key < 0) key = 9;
            } while (code != ConsoleKey.Enter && code != ConsoleKey.Escape);

            if (code == ConsoleKey.Escape) Environment.Exit(0);

            return key;
        }

        static void Main(string[] args)
        {
            HierarchicalMultiList<int> list = new HierarchicalMultiList<int>();

            HierarchicalMultiList<int> copy = null;

            Transit(ref list, ref copy);
        }
    }
}
