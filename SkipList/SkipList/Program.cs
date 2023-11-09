using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipList
{
    class Program
    {
        static void Add(ref SkipList list)
        {
            list.Print();
            Console.Write("Enter element: ");
            int data = int.Parse(Console.ReadLine());
            list.Add(data);
            list.Print();
        }

        static void Search(ref SkipList list)
        {
            list.Print();
            Console.Write("Enter element to search: ");
            int data = int.Parse(Console.ReadLine());
            list.Includes( data);
        }

        static void Delete(ref SkipList list)
        {
            list.Print();
            Console.Write("Enter element to delete: ");
            int data = int.Parse(Console.ReadLine());
            list.Delete(data);
            list.Print();
        }

        static void Clear(ref SkipList list)
        {
            list.Clear();
            list.Print();
        }

        static void Print(ref SkipList list)
        {
            list.Print();
        }

        static void Transit(ref SkipList list)
        {
            while (true)
            {
                int key = Menu();

                Console.Clear();

                switch (key)
                {
                    case 0:
                        {
                            Add(ref list);
                            break;
                        }
                    case 1:
                        {
                            Delete(ref list);
                            break;
                        }
                    case 2:
                        {
                            Search(ref list);
                            break;
                        }
                    case 3:
                        {
                            Clear(ref list);
                            break;
                        }
                    case 4:
                        {
                            Print(ref list);
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
                if (key == 0) Console.WriteLine(">    1. Add item");
                else Console.WriteLine("  1. Add item");
                if (key == 1) Console.WriteLine(">    2. Delete item");
                else Console.WriteLine("  2. Delete item");
                if (key == 2) Console.WriteLine(">    3. Search item");
                else Console.WriteLine("  3. Search item");
                if (key == 3) Console.WriteLine(">    4. Clear list");
                else Console.WriteLine("  4. Clear list");
                if (key == 4) Console.WriteLine(">    5. Print list");
                else Console.WriteLine("  5. Print list");

                Console.WriteLine("\nPress ESC to exit");

                code = Console.ReadKey().Key;

                if (code == ConsoleKey.DownArrow) key++;
                if (code == ConsoleKey.UpArrow) key--;

                if (key > 4) key = 0;
                if (key < 0) key = 4;
            } while (code != ConsoleKey.Enter && code != ConsoleKey.Escape);

            if (code == ConsoleKey.Escape) Environment.Exit(0);

            return key;
        }

        static void Main(string[] args)
        {
            SkipList s = new SkipList();
            Transit(ref s);
        }
    }
}
