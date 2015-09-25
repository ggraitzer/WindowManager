using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowManager.WindowLibrary;
using WindowManager.WindowLibrary.Models;

namespace WindowManager.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            bool firstLoop = true;

            Console.WriteLine("Press any key when you are ready to enumerate the windows.");
            Console.ReadKey();

            do
            {
                if (firstLoop)
                {
                    Console.WriteLine();
                }
                else
                {
                    firstLoop = false;
                }

                List<Window> windowList = WindowUtilities.GetWindows().OrderBy(w => w.Name).ToList();

                Dictionary<int, Window> windows = new Dictionary<int, Window>();

                int pos = 0;
                windowList.ForEach(w =>
                {
                    windows.Add(pos, w);
                    Console.WriteLine($"[{pos}] Id: {w.Id} - Name: {w.Name}");
                    Console.WriteLine($"\tPosition: Left {w.Rectangle.Left} Top {w.Rectangle.Top} Right {w.Rectangle.Right} Bottom {w.Rectangle.Bottom}");
                    pos++;
                });

                Console.WriteLine();

                Console.Write("Please select a window: ");

                var selection = int.Parse(Console.ReadLine());

                Window window = windows[selection];

                Console.Write($"Adjust Window with Name: {window.Name} y/n: ");

                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.KeyChar == 'y')
                {
                    Console.WriteLine($"Current position: {window.Rectangle}");
                    Console.Write("Enter new left: ");
                    var left = int.Parse(Console.ReadLine());

                    Console.Write("Enter new top: ");
                    var top = int.Parse(Console.ReadLine());

                    Console.Write("Enter new right: ");
                    var right = int.Parse(Console.ReadLine());

                    Console.Write("Enter new bottom: ");
                    var bottom = int.Parse(Console.ReadLine());

                    Rect rect = new Rect();
                    rect.Left = left;
                    rect.Top = top;
                    rect.Right = right;
                    rect.Bottom = bottom;

                    WindowUtilities.CreateBox(rect);

                    Console.Write($"Move window {window.Name} to this location? y/n: ");

                    if (Console.ReadKey().KeyChar == 'y')
                    {
                        WindowUtilities.MoveWindow(window.Id, left, top, right - left, bottom - top + 50);
                    }

                    WindowUtilities.RemoveBox();
                }
                else
                {
                    Console.WriteLine("Not Adjusting window.");
                }

                Console.Write("Continue? y/n: ");
            } while (Console.ReadKey().KeyChar == 'y');
        }
    }
}
