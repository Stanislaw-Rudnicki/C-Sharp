using System;
using System.Text;
using System.Linq;


namespace _29022020
{
    class Program
    {
        // З клавіатури вводиться число n – кількість елементів масиву.
        // Потрібно створити масив типу double та заповнити його псевдовипадковими числами (діапазон вказано у варіанті).
        // Виконати вказані у варіанті дії та після кожної з них вивести результат.
        // При виведенні результатів використовуйте інтерпольовані рядки мови C#.

        // 1. Знайти суму модулів елементів, які мають дробову частину більшу або рівну 0.5.
        // 2. Впорядкувати другу половину масиву за спаданням значень елементів.
        // [-15.6; 53.3]
        // 1 дробовий знак

        static void Zad_01()
        {
            int n = 0;
            double DMIN = -15.6, DMAX = 53.3, sum = 0;
            bool f = false;
            while (!f)
            {
                Console.Write("Введіть n: ");
                f = int.TryParse(Console.ReadLine(), out n);
            }

            double[] masRND = new double[n];
            Random rnd = new Random();
            for (int i = 0; i < masRND.Length; i++)
                masRND[i] = Math.Round(rnd.NextDouble() * (DMAX - DMIN) + DMIN, 1);

            Console.WriteLine();
            foreach (var el in masRND)
                Console.Write($"{el}\t");
            Console.WriteLine();

            for (int i = 0; i < masRND.Length; i ++)
                if(Math.Abs(masRND[i] % 1) >= 0.5)
                    sum += Math.Abs(masRND[i]);

            Console.WriteLine($"\nСума модулів елементів, які мають дробову частину більшу або рівну 0.5: {sum}");

           
            masRND.Skip(masRND.Length / 2).Take(masRND.Length - masRND.Length / 2)
                .OrderByDescending(o => o).ToArray().CopyTo(masRND, masRND.Length / 2);
            
            Console.WriteLine("\nВпорядкувати другу половину масиву за спаданням значень елементів:\n");
            foreach (var el in masRND)
                Console.Write($"{el}\t");
            Console.WriteLine("\n");
        }

        // З клавіатури вводяться числа n – кількість рядків матриці, m – кількість стовпців матриці.
        // Потрібно створити матрицю типу double та заповнити її псевдовипадковими числами (діапазон вказано у варіанті).
        // Виконати вказані у варіанті дії та після кожної з них вивести результат.
        // При виведенні результатів використовуйте інтерпольовані рядки мови C#.

        // 1. Визначити кількість рядків, які не містять жодного від’ємного елемента.
        // 2. Переставити рядки матриці, розміщуючи їх за спаданням сум елементів у рядках.
        // [-2.9; 60.3]
        // 1 дробовий знак

        static void Zad_02()
        {
            int n = 0, m = 0;
            double DMIN = -2.9, DMAX = 60.3;
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
                    mas[i, j] = Math.Round(rnd.NextDouble() * (DMAX - DMIN) + DMIN, 1);
                    Console.Write($"{mas[i, j],6:N1}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            int count = 0;
            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                    if (mas[i, j] < 0)
                    {
                        count++;
                        break;
                    }

            Console.WriteLine($"Кількість рядків, які не містять жодного від’ємного елемента: {n - count}\n\n");


            Console.WriteLine("Переставити рядки матриці, розміщуючи їх за спаданням сум елементів у рядках:\n");

            // Translate two-dimensional array into jagged array
            double[][] temp = new double[n][];
            for (int i = 0; i < n; i++)
            {
                temp[i] = new double[m];
                for (int j = 0; j < m; j++)
                {
                    temp[i][j] = mas[i, j];
                }
            }

            temp = temp.OrderByDescending(a => a.Sum()).ToArray();

            foreach (var el in temp)
            {
                Console.Write($"{el.Sum(),6:N1}\t|");
                foreach (var i in el)
                    Console.Write($"{i,6:N1}\t");
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
