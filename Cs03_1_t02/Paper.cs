using System;

namespace hw
{
    /*
        1.	+Разработать один из классов, в соответствии с полученным вариантом.
        2.	+Реализовать не менее пяти закрытых полей (различных типов), представляющих основные характеристики рассматриваемого класса.
        3.	+++Создать не менее трех методов управления классом 
            +и методы доступа к его закрытым полям. 
        4.	+Создать метод, в который передаются аргументы по ссылке. 
        5.	+-Создать не менее двух статических полей (различных типов),  представляющих общие характеристики объектов данного класса. 
        6.	+Обязательным требованием является реализация нескольких перегруженных конструкторов, аргументы которых определяются студентом, исходя из специфики реализуемого класса, 
            +а так же реализация конструктора по умолчанию.
        7.	+Создать статический конструктор.
        8.	Создать массив (не менее 5 элементов) объектов созданного класса.
        9.	Создать дополнительный метод для данного класса в другом файле, используя ключевое слово partial. 
    */

    partial class Paper
    {
        private double _weight;
        private double _height;
        private double _width;
        private string _internationalType;

        public static int _count;

        public Paper()
        {
            Weight = 80.0;
            DefineInternationalType("A4", out _internationalType);
            Type = "printing";
            Color = "white";

            _count++;
        }
        public Paper(double weight, double height, double width, string type = "printing", string color = "white")
        {
            Weight = weight;
            Height = height;
            Width = width;
            Type = type;
            Color = color;

            _count++;
        }
        public Paper(double weight, string internationalType, string type = "printing", string color = "white")
        {
            Weight = weight;
            InternationalType = internationalType;
            Type = type;
            Color = color;

            _count++;
        }
        
        static Paper()
        {
            _count = 0;
        }


        public double Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (value > 0)
                    _weight = value;
            }
        }
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value > 0)
                    _height = value;
            }
        }
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value > 0)
                    _width = value;
            }
        }
        public string Type { get; set; }
        public string InternationalType
        {
            get
            {
                return _internationalType;
            }
            set
            {
                try
                {
                    DefineInternationalType(value, out _internationalType);
                }
                catch (UndefinedTypeException)
                {
                    _internationalType = "Custom";
                }
            }
        }
        public string Color { get; set; }

        public void DefineInternationalType(string internationalType, out string type)
        {
            if (String.Compare(internationalType, "A2", true) == 0)
            {
                Height = 594.0;
                Width = 420.0;
                type = "A2";
            }
            else if (String.Compare(internationalType, "A3", true) == 0)
            {
                Height = 420.0;
                Width = 297.0;
                type = "A3";
            }
            else if (String.Compare(internationalType, "A4", true) == 0)
            {
                Height = 297.0;
                Width = 210.0;
                type = "A4";
            }
            else if (String.Compare(internationalType, "A5", true) == 0)
            {
                Height = 210.0;
                Width = 148.0;
                type = "A5";
            }
            else
            {
                throw (new UndefinedTypeException("Введенный тип не существует или находиться в разработке"));
            }
        }
    }
}
