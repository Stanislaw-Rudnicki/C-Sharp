// 1. Создать библиотеку классов с именем «ClassLib».
// 2. В библиотеке «ClassLib» создать класс с именем «РС», описывающий компьютер.
// Данный класс должен включать:
// - 3–4 поля (марка, серийный номер и т.д.),
// - свойства (к каждому полю),
// - конструкторы (по умолчанию, с параметрами),
// - методы, моделирующие функционирование ПЭВМ (включение/выключение, перегрузку).
// 3. Создать новый проект (тип — консольное приложение) с именем «SerializConsolApp».
// Добавить ссылку на библиотеку «ClassLib».
// 4. В функции Main() данного проекта создать коллекцию (на базе обобщенного класса List<T>)
// и добавить в коллекцию 4–5 объектов класса «РС».
// 5. Произвести сериализацию коллекции в бинарный файл с именем «listSerial.txt» в каталоге на диске D.
// В случае наличия аналогичного файла в каталоге старый файл перезаписать новым файлом и вывести об этом уведомление.
// 6. Создать новый проект (тип — консольное приложение) с именем «DeserializConsolApp».
// Добавить ссылку на библиотеку «ClassLib».
// 7. В функции Main() произвести десериализацию коллекции, созданной в проекте с именем «SerializConsolApp» и вывести на экран.
//
// Дополнительно:
// 8. В проекте «SerializConsolApp» реализовать функцию сохранения каждого объекта коллекции
// в отдельном каталоге с именами («объект1.txt», «объект2.txt», «объект3.txt»…).
// В проекте «DeserializConsolApp» функцию чтения объектов из файлов и вывода на экран значений полей объектов.


using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLib;

namespace SerializConsolApp
{
    class Program
    {
        static void SaveBinary<T>(string fname, T s)
        {
            try
            {
                if (File.Exists(fname))
                {
                    Console.WriteLine("Файл с таким именем уже существует. Он будет перезаписан.");
                }
                using (Stream stream = File.Create(fname))
                {
                    BinaryFormatter format = new BinaryFormatter();
                    format.Serialize(stream, s);
                }
                Console.WriteLine("SaveBinary is OK.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void SaveBinaryToSeparateFiles<T>(ICollection<T> s)
        {
            try
            {
                Directory.CreateDirectory("..\\..\\Folder");
                int i = 0;
                foreach (T item in s)
                {
                    using (Stream stream = File.Create("..\\..\\Folder\\объект" + (++i).ToString() + ".txt"))
                    {
                        BinaryFormatter format = new BinaryFormatter();
                        format.Serialize(stream, item);
                    }
                }
                Console.WriteLine("Successfully saved to " + i + " binary files.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Main(string[] args)
        {
            List<PC> PCList = new List<PC> {
            new PC("Dell", "DE56754342", new DateTime(2019, 10, 25), 0),
            new PC("HP", "HP56566765", new DateTime(2018, 11, 26), 1),
            new PC("Acer", "AC87343983", new DateTime(2018, 06, 21), 1),
            new PC("Lenovo", "LE32657548", new DateTime(2017, 12, 29), 0)};

            string fname = "..\\..\\listSerial.txt";
            SaveBinary(fname, PCList);

            SaveBinaryToSeparateFiles(PCList);
        }
    }
}
