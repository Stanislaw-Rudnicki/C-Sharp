using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLib;

namespace DeserializConsolApp
{
    class Program
    {
        static T LoadBinary<T>(string fname)
        {
            T s = default;
            try
            {
                using (Stream stream = File.OpenRead(fname))
                {
                    BinaryFormatter format = new BinaryFormatter();
                    s = (T)format.Deserialize(stream);
                }
                Console.WriteLine("LoadBinary is OK.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return s;
        }

        static List<T> LoadBinaryFromSeparateFiles<T>(string fname)
        {
            List<T> s = new List<T>();
            try
            {
                int i = 0;
                while (File.Exists("..\\..\\..\\" + fname + "\\объект" + (++i).ToString() + ".txt"))
                {
                    using (Stream stream = File.OpenRead("..\\..\\..\\" + fname + "\\объект" + (i).ToString() + ".txt"))
                    {
                        BinaryFormatter format = new BinaryFormatter();
                        s.Add((T)format.Deserialize(stream));
                    }
                }
                Console.WriteLine("Successfully loaded from " + --i + " binary files.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return s;
        }

        static void Main(string[] args)
        {
            string fname = "..\\..\\..\\listSerial.txt";
            List<PC> PCList = LoadBinary<List<PC>>(fname);
            Console.WriteLine(String.Join("\n", PCList) + "\n");

            fname = "Folder";
            List<PC> PCList2 = LoadBinaryFromSeparateFiles<PC>(fname);
            Console.WriteLine(String.Join("\n", PCList2) + "\n");
        }
    }
}
