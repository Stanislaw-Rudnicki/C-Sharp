// Уявімо, ви робите систему фільтрації коментарів на якомусь веб-порталі, будь то новини, відео-хостинг, …
// Ви хочете фільтрувати коментарі за різними критеріями, вміти легко додавати нові фільтри і модифікувати старі.
// Припустимо, ми будемо фільтрувати спам, коментарі з негативним змістом і занадто довгі коментарі.
// Спам будемо фільтрувати за наявністю зазначених ключових слів в тексті.
//
// Негативний зміст визначатимемо за наявністю одного з трьох смайликів:
// :(, =(, :|
//
// Занадто довгі коментарі будемо визначати виходячи з даного числа - максимальної довжини коментаря.
// Ви вирішили абстрагувати фільтр у вигляді наступного інтерфейсу:
//
// interface TextAnalyzer
// {
//    Label processText(String text);
// }
//
// Label - тип-перерахування, які містить мітки, якими будемо позначати текст:
// enum Label { SPAM, NEGATIVE_TEXT, TOO_LONG, OK }
//
// Далі, вам необхідно реалізувати три класи, які реалізують даний інтерфейс: SpamAnalyzer, NegativeTextAnalyzer і TooLongTextAnalyzer.
// SpamAnalyzer повинен конструюватися від масиву рядків з ключовими словами.
// Об'єкт цього класу повинен зберігати в своєму стані цей масив рядків в приватному поле keywords.
// NegativeTextAnalyzer повинен конструюватися конструктором за замовчуванням.
// TooLongTextAnalyzer повинен конструюватися від int'а з максимальною допустимою довжиною коментаря.
// Об'єкт цього класу повинен зберігати в своєму стані це число в приватному поле maxLength.
//
// Напевно ви вже помітили, що SpamAnalyzer і NegativeTextAnalyzer багато в чому схожі - 
// вони обидва перевіряють текст на наявність будь-яких ключових слів
// (в разі спаму ми отримуємо їх з конструктора, в разі негативного тексту ми заздалегідь знаємо набір сумних смайликів)
// і в разі знаходження одного з ключових слів повертають Label(SPAM і NEGATIVE_TEXT відповідно), а якщо нічого не знайшлося - повертають OK.
// Давайте цю логіку абстрагуємося в абстрактний клас KeywordAnalyzer наступним чином:
//
// Виділимо два абстрактних методу getKeywords і getLabel, один з яких буде повертати набір ключових слів, 
// а другий мітку, якої необхідно позначити позитивні спрацьовування.
// Нам нема чого показувати ці методи споживачам класів, тому залишимо доступ до них тільки для спадкоємців.
//
// Реалізуємо processText таким чином, щоб він залежав тільки від getKeywords і getLabel.
// Зробимо SpamAnalyzer і NegativeTextAnalyzer спадкоємцями KeywordAnalyzer і реалізуємо абстрактні методи.
//
// Останній штрих - написати метод checkLabels, який буде повертати мітку для коментаря по набору аналізаторів тексту. 
// checkLabels повинен повертати першим не-OK мітку в порядку даного набору аналізаторів, і OK, якщо все аналізатори повернули OK.
// Використовуйте, будь ласка, модифікатор доступу за замовчуванням для всіх класів.
// У підсумку, реалізуйте класи KeywordAnalyzer, SpamAnalyzer, NegativeTextAnalyzer і TooLongTextAnalyzer і метод checkLabels.
// TextAnalyzer і Label вже підключені, зайві обсяги імпорту вам не будуть потрібні.

using System;

namespace Cs08_1_t01
{
    interface ITextAnalyzer
    {
        Label ProcessText(String text);
    }

    enum Label { SPAM, NEGATIVE_TEXT, TOO_LONG, OK }

    internal abstract class KeywordAnalyzer : ITextAnalyzer
    {
        protected abstract String[] GetKeywords();
        protected abstract Label GetLabel();
        public Label ProcessText(String text)
        {
            String[] keywords = GetKeywords();
            foreach (String keyword in keywords)
            {
                if (text.Contains(keyword))
                {
                    return GetLabel();
                }
            }
            return Label.OK;
        }
    }

    internal class SpamAnalyzer : KeywordAnalyzer
    {
        private String[] keywords;
        public SpamAnalyzer(String[] keywords)
        {
            this.keywords = keywords;
        }
        protected override String[] GetKeywords()
        {
            return keywords;
        }
        protected override Label GetLabel()
        {
            return Label.SPAM;
        }
    }

