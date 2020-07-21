// Написать приложение, имитирующее работу банкомата
// Реализовать классы Bank, Client, Account. Изначально клиенту нужно открыть счёт в банке,
// получить номер счёта, получить свой пароль, положить сумму на счёт.
// 1. Приложение предлагает ввести пароль предполагаемой кредитной карточки, даётся 3 попытки на правильный ввод пароля.
//    Если попытки исчерпаны, приложение выдаёт соответствующее сообщение и завершается.
// 2. При успешном вводе пароля выводится меню. Пользователь может выбрать одно из нескольких действий:
//    - вывод баланса на экран
//    - пополнение счёта
//    - снять деньги со счёта
//    - выход
// 3. Если пользователь выбирает вывод баланса на экран, приложение отображает состояние предполагаемого счёта,
//    после чего предлагает либо вернуться в меню, либо совершить выход.
// 4. Если пользователь выбирает пополнение счёта, программа запрашивает сумму для пополнения и выполняет операцию,
//    сопровождая её выводом соответствующего комментария. Затем следует предложение вернуться в меню или выполнить выход.
// 5. Если пользователь выбирает снять деньг со счёта, программа запрашивает сумму.
//    Если сумма превышает сумму счёта пользователя, программа выдаёт сообщение и переводит пользователя в меню.
//    Иначе отображает сообщение о том, что сумма снята со счёта и уменьшает сумму счёта на указанную величину.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    class Menu
    {
        const ConsoleColor FOREGROUND = ConsoleColor.Gray;
        const ConsoleColor ITEMSELECT = ConsoleColor.White;
        const int MENULEFT = 20;
        const int MENUTOP = 4;
        static string[] menuItems;
        public static string[] MenuItems { set { menuItems = value; } }
        public static int Selected { get; private set; } = 0;
        static void PaintMenu()
        {
            Console.ForegroundColor = FOREGROUND;
            Console.Clear();
            Console.SetCursorPosition(MENULEFT, MENUTOP);
            Console.WriteLine("......MENU......\n");
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.SetCursorPosition(MENULEFT, MENUTOP + i + 1);
                if (i == Selected)
                {
                    Console.ForegroundColor = ITEMSELECT;
                    Console.Write("=>");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.WriteLine(menuItems[i]);
                Console.ForegroundColor = FOREGROUND;
            }
        }
        public static void MenuSelect()
        {
            ConsoleKey c = ConsoleKey.DownArrow;
            while (true)
            {
                if (c == ConsoleKey.UpArrow || c == ConsoleKey.DownArrow)
                {
                    PaintMenu();
                }
                c = Console.ReadKey().Key;
                switch (c)
                {
                    case ConsoleKey.Escape: //Esc
                        Selected = -1;
                        return;
                    case ConsoleKey.DownArrow: //down
                        ++Selected;
                        if (Selected == menuItems.Length) Selected = 0;
                        break;
                    case ConsoleKey.UpArrow://up
                        if (Selected == 0) Selected = menuItems.Length;
                        --Selected;
                        break;
                    case ConsoleKey.Enter: //Enter
                        return;
                }
            }
        }

    }

    class Account
    {
        public string Currency { get; }
        public double Amount { get; set; }
        public ulong AccountNumber { get; }
        public string Pin { get; set; }
        public Account(string cur)
        {
            Currency = cur;
            AccountNumber = ulong.Parse(DateTime.Now.ToString("yyyyddHHmmssffff"));
            Amount = 0;
            Pin = "0000";
        }
    }

    class Client
    {
        public static int count;
        public int Id { get; }
        public string Name { get; }
        List<Account> accounts = new List<Account>();
        public Client(string n)
        {
            Name = n;
            accounts.Add(new Account("UAH"));
            Id = ++count;
        }
        public ulong GetAccountNumber(string cur)
        {
            return accounts.Where(i => i.Currency == cur).FirstOrDefault().AccountNumber;
        }
        public void SetPinCode(string pin, ulong num)
        {
            accounts.Where(i => i.AccountNumber == num).FirstOrDefault().Pin = pin;
        }
        public string GetPinCode(ulong num)
        {
            return accounts.Where(i => i.AccountNumber == num).FirstOrDefault().Pin;
        }
        public double GetAmount(ulong num)
        {
            return accounts.Where(i => i.AccountNumber == num).FirstOrDefault().Amount;
        }
        public void Transaction(int sum, ulong num)
        {
            accounts.Where(i => i.AccountNumber == num).FirstOrDefault().Amount += sum;
        }
    }

    class Bank
    {
        public string BankName { get; set; }
        public int CurrentClient { get; set; }
        List<Client> clients = new List<Client>();
        string PinInput(int len, bool hide = false)
        {
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter &&
                    (key.Key >= ConsoleKey.D0 && key.Key <= ConsoleKey.D9 || key.Key >= ConsoleKey.NumPad0 && key.Key <= ConsoleKey.NumPad9) &&
                    pass.Length < len)
                {
                    pass += key.KeyChar;
                    if (hide) Console.Write("*");
                    else Console.Write(key.KeyChar);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter && (hide ? pass.Length == len : pass.Length <= len))
                    {
                        break;
                    }
                }
            } while (true);
            return pass;
        }
        bool PinCheck()
        {
            int attempt = 1;
            string pinEntered = "";
            string pinStored = clients.Where(i => i.Id == CurrentClient).FirstOrDefault().
                GetPinCode(clients.Where(i => i.Id == CurrentClient).FirstOrDefault().GetAccountNumber("UAH"));
            do
            {
                Console.Write("\nВведите ПИН-код (4 цифры)" + (attempt > 1 ? ", " + attempt + " попытка: " : ": "));
                pinEntered = PinInput(4, true);
                if (pinEntered == pinStored)
                    return true;
                attempt++;
            } while (attempt < 4);
            return false;
        }
        public void NewClient()
        {
            Console.Write("Чтобы открыть счёт введите ФИО: ");
            string name = Console.ReadLine();
            clients.Add(new Client(name));
            Console.WriteLine("\n" + clients.Where(i => i.Id == Client.count).FirstOrDefault().Name + ", поздравляем, Ваш счет открыт!");
            ulong AccountNumber = clients.Where(i => i.Id == Client.count).FirstOrDefault().GetAccountNumber("UAH");
            Console.WriteLine("Номер счёта в UAH: " + AccountNumber + "\n");
            CurrentClient = Client.count;
            do
            {
                Console.Write("Придумайте ПИН-код (4 цифры): ");
                string pass1 = PinInput(4, true);
                Console.Write("\nПовторите ПИН-код: ");
                string pass2 = PinInput(4, true);
                if (pass1 == pass2)
                {
                    clients.Where(i => i.Id == Client.count).FirstOrDefault().SetPinCode(pass1, AccountNumber);
                    break;
                }
                else Console.WriteLine("\n\nПИН-коды не совпадают. Попробуйте ещё раз!");
            } while (true);

            Console.WriteLine("\nПИН-код изменен!");

            Console.Write("\nНа какую сумму пополнить счёт? ");
            int sum = int.Parse(PinInput(10));
            clients.Where(i => i.Id == Client.count).FirstOrDefault().Transaction(sum, AccountNumber);
            Console.WriteLine("\nВаш счёт пополнен!");
            Console.WriteLine("\nТеперь можете воспользоваться банкоматом.\nДля продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
        public void Atm()
        {
            Console.Clear();
            if (PinCheck())
            {
                Menu.MenuItems = new string[] {
                    "Вывод баланса на экрану",
                    "Пополнение счёта",
                    "Снять деньги со счёта",
                    "Выход" };

                while (true)
                {
                    Menu.MenuSelect();
                    Console.Clear();
                    if (Menu.Selected < 0) break;
                    if (Menu.Selected == 0)
                    {
                        Console.Write("Баланс: " + clients.Where(i => i.Id == CurrentClient).FirstOrDefault().
                    GetAmount(clients.Where(i => i.Id == CurrentClient).FirstOrDefault().GetAccountNumber("UAH")) + " грн.\n");
                    };
                    if (Menu.Selected == 1)
                    {
                        Console.Write("На какую сумму пополнить счёт? ");
                        int sum = int.Parse(PinInput(10));
                        clients.Where(i => i.Id == CurrentClient).FirstOrDefault().
                            Transaction(sum, clients.Where(i => i.Id == CurrentClient).FirstOrDefault().GetAccountNumber("UAH"));
                        Console.WriteLine("\n\nВаш счёт пополнен!\n");
                    }
                    if (Menu.Selected == 2)
                    {
                        double amount = clients.Where(i => i.Id == CurrentClient).FirstOrDefault().
                            GetAmount(clients.Where(i => i.Id == CurrentClient).FirstOrDefault().GetAccountNumber("UAH"));
                        Console.Write("Какую сумму хотите получить? ");
                        int sum = int.Parse(PinInput(10));
                        if (sum < amount)
                        {
                            clients.Where(i => i.Id == CurrentClient).FirstOrDefault().
                                Transaction(-sum, clients.Where(i => i.Id == CurrentClient).FirstOrDefault().GetAccountNumber("UAH"));
                            Console.WriteLine("\n\nВозьмите деньги!\nНе забудьте забрать карту!\n");
                        }
                        else
                        {
                            Console.Write("\n\nБаланс Вашего счета меньше указанной суммы.\n");
                        }
                    };
                    if (Menu.Selected == 3)
                    {
                        break;
                    };
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\ndone\n");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\n\nВы 3 раза ввели неверный ПИН-код.\nВаша карта изъята! До свидания!\n");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Bank bank = new Bank { BankName = "Учебный" };
            Console.WriteLine("Добро пожаловать в банк \"" + bank.BankName + "\"!");
            bank.NewClient();
            bank.Atm();
            
		}
    }
}
