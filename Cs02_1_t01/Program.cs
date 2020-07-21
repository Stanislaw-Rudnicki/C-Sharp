using System;
using System.Linq;
using System.Text;

namespace _07032020
{
    class Program
    {
        // Користувач вводить рядок, який складається з символів.
        // Символи складаються вслова, які розділяються розділовими знаками.
        // Слова відокремлюються одним або декількома пробілами.
        // Виконати обробку рядка відповідно до завдання.
        // 1. Надрукувати найдовше і найкоротше слово в цьому рядку.
        // 2. Надрукувати всі слова, які не містять голосних букв.
        // 3. Надрукувати всі слова, які складаються лише з цифр.
        // 4. Надрукувати всі слова, які збігаються з першим словом.
        // 5. Перетворити рядок так, щоб всі букви в кожному слові були відсортовані за алфавітом.
        // 6. Перетворити рядок так, щоб всі слова були відсортовані за алфавітом у зворотному порядку.
        // 7. Прибрати з рядка всі зазначені символи, які введе користувач.
        // 8. Підрахувати частоту входження символів у введеному тексті. Відсортуйте статистику за спаданням.

        static void Zad01()
        {
            string str = "Дано целое число N большее 0, найти число, полученное   при прочтении числа N справа налево. " +
                "Например, если было  дано число 345, то программа  должна вывести    число 543.";
            Console.WriteLine(str + "\n");
            
            char[] sep = ".,+& -*?!<>«»".ToArray();
            string[] words = str.Split(sep, StringSplitOptions.RemoveEmptyEntries);


            Console.WriteLine("1. Надрукувати найдовше і найкоротше слово в цьому рядку.");

            string[] sortedWords = words.OrderBy(f => f.Length).ThenBy(f => f).ToArray();
            Console.WriteLine("\n" + sortedWords.First() + " | " + sortedWords.Last() + "\n");


            Console.WriteLine("2. Надрукувати всі слова, які не містять голосних букв.\n");

            char[] vowel = "ауоыиэяюёеієї".ToArray();
            foreach (var word in words)
                if (!vowel.Any(word.Contains))
                    Console.Write(word + " | ");
            Console.WriteLine("\b\b\0\n");


            Console.WriteLine("3. Надрукувати всі слова, які складаються лише з цифр.\n");
            
            foreach (var word in words)
                if (word.All(Char.IsDigit))
                    Console.Write(word + " | ");
            Console.WriteLine("\b\b\0\n");


            Console.WriteLine("4. Надрукувати всі слова, які збігаються з першим словом.\n");
            
            foreach (var word in words)
                if (String.Compare(words.First(), word, StringComparison.CurrentCultureIgnoreCase) == 0)
                    Console.Write(word + " | ");
            Console.WriteLine("\b\b\0\n");


            Console.WriteLine("5. Перетворити рядок так, щоб всі букви в кожному слові були відсортовані за алфавітом.\n");
            
            sep = " ".ToArray();
            words = str.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                char[] w = word.ToArray();
                Array.Sort(w, (a, b) => char.ToLower(a) - char.ToLower(b));
                if (Char.IsPunctuation(w.First()))
                {
                    foreach (var ch in w.Skip(1))
                        Console.Write(ch);
                    Console.Write(w.First());
                }
                else
                    Console.Write(w);
                Console.Write(" ");
            }
            Console.WriteLine("\b\0\n");


            Console.WriteLine("6. Перетворити рядок так, щоб всі слова були відсортовані за алфавітом у зворотному порядку.\n");

            sortedWords = words.OrderByDescending(f => f).ToArray();
            foreach (var word in sortedWords)
            {
                if (Char.IsPunctuation(word[0]))
                    Console.Write(word.Substring(1, word.Length) + word[0]);
                else
                    Console.Write(word);
                Console.Write(" ");
            }
            Console.WriteLine("\b\0\n");


            Console.WriteLine("7. Прибрати з рядка всі зазначені символи, які введе користувач.\n");

            Console.Write("Які символи прибрати: ");
            string str2 = String.Join("", str.Split(Console.ReadLine().ToArray()));
            Console.WriteLine("\n" + str2 + "\n");


            Console.WriteLine("8. Підрахувати частоту входження символів у введеному тексті. Відсортуйте статистику за спаданням.\n");
            
            var map = str.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).OrderByDescending(f => f.Count).ThenBy(f => f.Char);
            foreach(var i in map)
                Console.Write(i.Char + " = " + i.Count + ", ");
            Console.WriteLine("\b\b\0\n");
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Zad01();
        }
    }
}
