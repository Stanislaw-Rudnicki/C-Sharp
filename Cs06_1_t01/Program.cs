// Цель: Разработать программу, моделирующую танковый бой.
// В танковом бою участвуют 5 танков «Т-34» и 5 танков «Pantera».
// Каждый танк («Т-34» и «Pantera») описываются параметрами: «Боекомплект», «Уровень брони», «Уровень маневренности».
// Значение данных параметров задаются случайными числами от 0 до 100.
// Каждый танк участвует в парной битве, т.е.первый танк «Т-34» сражается с первым танком «Pantera» и т.д.
// Победа присуждается тому танку, который превышает противника по двум и более параметрам из трех (пример: см. программу).
// Основное требование: сражение (проверку на победу в бою) реализовать путем перегрузки оператора «*» (произведение).

// 1. В решение добавить новый проект с именем «Day7(Tanks)», в котором будут промоделированы танковые сражения.
//    В данный проект добавить ссылку на библиотеку классов «MyClassLib».
// 2. В библиотеке классов «MyClassLib» создать папку «WordOfTanks», а в ней разработать класс с именем «Tank».

// В классе должно быть реализовано
// • Поля
//   закрытые поля, предназначенные для представления
// 1. Названия танка.
// 2. Уровня боекомплекта танка.
// 3. Уровня брони.
// 4. Уровня маневренности.

// • Конструктор
// Конструктор с параметрами, обеспечивающий инициализацию всех полей класса Tank.
// При этом Боекомплект, Уровень брони, Уровень маневренности инициализируются случайными числами от 0 до 100 %.
// Название танка передаются в конструктор из функции Main().

// • Перегрузка оператора «^» (произведение)
// При перегрузке оператора «^» (произведение) должна быть реализована проверка на победу в бою одного танка по отношению к другому.
// Критерий победы — победивший танк должен превышать проигравший танк не менее чем по двум из трех параметров (Боекомплект, Уровень брони, Уровень маневренности).

// • Методы:
// Получение текущих значений параметров танка: Боекомплект, Уровень брони, Уровень маневренности в виде строки.

// Важно! При разработке программы использовать обработку исключительных ситуаций.
// Варианты возможных исключительных ситуаций рассмотреть самостоятельно!


using System;
using MyClassLib;

namespace Day7__Tanks_
{
    class Program
    {
        static void Main(string[] args)
        {
            const int COUNT_OF_TANKS = 5;
            const int END_RANGE = 100 + 1;
            const string FIRST_KIND_OF_TANK = "T-34";
            const string SECOND_KIND_OF_TANK = "Pantera";
            int weapon;
            int armourLevel;
            int manoeuvrability;

            Random rnd = new Random();
            Tank[] t34 = new Tank[COUNT_OF_TANKS];
            Tank[] pantera = new Tank[COUNT_OF_TANKS];

            for (int i = 0; i < COUNT_OF_TANKS; i++)
            {
                weapon = rnd.Next(END_RANGE);
                armourLevel = rnd.Next(END_RANGE);
                manoeuvrability = rnd.Next(END_RANGE);
                t34[i] = new Tank(FIRST_KIND_OF_TANK, weapon, armourLevel, manoeuvrability);

                weapon = rnd.Next(END_RANGE);
                armourLevel = rnd.Next(END_RANGE);
                manoeuvrability = rnd.Next(END_RANGE);
                pantera[i] = new Tank(SECOND_KIND_OF_TANK, weapon, armourLevel, manoeuvrability);
            }

            Battle(t34, pantera, COUNT_OF_TANKS);
        }
        static void Battle(Tank[] t34, Tank[] pantera, int COUNT_OF_TANKS)
        {
            for (int i = 0; i < COUNT_OF_TANKS; i++)
            {
                Console.WriteLine("Пара №{0}", i + 1);
                Tank.Show(t34[i], pantera[i]);
                try
                {
                    if (t34[i] ^ pantera[i]) Console.WriteLine("Победитель сражения: T-34\n");
                    else Console.WriteLine("Победитель сражения: Pantera\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
