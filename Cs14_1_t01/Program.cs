// Task 1. Дана целочисленная последовательность.
// Извлечь из нее все положительные числа, сохранив их исходный порядок следования.
//
// Task 2. Дана целочисленная последовательность.
// Извлечь из нее все нечетные числа, сохранив их исходный порядок следования и удалив все вхождения повторяющихся элементов, кроме первых.
// 
// Task 3. Дана целочисленная последовательность.
// Извлечь из нее все четные отрицательные числа, поменяв порядок извлеченных чисел на обратный.
//
// Task 4. Даны цифра D (целое однозначное число) и целочисленная последовательность A.
// Извлечь из A все различные положительные числа, оканчивающиеся цифрой D (в исходном порядке).
//
// Task 6. Даны целое число K (> 0) и строковая последовательность A.
// Из элементов A, извлечь те строки, которые имеют длину > K и начинаются с заглавной латинской буквы.

// Task 7. Исходная последовательность содержит сведения о клиентах фитнес-центра.
// Каждый элемент последовательности включает следующие целочисленные поля:
// <Код клиента> <Год> <Номер месяца> <Продолжительность занятий (в часах)>
// Найти элемент последовательности с минимальной продолжительностью занятий.
// Вывести эту продолжительность, а также соответствующие ей год и номер месяца (в указанном порядке на той же строке).
// Если имеется несколько элементов с минимальной продолжительностью, то вывести данные того из них, который является последним в исходной последовательности.

// Указание. Для нахождения требуемого элемента следует использовать методы OrderByDescending и Last.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cs14_1_t01
{
    public static class CollectionsExtension
    {
        public static void Print(this int[] mas)
        {
            if (mas.Length != 0)
            for (int i = 0; i < mas.Length; i++)
                Console.Write("{0,4}{1}", mas[i], (i + 1) % 14 == 0 && (i + 1) < mas.Length ? "\n\n" : "\t");
            else
                Console.WriteLine("Таких чисел нет\n");
            Console.WriteLine("\n");
        }
        public static void Print(this string[] mas)
        {
            if (mas.Length != 0)
                for (int i = 0; i < mas.Length; i++)
                    Console.WriteLine(mas[i]);
            else
                Console.WriteLine("Таких строк нет\n");
            Console.WriteLine("\n");
        }
    }

    class Client
    {
        public int ClientCode { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Duration { get; set; }
        public Client(int C, int Y, int M, int D)
        {
            ClientCode = C;
            Year = Y;
            Month = M;
            Duration = D;
        }
    }

    class Program
    {
        private const int N = 4 * 14, MIN = -100, MAX = 100;
        private static Random rnd = new Random();
        private static int[] masRND = Enumerable.Repeat(0, N).Select(i => rnd.Next(MIN, MAX + 1)).ToArray();
            
        static void Task_01()
        {
            Console.WriteLine("Извлечь все положительные числа, сохранив их исходный порядок следования:\n");
            masRND.Where(i => i > 0).ToArray().Print();
        }

        static void Task_02()
        {
            Console.WriteLine("Извлечь все нечетные числа, сохранив их исходный порядок следования\nи удалив все вхождения повторяющихся элементов, кроме первых:\n");
            masRND.Where(i => (i & 1) == 1).Distinct().ToArray().Print();
        }

        static void Task_03()
        {
            Console.WriteLine("Извлечь все четные отрицательные числа, поменяв порядок извлеченных чисел на обратный:\n");
            masRND.Where(i => (i & 1) != 1 && i < 0).Reverse().ToArray().Print();
        }

        static void Task_04()
        {
            Console.WriteLine("Даны цифра D (целое однозначное число) и целочисленная последовательность A.\n" +
                "Извлечь из A все различные положительные числа, оканчивающиеся цифрой D (в исходном порядке):\n");
            int D = rnd.Next(0, 10);
            Console.WriteLine($"D = {D}\n");
            masRND.Where(i => i > 0 && i % 10 == D).Distinct().ToArray().Print();
        }

        static void Task_06()
        {
            Console.WriteLine("Даны целое число K (> 0) и строковая последовательность A.\n" +
                "Из элементов A, извлечь те строки, которые имеют длину > K и начинаются с заглавной латинской буквы.:\n");
            int K = rnd.Next(0, 30);
            Console.WriteLine($"K = {K}\n");
            string[] sequence = {
                "Random Paragraphs and Sentences",
                "Implement a class that randomizes",
                "sentences and paragraphs using Random.",
                "Random paragraphs and sentences",
                "can be generated.",
                "The text must look like",
                "regular English writing.",
                "An algorithm can generate",
                "language that appears natural.",
                " "
            };
            sequence.Where(i => i.Length > K && (i[0] >= 'A') && (i[0] <= 'Z')).ToArray().Print();
        }

        static void Task_07()
        {
            List<Client> List = new List<Client>(Enumerable.Range(1, 100).
                Select(i => new Client(rnd.Next(1, 1001), rnd.Next(2010, 2021), rnd.Next(1, 13), rnd.Next(1, 1001))));

            Client Result = List.OrderByDescending(i => i.Duration).Last();
            
            Console.WriteLine("Вывести продолжительность, а также соответствующие ей год и номер месяца\n" +
                "(в указанном порядке на той же строке).\n");

            Console.WriteLine($"Duration = {Result.Duration}\tYear = {Result.Year}\tMonth = {Result.Month}\n");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            masRND.Print();
            Task_01();
            Task_02();
            Task_03();
            Task_04();
            Task_06();
            Task_07();
        }
    }
}
