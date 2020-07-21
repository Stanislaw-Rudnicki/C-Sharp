// Розробити клас TryPassword для перевірки пароля до заданих правил:
// - Довжина пароля повинна бути рівна або більша за зазначену.
// - Пароль повинен містити малі літери a…z;
// - Пароль повинен містити великі літери A…Z;
// - Пароль повинен містити арабські цифри;
// - Пароль повинен містити спеціальні символи %, *, ), ?, @, #, $, ~, …
// Кількість правил для перевірки встановлюється через конструктор.
// Для перевірки клас повинен мати функцію з параметром рядкового типу.
// Функція повертає false у випадку не відповідності пароля одному із встановлених правил генерується відповідна виняткова ситуація, інакше – true.

using System;
using System.Linq;
using System.Text;

namespace Cs10_1_t01
{
    class TryPassword
    {
        private readonly int rules;
        private int pwdLength;
        public int PwdLength
        {
            get => pwdLength;
            set => pwdLength = Math.Abs(value);
        }
        public TryPassword(int n, int r = 8)
        {
            if (n < 1) rules = 1;
            else if (n > 5) rules = 5;
            else rules = n;
            PwdLength = r;
        }
        public bool Check(string pwd)
        {
            switch (rules)
            {
                case 5:
                    if (pwd.Any(Char.IsPunctuation) || pwd.Any(Char.IsSymbol))
                        goto case 4;
                    throw new ArgumentException(message: "Password does not contain special characters!");
                case 4:
                    if (pwd.Any(Char.IsDigit))
                        goto case 3;
                    throw new ArgumentException(message: "Password does not contain digits!");
                case 3:
                    if (pwd.Any(Char.IsUpper))
                        goto case 2;
                    throw new ArgumentException(message: "Password does not contain uppercase letters!");
                case 2:
                    if (pwd.Any(Char.IsLower))
                        goto case 1;
                    throw new ArgumentException(message: "Password does not contain lowercase letters!");
                case 1:
                    if (pwd.Length > PwdLength)
                        break;
                    throw new ArgumentException(message: "Invalid password length!");
            };
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            TryPassword Tr = new TryPassword(5);
            bool f = false;
            while (!f)
            {
                Console.Write("Pick a password: ");
                try
                {
                    f = Tr.Check(Console.ReadLine());
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            }
            Console.WriteLine("OK, your password meets the requirements!\n");
        }
    }
}