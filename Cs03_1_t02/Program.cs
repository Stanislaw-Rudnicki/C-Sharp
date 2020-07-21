using System;
using System.Text;

namespace hw
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Paper[] papers = new Paper[5];

            papers[0] = new Paper(210, "A2");
            papers[1] = new Paper(80, "A4");
            papers[2] = new Paper(120, "A4");
            papers[3] = new Paper(80, "A5");
            papers[4] = new Paper(60, "A3");

            for (int i = 0; i < papers.Length; i++)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("[" + i + "]\n" + papers[i].PaperInfo());
                Console.WriteLine("-----------------------------------");
            }

            Console.WriteLine("\nСоздано объектов класса: " + Paper._count + "\n");
            //Console.ReadLine();

        }
    }
}
