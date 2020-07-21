// Создать модель карточной игры.
// Требования:
// 1. Класс Game формирует и обеспечивает:
// 1.1.1. Список игроков (минимум 2);
// 1.1.2. Колоду карт (36 карт);
// 1.1.3. Перетасовку карт (случайным образом);
// 1.1.4. Раздачу карт игрокам (равными частями каждому игроку);
// 1.1.5. Игровой процесс. Принцип: Игроки кладут по одной карте.
// У кого карта больше, то тот игрок забирает все карты и кладет их в конец своей колоды.
// Упрощение: при совпадении карт забирает первый игрок, шестерка не забирает туза.
// Выигрывает игрок, который забрал все карты.
// 2. Класс Player (набор имеющихся карт, вывод имеющихся карт).
// 3. Класс Karta (масть и тип карты (6–10, валет, дама, король, туз)).

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cs12_1_t01
{
    class Card
    {
        public int Suit { get; set; }
        public int Value { get; set; }
        public Card(int suit, int value)
        {
            Suit = suit;
            Value = value;
        }
        public override string ToString()
        {
            char[] suitsList = { '\u2665', '\u2666', '\u2663', '\u2660' };
            // 0 - червы, 1 - бубны, 2 - трефы, 3 - пики
            string[] valuesList = { " 6", " 7", " 8", " 9", "10", " В", " Д", " К", " Т" };
            return $"{valuesList[Value]}{suitsList[Suit]}";
        }
    }

    class Deck
    {
        Card[] deck;
        public int Stock { get; private set; }
        public Deck()
        {
            deck = new Card[36];
            int i = 0;
            for (int suit = 0; suit <= 3; ++suit)
            {
                for (int value = 0; value <= 8; ++value)
                {
                    deck[i] = new Card(suit, value);
                    ++i;
                }
            }
            Stock = 36;
        }
        public void Shuffle()
        {
            Random rnd = new Random();
            for (int i = 0; i < Stock - 1; ++i)
            {
                int k = rnd.Next(i, Stock);
                Card tmp = deck[i];
                deck[i] = deck[k];
                deck[k] = tmp;
            }
        }
        public Card Pop()
        {
            Card tmp = deck[--Stock];
            deck[Stock] = null;
            return tmp;
        }
        public override string ToString()
        {
            StringWriter strWriter = new StringWriter();
            for (int i = 0; i < Stock; i++)
            {
                strWriter.Write($"{deck[i]}\t");
                if ((i + 1) % 9 == 0) strWriter.WriteLine();
            }
            return strWriter.ToString();
        }
    }

    class Player
    {
        public Stack<Card> Cards = new Stack<Card>();
        public void ShowCards() => Console.WriteLine(string.Join(", ", Cards.Reverse()));
    }

    class Game
    {
        public List<Player> Players = new List<Player>();
        public Game(int n)
        {
            if (n < 2) n = 2;
            if (n > 6) n = 6;
            Deck D = new Deck();
            D.Shuffle();
            for (int i = 0; i < n; i++)
            {
                Players.Add(new Player());
            }
            for (int i = 0; i < 36;)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i < 36)
                    {
                        Players[j].Cards.Push(D.Pop());
                        i++;
                    }
                }
            }

        }
        public void Start()
        {
            while (!Players.Any(p => p.Cards.Count == 36))
            {
                Console.Clear();
                Card[] table = new Card[Players.Count];
                int maxCardIndex = 0;
                Card maxCard = null;
                for (int i = 0; i < Players.Count; i++)
                {
                    if (Players[i].Cards.Count != 0)
                    {
                        Card card = Players[i].Cards.Pop();
                        if (maxCard == null || maxCard.Value < card.Value)
                        {
                            maxCard = card;
                            maxCardIndex = i;
                        }
                        table[i] = card;
                    }
                }

                for (int i = 0; i < Players.Count; i++)
                {
                    Console.Write($"Игрок {i + 1}: ");
                    Players[i].ShowCards();
                    Console.WriteLine();
                }
                Console.Write($"Походили:\t");
                for (int i = 0; i < Players.Count; i++)
                {
                    Console.Write($"{i + 1}: ");
                    Console.Write(table[i] + "\t\t");
                }
                Console.WriteLine("\n");
                Array.ForEach(table, card => { if (card != null) Players[maxCardIndex].Cards.Push(card); });

                //Console.ReadKey(true);
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine($"Выиграл Игрок {Players.FindIndex(p => p.Cards.Count == 36) + 1}.\n");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            int n = 0;
            bool f = false;
            while (!f)
            {
                Console.Write("Задайте количество игроков (2-6): ");
                f = int.TryParse(Console.ReadLine(), out n);
            }
            Game G = new Game(n);
            G.Start();
        }
    }
}
