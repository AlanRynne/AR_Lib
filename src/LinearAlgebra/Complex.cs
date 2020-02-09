using System;

namespace AR_Lib.LinearAlgebra
{
    // Class representing a complex number
    public class Complex
    {
        // Public fields
        public double Real { get; set; }
        public double Imaginary { get; set; }

        // Constructor
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // Methods

        public double Arg() => Math.Atan2(Imaginary, Real);

        public double Norm() => Math.Sqrt(Norm2());

        public double Norm2() => Real * Real + Imaginary * Imaginary;

        public Complex Conjugate() => new Complex(Real, -Imaginary);

        public Complex Inverse()
        {
            return Conjugate().OverReal(Norm2());
        }

        public Complex Polar()
        {
            double a = Norm();
            double theta = Arg();

            return new Complex(Math.Cos(theta) * a, Math.Sin(theta) * a);
        }

        public Complex Exp()
        {
            double a = Math.Exp(Real);
            double theta = Imaginary;
            return new Complex(Math.Cos(theta) * a, Math.Sin(theta) * a);
        }


        // Private methods for operators
        Complex Plus(Complex v) => new Complex(Real + v.Real, Imaginary + v.Imaginary);

        Complex Minus(Complex v) => new Complex(Real - v.Real, Imaginary - v.Imaginary);

        Complex TimesReal(double s) => new Complex(Real * s, Imaginary * s);

        Complex OverReal(double s) => TimesReal(1 / s);

        Complex TimesComplex(Complex v)
        {
            double a = Real;
            double b = Imaginary;
            double c = v.Real;
            double d = v.Imaginary;

            double reNew = a * c - b * d;
            double imNew = a * d - b * c;

            return new Complex(reNew, imNew);
        }

        Complex OverComplex(Complex v) => TimesComplex(v.Inverse());

        // Operators

        public static Complex operator +(Complex v, Complex w) => v.Plus(w);
        public static Complex operator -(Complex v, Complex w) => v.Minus(w);

        public static Complex operator *(Complex v, double s) => v.TimesReal(s);
        public static Complex operator *(double s, Complex v) => v.TimesReal(s);
        public static Complex operator *(Complex v, Complex w) => v.TimesComplex(w);

        public static Complex operator /(Complex v, double s) => v.OverReal(s);
        public static Complex operator /(double s, Complex v) => v.OverReal(s);
        public static Complex operator /(Complex v, Complex w) => v.OverComplex(w);

    }
}
