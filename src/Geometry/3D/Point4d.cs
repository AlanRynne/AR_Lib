using System;
using System.Collections;
using System.Collections.Generic;
using AR_Lib.Geometry;
using AR_Lib.LinearAlgebra;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents a 4-dimensional point.
    /// </summary>
    public class Point4d : BasePoint
    {
        private double weight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point4d"/> class.
        /// </summary>
        /// <returns>Point with all values to zero.</returns>
        public Point4d()
            : base()
        {
            weight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point4d"/> class by cartesian coordinates and weight.
        /// </summary>
        /// <param name="x">X Coordinate.</param>
        /// <param name="y">Y Coordinate.</param>
        /// <param name="z">Z Coordinate.</param>
        /// <param name="w">Weight.</param>
        /// <returns>New 4-dimensional point with the specified values.</returns>
        public Point4d(double x, double y, double z, double w)
            : base(x, y, z)
        {
            weight = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point4d"/> class from a 3-dimensional point and a weight.
        /// </summary>
        /// <param name="pt">Point.</param>
        /// <param name="w">Weight.</param>
        /// <returns>New 4-dimensional point with the specified values.</returns>
        public Point4d(Point3d pt, double w)
            : base(pt)
        {
            weight = w;
        }

        /// <summary>
        /// Gets or sets the weight of this point.
        /// </summary>
        public double Weight
        {
            get => weight; set
            {
                weight = value;
                if (isUnset)
                    isUnset = false;
            }
        }

        /// <inheritdoc/>
        public static Point4d operator +(Point4d point, Point4d point2) => new Point4d(point.X + point2.X, point.Y + point2.Y, point.Z + point2.Z, point.Weight + point2.Weight);

        /// <inheritdoc/>
        public static Point4d operator -(Point4d point, Point4d point2) => new Point4d(point.X - point2.X, point.Y - point2.Y, point.Z - point2.Z, point.Weight - point2.Weight);

        /// <inheritdoc/>
        public static Point4d operator -(Point4d point) => new Point4d(-point.X, -point.Y, -point.Z, point.Weight);

        /// <inheritdoc/>
        public static Point4d operator *(Point4d point, double scalar) => new Point4d(point.X * scalar, point.Y * scalar, point.Z * scalar, point.Weight * scalar);

        /// <inheritdoc/>
        public static Point4d operator *(double scalar, Point4d point) => new Point4d(point.X * scalar, point.Y * scalar, point.Z * scalar, point.Weight * scalar);

        /// <inheritdoc />
        public static Point4d operator /(Point4d point, double scalar) => new Point4d(point.X / scalar, point.Y / scalar, point.Z / scalar, point.Weight / scalar);

        /// <inheritdoc/>
        public static Point4d operator /(double scalar, Point4d point) => new Point4d(point.X / scalar, point.Y / scalar, point.Z / scalar, point.Weight / scalar);

        /// <inheritdoc />
        public static bool operator ==(Point4d point, Point4d point2) => point.Equals(point2);

        /// <inheritdoc/>
        public static bool operator !=(Point4d point, Point4d point2) => !point.Equals(point2);

        /// <inheritdoc/>
        public static Point4d operator +(Point4d point, Vector3d v) => new Point4d(point.X + v.X, point.Y + v.Y, point.Z + v.Z, point.Weight);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Point4d pt)
            {
                return base.Equals(obj) && this.Weight == pt.Weight;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            // TODO: Non consistent getHashCode implementation
            return base.GetHashCode() ^ weight.GetHashCode();
        }

        // TODO: Add hasWeightedCoordinates boolean and implement a weightCoordinates() method
    }
}
