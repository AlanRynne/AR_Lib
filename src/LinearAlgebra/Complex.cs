using System;

namespace AR_Lib.LinearAlgebra
{
    /// <summary>
    /// Represents a complex number (a number with real + imaginary components).
    /// </summary>
    public class Complex
    {
        /// <summary>
        /// Gets or sets the Real component of the complex number.
        /// </summary>
        public double Real { get; set; }

        /// <summary>
        /// Gets or sets the Imaginary component of the complex number.
        /// </summary>
        public double Imaginary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="real">Real component.</param>
        /// <param name="imaginary">Imaginary component.</param>
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // Methods

        /// <summary>
        /// Computes the phase angle of this complex number.
        /// </summary>
        /// <returns></returns>
        public double Arg() => Math.Atan2(Imaginary, Real);

        /// <summary>
        /// Computes the length of the complex number.
        /// </summary>
        /// <returns></returns>
        public double Norm() => Math.Sqrt(Norm2());

        /// <summary>
        /// Computes the squared length of the complex number.
        /// </summary>
        /// <returns></returns>
        public double Norm2() => (Real * Real) + (Imaginary * Imaginary);

        /// <summary>
        /// Conjugates complex number (negates the imaginary component).
        /// </summary>
        /// <returns></returns>
        public Complex Conjugate() => new Complex(Real, -Imaginary);

        /// <summary>
        /// Computes the inverse of the complex number ((a + bi)^-1).
        /// </summary>
        /// <returns></returns>
        public Complex Inverse() => Conjugate().OverReal(Norm2());

        /// <summary>
        /// Computes the polar form ae^(iθ), where a is the norm and θ is the phase angle of this complex number.
        /// </summary>
        /// <returns></returns>
        public Complex Polar()
        {
            double a = Norm();
            double theta = Arg();

            return new Complex(Math.Cos(theta) * a, Math.Sin(theta) * a);
        }

        /// <summary>
        /// Exponentiates this complex number.
        /// </summary>
        /// <returns></returns>
        public Complex Exp()
        {
            double a = Math.Exp(Real);
            double theta = Imaginary;
            return new Complex(Math.Cos(theta) * a, Math.Sin(theta) * a);
        }

        // Private methods for operators
        private Complex Plus(Complex v) => new Complex(Real + v.Real, Imaginary + v.Imaginary);

        private Complex Minus(Complex v) => new Complex(Real - v.Real, Imaginary - v.Imaginary);

        private Complex TimesReal(double s) => new Complex(Real * s, Imaginary * s);

        private Complex OverReal(double s) => TimesReal(1 / s);

        private Complex TimesComplex(Complex v)
        {
            var a = Real;
            var b = Imaginary;
            var c = v.Real;
            var d = v.Imaginary;

            var reNew = (a * c) - (b * d);
            var imNew = (a * d) - (b * c);

            return new Complex(reNew, imNew);
        }

        private Complex OverComplex(Complex v) => TimesComplex(v.Inverse());

        // Operators

        /// <summary>
        /// Adds to complex numbers.
        /// </summary>
        /// <param name="v">First complex number.</param>
        /// <param name="w">Second complex number.</param>
        public static Complex operator +(Complex v, Complex w) => v.Plus(w);

        /// <summary>
        /// Substracts one complex number from another.
        /// </summary>
        /// <param name="v">First complex number.</param>
        /// <param name="w">Second complex number.</param>
        public static Complex operator -(Complex v, Complex w) => v.Minus(w);

        /// <summary>
        /// Multiplies a complex number with a number.
        /// </summary>
        /// <param name="v">Multiplicand.</param>
        /// <param name="s">Multiplier.</param>
        public static Complex operator *(Complex v, double s) => v.TimesReal(s);

        /// <summary>
        /// Multiplies a complex number with a number.
        /// </summary>
        /// <param name="s">Multiplier.</param>
        /// <param name="v">Multiplicand.</param>
        public static Complex operator *(double s, Complex v) => v.TimesReal(s);

        /// <summary>
        /// Multiplies to complex numbers.
        /// </summary>
        /// <param name="v">Multiplicand.</param>
        /// <param name="w">Multiplier.</param>
        public static Complex operator *(Complex v, Complex w) => v.TimesComplex(w);

        /// <summary>
        /// Divides a complex number by a number.
        /// </summary>
        /// <param name="v">Divisor.</param>
        /// <param name="s">Dividend.</param>
        public static Complex operator /(Complex v, double s) => v.OverReal(s);

        /// <summary>
        /// Divides two complex numbers.
        /// </summary>
        /// <param name="v">Divisor.</param>
        /// <param name="w">Dividend.</param>
        public static Complex operator /(Complex v, Complex w) => v.OverComplex(w);
    }
}
