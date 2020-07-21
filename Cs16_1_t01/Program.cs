// Написать приложение, позволяющее:
// • создать новый файл с именем «Day17.txt». В случае наличия файла с таким именем — вывести сообщение;
// • открыть и прочесть файл с именем «Day17.txt». В случае отсутствия — вывести сообщение об отсутствии;
// • записать форматированную информацию в файл.
// Структура записываемой информации:
// Исходные данные: двумерный массив дробных чисел,
// двумерный массив целых чисел.
// - фамилия, имя, отчество, дата рождения,
// - с новой строки количество строк и столбцов массива дробных чисел,
// - с новой строки значения элементов двумерного массива дробных чисел (каждая строка массива в новой строке файла).
// - с новой строки количество строк и столбцов массива целых чисел.
// - с новой строки двумерный массив целых чисел, записанных в одну строку.
// - с новой строки текущая дата.
// • прочесть информацию из файла и преобразовать ее в соответствии с исходной структурой.
// Реализовать простейшее меню.


using System;
using System.Text;
using System.IO;

namespace Cs16_1_t01
{
    class Menu
    {
        const ConsoleColor FOREGROUND = ConsoleColor.Gray;
        const ConsoleColor ITEMSELECT = ConsoleColor.White;
        const int MENULEFT = 20;
        const int MENUTOP = 4;
        static string[] menuItems;
        public static string[] MenuItems { set { menuItems = value; } }
        public static int Selected { get; private set; } = 0;
        static void PaintMenu()
        {
            Console.ForegroundColor = FOREGROUND;
            Console.Clear();
            Console.SetCursorPosition(MENULEFT, MENUTOP);
            Console.WriteLine("......MENU......\n");
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.SetCursorPosition(MENULEFT, MENUTOP + i + 1);
                if (i == Selected)
                {
                    Console.ForegroundColor = ITEMSELECT;
                    Console.Write("=>");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.WriteLine(menuItems[i]);
                Console.ForegroundColor = FOREGROUND;
            }
        }
        public static void MenuSelect()
        {
            ConsoleKey c = ConsoleKey.DownArrow;
            while (true)
            {
                if (c == ConsoleKey.UpArrow || c == ConsoleKey.DownArrow)
                {
                    PaintMenu();
                }
                c = Console.ReadKey().Key;
                switch (c)
                {
                    case ConsoleKey.Escape: //Esc
                        Selected = -1;
                        return;
                    case ConsoleKey.DownArrow: //down
                        ++Selected;
                        if (Selected == menuItems.Length) Selected = 0;
                        break;
                    case ConsoleKey.UpArrow://up
                        if (Selected == 0) Selected = menuItems.Length;
                        --Selected;
                        break;
                    case ConsoleKey.Enter: //Enter
                        return;
                }
            }
        }

    }

    class Program
    {
        static void SaveFileSW(string file)
        {
            using (StreamWriter stream = new StreamWriter(file, false, Encoding.Unicode))
            {
                // - фамилия, имя, отчество, дата рождения,
                stream.WriteLine("Рудницкий Станислав Александрович, 22.03.1983");
                
                // - с новой строки количество строк и столбцов массива дробных чисел,
                int n = 5, m = 10;
                stream.WriteLine(n + ", " + m);
                
                // - с новой строки значения элементов двумерного массива дробных чисел (каждая строка массива в новой строке файла).
                double dMin = -10.34, dMax = 53.44;
                double[,] mas = new double[n, m];
                Random rnd = new Random();
                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        mas[i, j] = Math.Round(rnd.NextDouble() * (dMax - dMin) + dMin, 2);
                        stream.Write($"{mas[i, j]}; ");
                    }
                    stream.WriteLine();
                }

                // - с новой строки количество строк и столбцов массива целых чисел.
                stream.WriteLine(n + ", " + m);

                // - с новой строки двумерный массив целых чисел, записанных в одну строку.
                int Min = -10, Max = 99;
                int[,] intmas = new int[n, m];
                for (int i = 0; i < intmas.GetLength(0); i++)
                {
                    for (int j = 0; j < intmas.GetLength(1); j++)
                    {
                        intmas[i, j] = rnd.Next(Min, Max + 1);
                        stream.Write($"{intmas[i, j]}; ");
                    }
                }

                // - с новой строки текущая дата.
                stream.WriteLine("\n" + DateTime.Now.ToShortDateString());

                Console.WriteLine("\nFile Saved Successfully\n");
            }
        }

        static void LoadFileSW(string file)
        {
            using (StreamReader stream = new StreamReader(file, Encoding.Unicode))
            {
                string[] str = stream.ReadLine().Split(',');
                string fullName = str[0];

                DateTime Bday = DateTime.Parse(str[1]);
                Console.WriteLine(fullName + ", " + Bday.ToShortDateString() + "\n");

                str = stream.ReadLine().Split(',');
                double[,] mas = new double[int.Parse(str[0]), int.Parse(str[1])];
                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    str = stream.ReadLine().Split(';');
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        mas[i, j] = double.Parse(str[j]);
                        Console.Write($"{mas[i, j],6:N2}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                str = stream.ReadLine().Split(',');
                int[,] intmas = new int[int.Parse(str[0]), int.Parse(str[1])];
                
                str = stream.ReadLine().Split(';');
                for (int i = 0; i < intmas.GetLength(0); i++)
                {
                    for (int j = 0; j < intmas.GetLength(1); j++)
                    {
                        intmas[i, j] = int.Parse(str[i + j]);
                        Console.Write($"{intmas[i, j],6}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                DateTime Today = DateTime.Parse(stream.ReadLine());
                Console.WriteLine(Today.ToShortDateString());

                Console.WriteLine("\nFile Loaded Successfully\n");
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            string fileName = "Day17.txt";

            Menu.MenuItems = new string[] {
                    "Создать новый файл с именем «" + fileName + "»",
                    "Открыть и прочесть файл с именем «" + fileName + "»" };

            while (true)
            {
                Menu.MenuSelect();
                Console.Clear();
                if (Menu.Selected < 0) break;
                if (Menu.Selected == 0)
                {
                    if (File.Exists("Day17.txt"))
                    {
                        Console.WriteLine("Файл с таким именем уже существует.");
                        string ch;
                        do
                        {
                            Console.Write("Перезаписать? (y/n): ");
                            ch = Console.ReadLine().ToLower();
                        } while (ch.Length == 0 || ch[0] != 'y' && ch[0] != 'n');
                        if (ch[0] == 'y')
                            SaveFileSW(fileName);
                    }
                    else
                        SaveFileSW(fileName);
                };
                if (Menu.Selected == 1)
                {
                    if (!File.Exists("Day17.txt"))
                        Console.WriteLine("Файл не найден.");
                    else
                        LoadFileSW(fileName);
                };
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\ndone\n");
                Console.ReadKey();
            }
        }
    }
}