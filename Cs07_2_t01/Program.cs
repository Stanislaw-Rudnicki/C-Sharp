// Створити абстрактний базовий клас «Транспортний засіб»
// Розробіть похідні класи зображенні на малюнку.
// Засоби масового перевезення людей, Сільськогосподарська техніка,
// Легкові автомобілі, Вантажні автомобілі, Мотоцикли, Скутери.
// Пасажиру потрібно проїхати з пункту А до пункту В з використанням різних транспортних засобів.
// Кількість транспортних засобів генерується випадково.
// Обчислити час і вартість проїзду пасажира.

using System;
using System.Text;
using System.Linq;

namespace Cs07_2_t01
{
    abstract class Vehicle
    {
        protected double avgSpeed;
        protected double avgCost;
        public double Distance { get; set; }
        public Vehicle(double dist, double speed, double cost) { Distance = dist;  avgSpeed = speed; avgCost = cost; }
        public TimeSpan Time()
        {
            return TimeSpan.FromSeconds(Distance / (avgSpeed / 3600));
        }
        public virtual double Cost()
        {
            return Distance * avgCost;
        }
    }

    class PublicTransport : Vehicle
    {
        public PublicTransport(double dist) : base(dist, 30, 10) { }
        public override double Cost()
        {
            return avgCost;
        }
        public override string ToString()
        {
            return $"громадським транспортом";
        }
    }

    class AgriculturalMachinery : Vehicle
    {
        public AgriculturalMachinery(double dist) : base(dist, 40, 50) { }
        public override string ToString()
        {
            return $"сільськогосподарською технікою";
        }
    }

    class Car : Vehicle
    {
        public Car(double dist) : base(dist, 90, 5) { }
        public override string ToString()
        {
            return $"легковим автомобілем";
        }
    }

    class Truck : Vehicle
    {
        public Truck(double dist) : base(dist, 80, 15) { }
        public override string ToString()
        {
            return $"вантажним автомобілем";
        }
    }

    class Motorcycle : Vehicle
    {
        public Motorcycle(double dist) : base(dist, 100, 4) { }
        public override string ToString()
        {
            return $"мотоциклом";
        }
    }

    class Scooter : Vehicle
    {
        public Scooter(double dist) : base(dist, 50, 3) { }
        public override string ToString()
        {
            return $"скутером";
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Random rnd = new Random();
            int COUNT_OF_VECHICLES = rnd.Next(5, 11);
            int DST, DISTANCE = DST = rnd.Next(100, 1001);

            Vehicle[] arr = new Vehicle[COUNT_OF_VECHICLES];
            for (int i = 0; i < arr.Length; i++)
            {
                int D = rnd.Next(DISTANCE + 1);
                DISTANCE -= D;
                int R = rnd.Next(6);
                if (R == 0) arr[i] = new PublicTransport(D);
                else if (R == 1) arr[i] = new AgriculturalMachinery(D);
                else if (R == 2) arr[i] = new Car(D);
                else if (R == 3) arr[i] = new Truck(D);
                else if (R == 4) arr[i] = new Motorcycle(D);
                else arr[i] = new Scooter(D);
            }

            TimeSpan totalTime = TimeSpan.FromSeconds(0);
            double TotalCost = 0;

            foreach (var item in arr.Where(n => n.Distance != 0))
            {
                totalTime += item.Time();
                TotalCost += item.Cost();
                Console.WriteLine("Пасажир проїхав " + item);
                Console.WriteLine($"{item.Distance} км, протягом { item.Time().Hours} год. { item.Time().Minutes} хв. { item.Time().Seconds} с,");
                Console.WriteLine("вартість проїзду: " + item.Cost() + " грн.\n");
            }

            Console.WriteLine("\nЗагалом пасажир подолав " + DST + " км.");
            Console.WriteLine($"Подорож тривала {totalTime.Hours} год. {totalTime.Minutes} хв. {totalTime.Seconds} с,");
            Console.WriteLine("і коштувала " + TotalCost + " грн.\n");
        }
    }
}
