// Прочитать текст C#-программы (выбрать самостоятельно) и все слова public в объявлении полей классов заменить
// на слово private. В исходном коде в каждом слове длиннее двух символов все строчные символы заменить прописными.
// Также в коде программы удалить все «лишние» пробелы и табуляции, оставив только необходимые для разделения операторов.
// Записать символы каждой строки программы в другой файл в обратном порядке.

using System;
using System.Linq;
using System.Text;
using System.IO;

namespace Cs16_1_t02
{
    class Program
    {
        static void SaveFileSW(string inputFile, string outputFile)
        {
            using (StreamReader reader = new StreamReader(inputFile, Encoding.Unicode))
            {
                using (StreamWriter writer = new StreamWriter(outputFile, false, Encoding.Unicode))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        
                        if (words.Length > 0 && words[0] == "public")
                            words[0] = "private";
                        
                        for (int i = 0; i < words.Length; i++)
                            if (words[i].Length > 2)
                                words[i] = words[i].ToUpper();
                        
                        writer.WriteLine(string.Join(" ", words).Reverse().ToArray());
                    }
                    Console.WriteLine("\nOutput File Saved Successfully\n");
                }
            }
        }  
    
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            string inputFile = "Program_SortedArrayList.cs";
            string outputFile = "Program_SortedArrayList_2.cs";

            SaveFileSW(inputFile, outputFile);
        }
    }
}