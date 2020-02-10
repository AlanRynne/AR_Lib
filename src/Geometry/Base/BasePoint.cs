using System;

#pragma warning disable 1591

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Abstract class representing a generic vector entity. All vector related entities must inherit from it.
    /// </summary>
    public abstract class BasePoint
    {
        // Public properties

        /// <summary>
        /// Gets or sets x Coordinate.
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }

            set
            {
                if (isUnset)
                    isUnset = false;
                x = Math.Round(value, Settings.MaxDecimals);
            }
        }

        /// <summary>
        /// Gets or sets y Coordinate.
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                if (isUnset)
                    isUnset = false;
                y = Math.Round(value, Settings.MaxDecimals);
            }
        }

        /// <summary>
        /// Gets or sets z Coordinate.
        /// </summary>
        public double Z
        {
            get
            {
                return z;
            }

            set
            {
                if (isUnset)
                    isUnset = false;
                z = Math.Round(value, Settings.MaxDecimals);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current point is unset.
        /// </summary>
        /// <value>True if Unset.</value>
        public bool IsUnset { get => isUnset; }

        // Private parameters
        protected double x;
        protected double y;
        protected double z;
        protected bool isUnset;

        // Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePoint"/> class.
        /// </summary>
        /// <returns>New origin point.</returns>
        protected BasePoint()
            : this(0, 0, 0)
        {
            isUnset = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePoint"/> class.
        /// </summary>
        /// <param name="point">Point to copy coordinates from.</param>
        /// <returns></returns>
        protected BasePoint(BasePoint point)
            : this(point.X, point.Y, point.Z)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePoint"/> class by cartesian coordinates.
        /// </summary>
        /// <param name="xCoord">X coordinate.</param>
        /// <param name="yCoord">Y coordinate.</param>
        /// <param name="zCoord">Z coordinate.</param>
        protected BasePoint(double xCoord, double yCoord, double zCoord)
        {
            X = xCoord;
            Y = yCoord;
            Z = zCoord;
            isUnset = false;
        }

        // Mathematical operations

        /// <summary>
        /// Add a point to this point.
        /// </summary>
        /// <param name="point">Point to add.</param>
        public void Add(BasePoint point)
        {
            x += point.X;
            y += point.Y;
            z += point.Z;
            isUnset = false;
        }

        /// <summary>
        /// Substract a point from this one.
        /// </summary>
        /// <param name="point">Point to substract.</param>
        public void Substract(BasePoint point)
        {
            x -= point.X;
            y -= point.Y;
            z -= point.Z;
            isUnset = false;
        }

        /// <summary>
        /// Multiply this point by a number.
        /// </summary>
        /// <param name="scalar">Number to multiply by.</param>
        public void Multiply(double scalar)
        {
            x *= scalar;
            y *= scalar;
            z *= scalar;
        }

        /// <summary>
        /// Divide this point by a number.
        /// </summary>
        /// <param name="scalar">Number to divide by.</param>
        public void Divide(double scalar)
        {
            x /= scalar;
            y /= scalar;
            z /= scalar;
        }

        /// <summary>
        /// Negates this point.
        /// </summary>
        public void Negate()
        {
            x = -x;
            y = -y;
            z = -z;
        }

        /// <summary>
        /// Returns the string representation of this point.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "{ " + x + ", " + y + ", " + z + " }";

        /// <summary>
        /// Converts a point to an array of numbers.
        /// </summary>
        /// <returns>Array with cartesian coordinates of point.</returns>
        public double[] ToArray() => new double[] { x, y, z };

        // Override Methods

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is BasePoint))
                return false;
            var pt = (BasePoint)obj;
            return Math.Abs(X - pt.X) <= Settings.Tolerance
                && Math.Abs(Y - pt.Y) <= Settings.Tolerance
                && Math.Abs(Z - pt.Z) <= Settings.Tolerance;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;
                double tol = Settings.Tolerance * 2;
                double tX = (int)(X * (1 / tol)) * tol;
                double tY = (int)(Y * (1 / tol)) * tol;
                double tZ = (int)(Z * (1 / tol)) * tol;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ tX.GetHashCode();
                hash = (hash * hashingMultiplier) ^ tY.GetHashCode();
                hash = (hash * hashingMultiplier) ^ tZ.GetHashCode();
                return hash;
            }
        }
    }
}