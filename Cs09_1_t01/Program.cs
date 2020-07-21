// Розробити клас MyHashtable.
// Клас описує роботу зі студентами групи.
// Кожний студент має невизначену кількість оцінок, які можна додавати або видаляти.
// Вивести на екран список студентів з оцінками та середнім балом по кожному студенту та по групі в цілому.
// Клас повинен мати можливість шукати студента в групі та редагувати його поля.
// Зробити меню.
// Клас студент розробити самостійно.

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _02052020objcol
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

    public static class CollectionsExtension
    {
        public static void Print(this IDictionary list)
        {
            int count = 1;
            double avgSum = 0;
            Console.WriteLine($"ID# {"ПІБ",-31} Дата нар.\tСер. бал\tОцінки\n\n");
            foreach (var key in list.Keys)
            {
                Console.WriteLine($"{count++,3} {list[key]}\n");
                if (list[key] is Student)
                    avgSum += (list[key] as Student).AvgGrade;
            }
            Console.WriteLine($"\t\tСередній бал по групі в цілому: {avgSum / list.Count,8:N2}");
        }
    }

    class Student : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Bday { get; set; }
        public List<int> Grades = new List<int>();
        public double AvgGrade { get => Grades.Average(); }
        public Student(string LN, string FN, DateTime BD, int[] GR)
        {
            FirstName = FN;
            LastName = LN;
            Bday = BD;
            Grades.AddRange(GR);
        }
        public string GradesToString()
        {
            string str = string.Empty; 
            foreach (int item in Grades)
                str += item + ", ";
            return str.Remove(str.Length - 2);
        }
        public object Clone()
        {
            return new Student(LastName, FirstName, Bday, Grades.ToArray());
        }
        public override string ToString()
        {
            return $"{LastName,-15} {FirstName,-15} {Bday.ToShortDateString(),10}\t{AvgGrade,8:N2}\t{GradesToString()}";
        }
        
    }

    class MyHashtable : Hashtable
    {
        public void Edit(string request)
        {
            Student tmp = (Student)(this[request] as Student).Clone();
            Console.Write("Прізвище: ");
            SendKeys.SendWait(tmp.LastName);
            tmp.LastName = Console.ReadLine();
            Console.Write("Ім'я: ");
            SendKeys.SendWait(tmp.FirstName);
            tmp.FirstName = Console.ReadLine();
            Console.Write("Дата народження: ");
            SendKeys.SendWait(tmp.Bday.ToShortDateString());
            while (true)
            {
                try
                {
                    tmp.Bday = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.Write("Щось пішло не так! Введіть дату у форматі дд.мм.рррр: ");
                    continue;
                }
            }
            Console.Write("Оцінки: ");
            SendKeys.SendWait(tmp.GradesToString());
            while (true)
            {
                try
                {
                    tmp.Grades = Console.ReadLine().Split(',').Select(Int32.Parse).ToList();
                    break;
                }
                catch (Exception)
                {
                    Console.Write("Щось пішло не так! Введіть оцінки через кому: ");
                    continue;
                }
            }
            Remove(request);
            Add(tmp.LastName + " " + tmp.FirstName, tmp);
        }
    }

    class Program
    {
        static Random rnd = new Random();
        static int[] Grades()
        {
            int[] mas = new int[rnd.Next(3, 11)];
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = rnd.Next(3, 6);
            }
            return mas;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            MyHashtable Group = new MyHashtable{
                {"Ананченко Михайло", new Student("Ананченко", "Михайло", new DateTime(1983,02,04), Grades()) },
                {"Лозинський Роман", new Student("Лозинський", "Роман", new DateTime(1991,08,06), Grades()) },
                {"Бакумов Олександр", new Student("Бакумов", "Олександр", new DateTime(1994,04,09), Grades()) },
                {"Бобровська Соломія", new Student("Бобровська", "Соломія", new DateTime(1990,03,09), Grades()) },
                {"Бабенко Микола", new Student("Бабенко", "Микола", new DateTime(1989,09,04), Grades()) },
                {"Аллахвердієва Ірина", new Student("Аллахвердієва", "Ірина", new DateTime(1978,08,26), Grades()) },
                {"Ковальов Олександр", new Student("Ковальов", "Олександр", new DateTime(1987,10,07), Grades()) },
                {"Макаренко Михайло", new Student("Макаренко", "Михайло", new DateTime(1990,04,09), Grades()) },
                {"Богуцька Єлізавета", new Student("Богуцька", "Єлізавета", new DateTime(1984,04,14), Grades()) },
                {"Третьякова Галина", new Student("Третьякова", "Галина", new DateTime(1993,01,21), Grades()) },
            };

            Menu.MenuItems = new string[] {
                    "Вивести на екран список студентів",
                    "Шукати студента",
                    "Редагувати студента",
                    "Вихід" };

            while (true)
            {
                Menu.MenuSelect();
                Console.Clear();
                if (Menu.Selected < 0) break;
                if (Menu.Selected == 0)
                {
                    Group.Print();
                };
                if (Menu.Selected == 1)
                {
                    Console.Write("Введіть пошуковий запит (Прізвище та Ім'я): ");
                    string request = Console.ReadLine();
                    if (Group.ContainsKey(request))
                    {
                        Console.WriteLine($"\nID# {"ПІБ",-31} Дата нар.\tСер. бал\tОцінки\n");
                        Console.WriteLine("    " + Group[request]);
                    }
                    else
                        Console.WriteLine("\nНічого не знайдено");
                }
                if (Menu.Selected == 2)
                {
                    Console.Write("Введіть пошуковий запит (Прізвище та Ім'я): ");
                    string request = Console.ReadLine();
                    if (Group.ContainsKey(request))
                        Group.Edit(request);
                    else
                        Console.WriteLine("\nНічого не знайдено");
                };
                if (Menu.Selected == 3)
                {
                    break;
                };
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\ndone\n");
                Console.ReadKey();
            }
        }
    }
}