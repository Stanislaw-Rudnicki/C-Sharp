// Игра «Автомобильные гонки»
// Разработать игру "Автомобильные гонки" с использованием делегатов.
// 1. В игре использовать несколько типов автомобилей:
// спортивные, легковые, грузовые и автобусы.
// 2. Реализовать игру «Гонки».
// Принцип игры: Автомобили двигаются от старта к финишу со скоростями,
// которые изменяются в установленных пределах случайным образом.
// Победителем считается автомобиль, пришедший к финишу первым.
// Рекомендации по выполнению работы
// 1. Разработать абстрактный класс «автомобиль» (класс Car).
// Собрать в нем все общие поля, свойства (например, скорость) методы (например, ехать).
// 2. Разработать классы автомобилей с конкретной реализацией конструкторов и методов, свойств.
// В классы автомобилей добавить необходимые события (например, финиш).
// 3. Класс игры должен производить запуск соревнований автомобилей, выводить сообщения о текущем
// положении автомобилей, выводить сообщение об автомобиле, победившем в гонках.
// Создать делегаты, обеспечивающие вызов методов из классов автомобилей
// (например, выйти на старт, начать гонку).
// 4. Игра заканчивается, когда какой-то из автомобилей проехал определенное расстояние
// (старт в положении 0, финиш в положении 100).
// Уведомление об окончании гонки (прибытии какого-либо автомобиля на финиш) реализовать с помощью событий.


using System;
using System.Text;

namespace Cs15_1_t01
{
    delegate void MoveAction(); // делегат перемещения
    delegate void PosSetter(int Position); // установка позиции
    delegate void FinishAction(object Winner); // обратная связь

    class Game
    {
        protected bool isGameStarted = false;

        public MoveAction Move;
        public PosSetter MoveTo;

        public object Winner;

        public void Run()
        {
            // перемещаем всех на старт
            MoveTo(0);
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            
            // двигаем всех вперед, пока кто-нибудь не приедет к финишу
            isGameStarted = true;
            while (isGameStarted)
            {
                Move();
                System.Threading.Thread.Sleep(500);
                Console.Clear();
            }
        }

        public void OnFinish(object Winner)
        {
            // кто-то приехал к финишу
            isGameStarted = false;
            this.Winner = Winner;
        }
    }

    // Абстрактный класс «автомобиль»
    abstract class Car
    {
        public FinishAction Finish;
        public string Name;

        int _pos;
        public int Position
        {
            get { return _pos; }
            set
            {
                _pos = value;
                Console.WriteLine(this + " на позиции " + _pos);
                if (_pos >= 100) Finish(this);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public void JoinGame(Game Game)
        {
            // Подписываемся на игру
            Game.Move += this.Move;
            Game.MoveTo += this.MoveTo;
            Finish = Game.OnFinish;
        }

        public abstract void Move();

        public void MoveTo(int Position)
        {
            this.Position = Position;
        }
    }

    class PassengerCar : Car
    {
        public int Speed;
        public override void Move()
        {
            Position += Speed;
        }
    }

    class SportCar : Car
    {
        public int Speed;
        public override void Move()
        {
            Position += Speed;
        }
    }

    class Truck : Car
    {
        public int Speed;
        public override void Move()
        {
            Position += Speed;
        }
    }

    class Bus : Car
    {
        public int Speed;
        public override void Move()
        {
            Position += Speed;
        }
    }

    static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Game game = new Game();

            Random rnd = new Random();
            Car car1 = new PassengerCar() { Name = "Легковой", Speed = rnd.Next(10, 12) };
            Car car2 = new SportCar() { Name = "Спортивный", Speed = rnd.Next(12, 20) };
            Car car3 = new Truck() { Name = "Грузовой", Speed = rnd.Next(7, 10) };
            Car car4 = new Bus() { Name = "Автобус", Speed = rnd.Next(8, 10) };

            // Подписываемся на участие в игре
            car1.JoinGame(game);
            car2.JoinGame(game);
            car3.JoinGame(game);
            car4.JoinGame(game);

            game.Run();

            Console.WriteLine("Победил " + game.Winner + " автомобиль\n");
        }
    }
}