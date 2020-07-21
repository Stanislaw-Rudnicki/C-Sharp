// В С # индексация начинается с нуля, но в некоторых языках программирования это не так.
// Например, в Turbo Pascal индексация массиве начинается с 1.
// Напишите класс RangeOfArray, который позволяет работать с массивом такого типа,
// в котором индексный диапазон устанавливается пользователем. Например, в диапазоне от 6 до 10, или от -9 до 15.
// Подсказка: В классе можно объявить две переменных, которые будут содержать верхний и нижний индекс допустимого диапазона.

using System;
using System.IO;

namespace SimpleProject
{
    public class RangeOfArray
    {
        private int lowerBound;
        private int upperBound;
        private int[] arr;
        public RangeOfArray(int lowerIndex, int upperIndex)
        {
            lowerBound = lowerIndex;
            upperBound = upperIndex;
            arr = new int[Math.Abs(upperIndex - lowerIndex) + 1];
        }
        public int Length => arr.Length;
        public int this[int index]
        {
            get
            {
                if (index < lowerBound || index > upperBound)
                {
                    throw new IndexOutOfRangeException();
                }
                return arr[index + Math.Abs(lowerBound)];
            }
            set => arr[index + Math.Abs(lowerBound)] = value;
        }
        public override string ToString()
        {
            StringWriter strWriter = new StringWriter();
            for (int i = 0; i < arr.Length; i++)
            {
                strWriter.Write("{0,4}{1}", arr[i], (i + 1) % 14 == 0 && (i + 1) < arr.Length ? "\n\n" : "\t");
            }
            strWriter.WriteLine("\n");
            return strWriter.ToString();
        }

    }
    public class Program
    {
        public static void Main()
        {
            Random rnd = new Random();
            int lowerIndex = 0, uperIndex = 0;
            bool f = false;
            while (!f)
            {
                Console.Write("Введите нижний индекс диапазона массива: ");
                f = int.TryParse(Console.ReadLine(), out lowerIndex);
            }
            f = false;
            while (!f)
            {
                Console.Write("Введите верхний индекс диапазона массива: ");
                f = int.TryParse(Console.ReadLine(), out uperIndex);
            }

            if (lowerIndex < uperIndex)
            {
                RangeOfArray mas = new RangeOfArray(lowerIndex, uperIndex);

                try
                {
                    Console.WriteLine();
                    for (int i = lowerIndex; i <= uperIndex; i++)
                    {
                        mas[i] = rnd.Next(100);
                    }
                    Console.WriteLine(mas);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Введены некорректные индексы");
            }
        }
    }
}