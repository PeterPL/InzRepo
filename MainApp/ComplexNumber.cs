using System;

namespace MainApp
{
    /// <summary>
    /// Complex number.
    /// </summary>
    struct ComplexNumber
    {
        public double Real;
        public double Imaginary;

        public ComplexNumber(double real)
        {
            Real = real;
            Imaginary = 0;
        }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static ComplexNumber operator *(ComplexNumber number1, ComplexNumber number2)
        {
            return new ComplexNumber(number1.Real * number2.Real - number1.Imaginary * number2.Imaginary,
                number1.Imaginary * number2.Real + number1.Real * number2.Imaginary);
        }

        public static ComplexNumber operator +(ComplexNumber number1, ComplexNumber number2)
        {
            return new ComplexNumber(number1.Real + number2.Real, number1.Imaginary + number2.Imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber number1, ComplexNumber number2)
        {
            return new ComplexNumber(number1.Real - number2.Real, number1.Imaginary - number2.Imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber number)
        {
            return new ComplexNumber(-number.Real, -number.Imaginary);
        }

        public static implicit operator ComplexNumber(double number)
        {
            return new ComplexNumber(number, 0);
        }

        public ComplexNumber PoweredE()
        {
            var e = Math.Exp(Real);
            return new ComplexNumber(e * Math.Cos(Imaginary), e * Math.Sin(Imaginary));
        }

        public double Power2()
        {
            return Real * Real - Imaginary * Imaginary;
        }

        public double AbsPower2()
        {
            return Real * Real + Imaginary * Imaginary;
        }

        public override string ToString()
        {
            return String.Format("{0}+i*{1}", Real, Imaginary);
        }
    }
}