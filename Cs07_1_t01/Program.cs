// Створити ієрархію класів з об’єктів: Прямокутник, Відрізок, Квадрат, Точка.
// Створити масив з N елементів, який заповнюється випадково об’єктами з даної ієрархії.
// Вивести на екран доступну інформацію по фігурі (довжину, площу периметр,..)
// та примітивно намалювати фігуру.


using System;
using System.Text;
using System.IO;

namespace Cs07_1_t01
{
    class Point
    {
        protected double x, y;
        public Point(double x_, double y_) { x = x_; y = y_; }
        public override string ToString()
        {
            StringWriter strWriter = new StringWriter();
            strWriter.WriteLine($"Point: Coords: x = {x}, y = {y}.\n");
            strWriter.WriteLine(" * ");
            return strWriter.ToString();
        }
    }

    class Segment : Point
    {
        protected double l;
        public Segment(double x_, double y_, double l_) : base(x_, y_)
        {
            l = l_;
        }
        public override string ToString()
        {
            StringWriter strWriter = new StringWriter();
            strWriter.WriteLine($"Segment: Length: {l}.\n");
            strWriter.WriteLine(new StringBuilder().Insert(0, " *", (int)l).ToString());
            return strWriter.ToString();
        }
    }
    
    class Square : Segment
    {
        public Square(double x_, double y_, double a_) : base(x_, y_, a_) { }
        public override string ToString()
        {
            StringWriter strWriter = new StringWriter();
            strWriter.Write($"Square: Side length: {l}; ");
            strWriter.Write($"Perimeter: {l * 4}; ");
            strWriter.WriteLine($"Area: {l * l}.\n");
            string str = new StringBuilder().Insert(0, " *", (int)l).ToString();
            strWriter.WriteLine(str);
            int i = 0;
            while (i != (int)l - 2)
            {
                strWriter.WriteLine(" *"+ new string(' ', ((int)l - 2) * 2) + " *");
                i++;
            }
            strWriter.WriteLine(str);
            return strWriter.ToString();
        }
    }

    class Rectangle : Segment
    {
        private double b;
        public Rectangle(double x_, double y_, double a_, double b_) : base(x_, y_, a_)
        {
            b = b_;
        }
        public override string ToString()
        {
            StringWriter strWriter = new StringWriter();
            strWriter.Write($"Rectangle: Side length: {l}, {b}; ");
            strWriter.Write($"Perimeter: {2 * (l + b)}; ");
            strWriter.WriteLine($"Area: {l * b}.\n");
            string str = new StringBuilder().Insert(0, " *", (int)b).ToString();
            strWriter.WriteLine(str);
            int i = 0;
            while (i != (int)l - 2)
            {
                strWriter.WriteLine(" *" + new string(' ', ((int)b - 2) * 2) + " *");
                i++;
            }
            strWriter.WriteLine(str);
            return strWriter.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int n = rnd.Next(5, 11);
            Point[] arr = new Point[n];
            for (int i = 0; i < arr.Length; i++)
            {
                int R = rnd.Next(4);
                if (R == 0) arr[i] = new Point(3, 4);
                else if (R == 1) arr[i] = new Segment(3, 4, rnd.Next(3, 11));
                else if (R == 2) arr[i] = new Square(3, 4, rnd.Next(3, 11));
                else arr[i] = new Rectangle(3, 4, rnd.Next(3, 11), rnd.Next(3, 11));
            }

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
