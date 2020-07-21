using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    /// <summary>
    /// Розробити клас Rational - дріб. 
    /// містить Numerator -чисельник, Denominator - знаменник
    /// Клас має проходити всі тести в проекті.
    /// </summary>
    public class Rational
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        private void Cancellation()
        {
            if (Numerator != 0)
            {
                int m = Math.Abs(Denominator),
                    n = Math.Abs(Numerator),
                    ost = m % n;
                while (ost != 0)
                {
                    m = n; n = ost;
                    ost = m % n;
                }
                int nod = n;
                if (nod != 1)
                {
                    Numerator /= nod; Denominator /= nod;
                }
            }
        } // сокращение дроби
        public Rational(int n, int d)
        {
            Numerator = (d < 0 && n > 0 || d < 0 && n < 0) ? -n : n;
            Denominator = (n == 0) ? 1 : Math.Abs(d);
            Cancellation();
        }
        public Rational(int n) : this(n, 1) { }
        public bool IsNan => Denominator == 0 ? true : false;
        public static Rational operator +(Rational A, Rational B)
        {
            int numerator = A.Numerator * B.Denominator + B.Numerator * A.Denominator;
            int denominator = A.Denominator * B.Denominator;
            return new Rational(numerator, denominator);
        }
        public static Rational operator -(Rational A, Rational B)
        {
            int numerator = A.Numerator * B.Denominator - B.Numerator * A.Denominator;
            int denominator = A.Denominator * B.Denominator;
            return new Rational(numerator, denominator);
        }
        public static Rational operator *(Rational A, Rational B)
        {
            int numerator = A.Numerator * B.Numerator;
            int denominator = A.Denominator * B.Denominator;
            return new Rational(numerator, denominator);
        }
        public static Rational operator /(Rational A, Rational B)
        {
            int numerator = (B.Denominator == 0) ? 1 : A.Numerator * B.Denominator;
            int denominator = (B.Denominator == 0) ? 0 : A.Denominator * B.Numerator;
            return new Rational(numerator, denominator);
        }
        public static implicit operator double(Rational A)
        {
            return (A.Numerator + 0.0) / A.Denominator;
        }
        public static implicit operator Rational(int A)
        {
            return new Rational(A);
        }
    }
}
