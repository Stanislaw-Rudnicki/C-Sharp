using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace _29022020
{
    class Program
    {
        // Написать программу, которая считывает символы с клавиатуры, пока не будет введена точка.
        // Программа должна сосчитать количество введенных пользователем пробелов.

        static void Zad_01()
        {
            char ch;
            int i = 0;
            do
            {
                Console.Write("Введите символ: ");
                ch = Console.ReadLine()[0];
                if (ch == ' ') i++;
            } while (ch != '.');
            Console.WriteLine($"Пробелов: {i}\n");
        }

        // Ввести с клавиатуры номер трамвайного билета (6-значное число)
        // и про-верить является ли данный билет счастливым
        // (если на билете напечатано шестизначное число,
        // и сумма первых трёх цифр равна сумме последних трёх, то этот билет считается счастливым).

        static void Zad_02()
        {
            bool lucky(int a)
            {
                int sum1, sum2;
                sum1 = a / 100000 + (a / 10000 % 10) + (a / 1000 % 10);
                sum2 = a / 100 % 10 + (a / 10 % 10) + a % 10;
                return (sum1 == sum2);
            }

            Console.Write("Введіть шестизначне число: ");
            bool f = int.TryParse(Console.ReadLine(), out int n);

            if (f && n > 99999 && n < 1000000)
            {
                Console.Write("\n\tЧисло ");
                if (!lucky(n)) Console.Write("не ");
                Console.WriteLine("щасливе!\n");
            }
            else
                Console.WriteLine("\n\tЧисло не шестизначне!\n");

        }

        // Числовые значения символов нижнего регистра в коде ASCII отличаются от значений символов верхнего регистра на величину 32.
        // Используя эту информацию, написать программу, которая считывает с клавиатуры и конвертирует
        // все символы нижнего регистра в символы верхнего регистра и наоборот.

        static void Zad_03()
        {
            Console.Write("Введите строку: ");
            string str = Console.ReadLine();

            // Тут +-32 не буде робити для кирилиці, тому що використовується UTF-16.
            // Щоб робила кирилиця з UTF-16, треба якось так писати:

            string OppositeCase(string s)
            {
                char[] c = s.ToCharArray();
                char[] cUpper = s.ToUpper().ToCharArray();
                char[] cLower = s.ToLower().ToCharArray();

                for (int i = 0; i < c.Length; i++)
                    if (c[i] == cUpper[i])
                        c[i] = cLower[i];
                    else
                        c[i] = cUpper[i];

                return new string(c);
            }
            Console.WriteLine("\n" + OppositeCase(str) + "\n");
        }

        // Даны целые положительные числа A и B (A < B).
        // Вывести все целые числа от A до B включительно; каждое число должно выводиться на новой строке;
        // при этом каждое число должно выводиться количество раз, равное его значению.
        // Например: если А = 3, а В = 7, то программа должна сформировать в консоли следующий вывод:
        // 3 3 3
        // 4 4 4 4
        // 5 5 5 5 5
        // 6 6 6 6 6 6
        // 7 7 7 7 7 7 7

        static void Zad_04()
        {
            int a = 0, b = 0;
            bool f = false;
            while (!f)
            {
                Console.Write("Введіть A: ");
                f = int.TryParse(Console.ReadLine(), out a);
            }
            f = false;
            while (!f)
            {
                Console.Write("Введіть B: ");
                f = int.TryParse(Console.ReadLine(), out b);
            }

            if (a < b)
            {
                Console.WriteLine();
                for (int i = a; i <= b; i++)
                {
                    for (int j = 0; j < i; j++)
                        Console.Write(i + " ");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            else
                Console.WriteLine("\nError: A > B\n");

        }

        // Дано целое число N (> 0), найти число, полученное при прочтении числа N справа налево.
        // Например, если было введено число 345, то программа должна вывести число 543.

        static void Zad_05()
        {
            int n = 0, m = 0;
            bool f = false;
            while (!f)
            {
                Console.Write("Введіть N: ");
                f = int.TryParse(Console.ReadLine(), out n);
            }

            while (n > 0)
            {
                int rest = n % 10;
                n /= 10;
                m = m * 10 + rest;
            }
            Console.WriteLine($"\n{m}\n");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Zad_01();
            Zad_02();
            Zad_03();
            Zad_04();
            Zad_05();

            //Console.ReadKey();
        }
    }
}
