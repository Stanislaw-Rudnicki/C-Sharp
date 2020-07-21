using System;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading;

namespace _29022020
{
    class Program
    {
        // Написати програму, для виконання операцій з множинами цілих чисел.
        // Множину представляє масив випадкових чисел.
        // Операції представлено на зображенні

        static void Zad_01()
        {
            int n = 0, m = 0;
            int dMin = -50, dMax = 50;
            bool f = false;
            while (!f)
            {
                Console.Write("Введіть n: ");
                f = int.TryParse(Console.ReadLine(), out n);
            }

            int[] mas1 = new int[n];
            Random rnd = new Random();

            Console.WriteLine();
            for (int i = 0; i < mas1.Length; i++)
            {
                mas1[i] = rnd.Next(dMin, dMax);
                //Console.Write($"{mas1[i]}\t");
                Console.Write("{0,4}{1}", mas1[i], (i + 1) % 14 == 0 && (i + 1) < mas1.Length ? "\n\n" : "\t");
            }
            Console.WriteLine("\n");

            f = false;
            while (!f)
            {
                Console.Write("Введіть m: ");
                f = int.TryParse(Console.ReadLine(), out m);
            }
            
            Console.WriteLine();
            int[] mas2 = new int[m];
            for (int i = 0; i < mas2.Length; i++)
            {
                mas2[i] = rnd.Next(dMin, dMax);
                Console.Write("{0,4}{1}", mas2[i], (i + 1) % 14 == 0 && (i + 1) < mas2.Length ? "\n\n" : "\t");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Об'єднання множин:\n");
            var result = mas1.Union(mas2);
            int j = 0;
            foreach (var el in result)
                Console.Write("{0,4}{1}", el, ++j % 14 == 0 && j < result.Count() ? "\n\n" :"\t");
            Console.WriteLine("\n");

            Console.WriteLine("Перетин множин:\n");
            result = mas1.Intersect(mas2);
            j = 0;
            foreach (var el in result)
                Console.Write("{0,4}{1}", el, ++j % 14 == 0 && j < result.Count() ? "\n\n" : "\t");
            Console.WriteLine("\n");

            Console.WriteLine("Різниця множин:\n");
            j = 0;
            result = mas1.Except(mas2);
            foreach (var el in result)
                Console.Write("{0,4}{1}", el, ++j % 14 == 0 && j < result.Count() ? "\n\n" : "\t");
            Console.WriteLine("\n");
        }
       

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Zad_01();

            //Console.ReadKey();
        }
    }
}