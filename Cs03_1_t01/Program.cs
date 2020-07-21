// 1.1	Разработать один из классов, в соответствии с полученным вариантом.
// 1.2	Реализовать не менее пяти закрытых полей (различных типов), представляющих основные характеристики рассматриваемого класса.
// 1.3	Создать не менее трех методов управления классом и методы доступа к его закрытым полям. 
// 1.4	Создать метод, в который передаются аргументы по ссылке. 
// 1.5	Создать не менее двух статических полей  (различных типов), представляющих общие характеристики объектов данного класса.  
// 1.6	Обязательным требованием является реализация нескольких перегруженных конструкторов, аргументы которых определяются студентом, исходя из специфики реализуемого класса, а так же реализация конструктора по умолчанию.
// 1.7	Создать статический конструктор.
// 1.8	Создать массив (не менее 5 элементов) объектов созданного класса.
// 1.9	Создать дополнительный метод для данного класса в другом файле, используя ключевое слово partial.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace case_1
{
    partial class  Car
    {
        private int mileage;
        private string make;
        private bool gasoline;
        private int enginePower;
        private int maxSpeed;

        public static string carForeign;
        public static int YearOfIssueNotLower;              
        
        public Car()
        {
            mileage = 0;
            make = "Default";
            gasoline = false;
            enginePower = 0;
            maxSpeed = 0;
            //Print();
        }       
        public Car(int _milage, string _mark, bool _Gasoline, int _enginePower, int _maxSpeed)
        {
            mileage = _milage;
            make = _mark;
            gasoline = _Gasoline;
            enginePower = _enginePower;
            maxSpeed = _maxSpeed;
            Print();
        }
        public Car(string _mark)
        {
            make = _mark;
            Print();
        }
        
        public void Print()
        {
            Console.WriteLine("Mileage = " + mileage);
            Console.WriteLine("Make = " + make);
            Console.WriteLine("Gasoline = " + gasoline);
            Console.WriteLine("Engine Power = " + enginePower);
            Console.WriteLine("Max Speed = " + maxSpeed);
        }
        public void Print(ref Car obj)
        {
            Console.WriteLine("Milage = " + obj.mileage);
            Console.WriteLine("Make = " + obj.make);
            Console.WriteLine("Gasoline = " + obj.gasoline);
            Console.WriteLine("EnginePower = " + obj.enginePower);
            Console.WriteLine("MaxSpeed = " + obj.maxSpeed);
        }      
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car Car1 = new Car();
            Car Car2 = new Car("Renault");
            Car[] car5Cars = new Car[5];
            for (int i = 0; i < car5Cars.Length; i++)
            {
                car5Cars[i] = new Car();
            }
            
            //Car2.Print();
            Console.WriteLine();
            Car1.MaxSpeed = 9;
            Console.WriteLine(Car1.MaxSpeed);
            Car1.Print(ref Car1);
            Console.WriteLine();
            Car1.Print();
            Console.WriteLine();
            Console.WriteLine(Car.carForeign);
        }
    }
}