    internal class NegativeTextAnalyzer : KeywordAnalyzer
    {
        private String[] keywords = { ":(", "=(", ":|" };
        protected override String[] GetKeywords()
        {
            return keywords;
        }
        protected override Label GetLabel()
        {
            return Label.NEGATIVE_TEXT;
        }
    }

    internal class TooLongTextAnalyzer : ITextAnalyzer
    {
        private int maxLength;
        public TooLongTextAnalyzer(int commentMaxLength)
        {
            maxLength = commentMaxLength;
        }
        public Label ProcessText(String text)
        {
            return text.Length > maxLength ? Label.TOO_LONG : Label.OK;
        }
    }


    class Program
    {
        public static Label CheckLabels(ITextAnalyzer[] analyzers, String text)
        {
            for (int i = 0; i < analyzers.Length; i++)
                if (analyzers[i].ProcessText(text) != Label.OK)
                    return analyzers[i].ProcessText(text);
            return Label.OK;
        }

        public static void Main(string[] args)
        {
            // ініціалізація аналізаторів для перевірки в порядку даного набору аналізаторів
            String[] spamKeywords = { "spam", "bad" };
            int commentMaxLength = 40;
            ITextAnalyzer[] textAnalyzers1 = {
            new SpamAnalyzer (spamKeywords),
            new NegativeTextAnalyzer (),
            new TooLongTextAnalyzer (commentMaxLength)
            };
            ITextAnalyzer[] textAnalyzers2 = {
            new SpamAnalyzer (spamKeywords),
            new TooLongTextAnalyzer (commentMaxLength),
            new NegativeTextAnalyzer ()
            };
            ITextAnalyzer[] textAnalyzers3 = {
            new TooLongTextAnalyzer (commentMaxLength),
            new SpamAnalyzer (spamKeywords),
            new NegativeTextAnalyzer ()
            };
            ITextAnalyzer[] textAnalyzers4 = {
            new TooLongTextAnalyzer (commentMaxLength),
            new NegativeTextAnalyzer (),
            new SpamAnalyzer (spamKeywords)
            };
            ITextAnalyzer[] textAnalyzers5 = {
            new NegativeTextAnalyzer (),
            new SpamAnalyzer (spamKeywords),
            new TooLongTextAnalyzer (commentMaxLength)
            };
            ITextAnalyzer[] textAnalyzers6 = {
            new NegativeTextAnalyzer (),
            new TooLongTextAnalyzer (commentMaxLength),
            new SpamAnalyzer (spamKeywords)
            };
            // тестові коментарі
            String[] tests = new String[8];
            tests[0] = "This comment is so good."; // OK
            tests[1] = "This comment is so Loooooooooooooooooooooooooooong."; // TOO_LONG
            tests[2] = "Very negative comment !!!! = (!!!!;"; // NEGATIVE_TEXT
            tests[3] = "Very BAAAAAAAAAAAAAAAAAAAAAAAAD comment with: |;"; // NEGATIVE_TEXT or TOO_LONG
            tests[4] = "This comment is so bad ...."; // SPAM
            tests[5] = "The comment is a spam, maybeeeeeeeeeeeeeeeeeeeeee!"; // SPAM or TOO_LONG
            tests[6] = "Negative bad :( spam."; // SPAM or NEGATIVE_TEXT
            tests[7] = "Very bad, very neg = (, very .................."; // SPAM or NEGATIVE_TEXT or TOO_LONG
            ITextAnalyzer[][] textAnalyzers = { textAnalyzers1, textAnalyzers2, textAnalyzers3, textAnalyzers4, textAnalyzers5, textAnalyzers6 };
            int numberOfAnalyzer; // номер аналізатора, зазначений в ідентифікатор textAnalyzers {№}
            int numberOfTest = 0; // номер тесту, який відповідає індексу тестових коментарів
            foreach (String test in tests)
            {
                numberOfAnalyzer = 1;
                Console.Write("test #" + numberOfTest + ":");
                Console.WriteLine(test);
                foreach (ITextAnalyzer[] analyzers in textAnalyzers)
                {
                    Console.Write(numberOfAnalyzer + ":");
                    Console.WriteLine(CheckLabels(analyzers, test));
                    numberOfAnalyzer++;
                }
                numberOfTest++;
            }
        }
    }
}