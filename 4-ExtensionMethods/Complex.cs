using System;

namespace ExtensionMethods
{
    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double _real;

        private readonly double _imaginary;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="real">the real part.</param>
        /// <param name="imaginary">the imaginary part.</param>
        public Complex(double real, double imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => _real;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => _imaginary;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(_real * _real + _imaginary * _imaginary);

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(_imaginary, _real);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString() => $"{_real} {(_imaginary >= 0 ? "+" : "-")}i{Math.Abs(_imaginary)}";

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Math.Abs(Real - other.Real) < Tolerance && Math.Abs(Imaginary - other.Imaginary) < Tolerance;
        }

        private const double Tolerance = 1E-7;

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj is IComplex complex)
            {
                return Equals(complex);
            }

            return false;
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return HashCode.Combine(_real, _imaginary);
        }
    }
}
