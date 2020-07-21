// Розробити клас SortedArrayList на основі ArrayList, який має методи:
// - Add – додає студента до групи студентів у положення відповідно до рейтингу.
// - ModifyIndex – дозволяє редагувати за індексом студента.
// - Перевизначити методи Insert класу ArrayList. Оскільки додавати в довільне положення неможливо, адже ми сортуємо по рейтингу.

using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Cs09_1_t02
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
        public static void Print(this ICollection list)
        {
            int count = 0;
            Console.WriteLine($"ID# {"ПІБ",-31} Дата нар.\t{"Рейтинг",10}\n\n");
            foreach (var item in list)
                Console.WriteLine($"{count++,3} {item}\n");
        }
    }
    class Student : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Bday { get; set; }
        public int Rating { get; set; }
        public Student(string LN, string FN, DateTime BD, int R)
        {
            FirstName = FN;
            LastName = LN;
            Bday = BD;
            Rating = R;
        }
        public object Clone()
        {
            return new Student(LastName, FirstName, Bday, Rating);
        }
        public override string ToString()
        {
            return $"{LastName,-15} {FirstName,-15} {Bday.ToShortDateString(),10}\t{Rating,10}";
        }

    }

    class CompIntRaring : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is Student X && y is Student Y)
                return X.Rating.CompareTo(Y.Rating);
            throw new InvalidCastException();
        }
    }

    class SortedArrayList : ArrayList
    {
        public override int Add(object value)
        {
            base.Add(value);
            Sort(new CompIntRaring());
            return BinarySearch(value, new CompIntRaring());
        }
        public override void Insert(int index, object value)
        {
            Add(value);
        }

        public void ModifyIndex(int index)
        {
            Student ptr = this[index] as Student;
            Console.Write("Прізвище: ");
            SendKeys.SendWait(ptr.LastName);
            ptr.LastName = Console.ReadLine();
            Console.Write("Ім'я: ");
            SendKeys.SendWait(ptr.FirstName);
            ptr.FirstName = Console.ReadLine();
            Console.Write("Дата народження: ");
            SendKeys.SendWait(ptr.Bday.ToShortDateString());
            while (true)
            {
                try
                {
                    ptr.Bday = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.Write("Щось пішло не так! Введіть дату у форматі дд.мм.рррр: ");
                    continue;
                }
            }
            int ratingPrev = ptr.Rating;
            Console.Write("Рейтинг: ");
            SendKeys.SendWait(ptr.Rating.ToString());
            while (true)
            {
                try
                {
                    ptr.Rating = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.Write("Щось пішло не так! Введіть рейтинг: ");
                    continue;
                }
            }
            if (ratingPrev != ptr.Rating)
            {
                Sort(new CompIntRaring());
            }
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Random rnd = new Random();
            SortedArrayList Group = new SortedArrayList();
            Group.Add(new Student("Ананченко", "Михайло", new DateTime(1983, 02, 04), rnd.Next(1001)));
            Group.Add(new Student("Лозинський", "Роман", new DateTime(1991,08,06), rnd.Next(1001)));
            Group.Add(new Student("Бакумов", "Олександр", new DateTime(1994,04,09), rnd.Next(1001)));
            Group.Add(new Student("Бобровська", "Соломія", new DateTime(1990,03,09), rnd.Next(1001)));
            Group.Add(new Student("Бабенко", "Микола", new DateTime(1989,09,04), rnd.Next(1001)));
            Group.Add(new Student("Аллахвердієва", "Ірина", new DateTime(1978,08,26), rnd.Next(1001)));
            Group.Add(new Student("Ковальов", "Олександр", new DateTime(1987,10,07), rnd.Next(1001)));
            Group.Add(new Student("Макаренко", "Михайло", new DateTime(1990,04,09), rnd.Next(1001)));
            Group.Add(new Student("Богуцька", "Єлізавета", new DateTime(1984,04,14), rnd.Next(1001)));
            Group.Add(new Student("Третьякова", "Галина", new DateTime(1993,01,21), rnd.Next(1001)));


            Menu.MenuItems = new string[] {
                    "Вивести на екран список студентів",
                    "Додати студента до групи",
                    "Редагувати за індексом студента",
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
                    Student tmp = new Student("Pupkin", "Vasily", new DateTime(1970, 01, 01), 0);
                    Console.Write("Прізвище: ");
                    tmp.LastName = Console.ReadLine();
                    Console.Write("Ім'я: ");
                    tmp.FirstName = Console.ReadLine();
                    Console.Write("Дата народження: ");
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
                    Console.Write("Рейтинг: ");
                    while (true)
                    {
                        try
                        {
                            tmp.Rating = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch (Exception)
                        {
                            Console.Write("Щось пішло не так! Введіть рейтинг: ");
                            continue;
                        }
                    }
                    Group.Insert(tmp.Rating, tmp);
                }
                if (Menu.Selected == 2)
                {
                    int n = 0;
                    bool f = false;
                    Console.WriteLine("Якого студента редагувати?");
                    while (!f || (n < 0 || n >= Group.Count))
                    {
                        Console.Write($"Введіть індекс від 0 до {Group.Count - 1}: ");
                        f = int.TryParse(Console.ReadLine(), out n);
                    }
                    Group.ModifyIndex(n);
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