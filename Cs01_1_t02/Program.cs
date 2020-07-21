using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace _29022020
{
    class Program
    {
        // З клавіатури вводиться число n – кількість елементів масиву.
        // Потрібно створити масив типу double та заповнити його псевдовипадковими числами (діапазон вказано у варіанті).
        // Виконати вказані у варіанті дії та після кожної з них вивести результат.
        // При виведенні результатів використовуйте інтерпольовані рядки мови C#.

        // 1. Знайти суму елементів з індексами, які діляться на 3,
        // 2. Впорядкувати першу половину масиву за зростанням значень елементів.
        // [-10.51; 10.53]

        static void Zad_01()
        {
            int n = 0;
            double dMin = -10.51, dMax = 10.53, sum = 0;
            bool f = false;
            while (!f)
            {
                Console.Write("Введіть n: ");
                f = int.TryParse(Console.ReadLine(), out n);
            }

            double[] masRND = new double[n];
            Random rnd = new Random();
            for (int i = 0; i < masRND.Length; i++)
                masRND[i] = Math.Round(rnd.NextDouble() * (dMax - dMin) + dMin, 2);

            Console.WriteLine();
            foreach (var el in masRND)
                Console.Write($"{el}\t");
            Console.WriteLine();

            for (int i = 0; i < masRND.Length; i += 3)
                sum += masRND[i];
            
            Console.WriteLine($"\nСума елементів з індексами, які діляться на 3: {sum}");
           
            bool flag;
            do
            {
                flag = false;
                for (int i = 0; i < masRND.Length / 2 - 1; i++)
                    if (masRND[i] > masRND[i + 1])
                    {
                        double c = masRND[i];
                        masRND[i] = masRND[i + 1];
                        masRND[i + 1] = c;
                        flag = true;
                    }
            } while (flag);

            Console.WriteLine("\nВпорядкувати першу половину масиву за зростанням значень елементів:\n");
            foreach (var el in masRND)
                Console.Write($"{el}\t");
            Console.WriteLine("\n");
        }

        // З клавіатури вводяться числа n – кількість рядків матриці, m – кількість стовпців матриці.
        // Потрібно створити матрицю типу double та заповнити її псевдовипадковими числами (діапазон вказано у варіанті).
        // Виконати вказані у варіанті дії та після кожної з них вивести результат.
        // При виведенні результатів використовуйте інтерпольовані рядки мови C#.

        // 1. Знайти найбільші елементи у кожному стовпцю. Серед них визначити найменший.
        // 2. Поміняти порядок слідування елементів у рядках на протилежний.
        // [-10.34; 53.44], 2 дробових знаки

        static void Zad_02()
        {
            int n = 0, m = 0;
            double dMin = -10.34, dMax = 53.44;
            bool f = false;
            while (!f)
            {
                Console.Write("Введіть n: ");
                f = int.TryParse(Console.ReadLine(), out n);
            }
            f = false;
            while (!f)
            {
                Console.Write("Введіть m: ");
                f = int.TryParse(Console.ReadLine(), out m);
            }

            Console.WriteLine();
            double[,] mas = new double[n, m];
            Random rnd = new Random();
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    mas[i, j] = Math.Round(rnd.NextDouble() * (dMax - dMin) + dMin, 2);
                    Console.Write($"{mas[i, j],6:N2}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            double[] masM = new double[m];
            for (int j = 0; j < mas.GetLength(1); j++)
            {
                masM[j] = mas[0, j];
                for (int i = 0; i < mas.GetLength(0); i++)
                    if (mas[i, j] > masM[j]) masM[j] = mas[i, j];
            }

            Console.WriteLine("Найбільші елементи у кожному стовпці:\n"); 
            foreach (var el in masM)
                Console.Write($"{el,6:N2}\t");
            Console.WriteLine("\n");

            Console.Write("Серед них найменший: ");
            int min = 0;
            for (int i = 0; i < masM.Length; i++)
                if (masM[i] < masM[min]) min = i;
            Console.WriteLine($"{masM[min],6:N2}\n");

            Console.WriteLine("Поміняти порядок слідування елементів у рядках на протилежний:\n");
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1) / 2; j++)
                {
                    double c = mas[i, j];
                    mas[i, j] = mas[i, mas.GetLength(1) - 1 - j];
                    mas[i, mas.GetLength(1) - 1 - j] = c;
                }
                for (int j = 0; j < mas.GetLength(1); j++)
                    Console.Write($"{mas[i, j],6:N2}\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Zad_01();
            Zad_02();

            //Console.ReadKey();
        }
    }
}
