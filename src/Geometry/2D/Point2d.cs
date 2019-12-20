using System;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// 2-Dimensional point
    /// </summary>
    public class Point2d
    {
        public double X;
        public double Y;

        public Point2d Origin => new Point2d(0, 0);

        /// <summary>
        /// Constructs an empty 2D point.
        /// </summary>
        public Point2d() : this(0, 0) { }

        /// <summary>
        /// Constructs a new 2D point out of x and y coordinates
        /// </summary>
        /// <param name="x">X coordinate of the point</param>
        /// <param name="y">Y coordinate of the point</param>
        public Point2d(double x, double y) 
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a new 2D point out of an existing point
        /// </summary>
        /// <param name="pt">A 2D point</param>
        public Point2d(Point2d pt): this(pt.X,pt.Y) { }

        // Overrided methods

        /// <summary>
        /// String representation of a 2-dimensional point instance
        /// </summary>
        /// <returns>Returns string representation of this Point2d instance.</returns>
        public override string ToString()
        {
            return "Point2d{ " + this.X + "; " + this.Y + "}";
        }

        /// <summary>
        /// Compares a Point2d instance to the given objects.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>Returns true if object is equals, false if not.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Point2d)
            {
                Point2d pt = obj as Point2d;
                return this.X == pt.X && this.Y == pt.Y ? true : false;
            }
            return false;
        }
        /// <summary>
        /// Gets the hash code for the corresponding Point2d instance.
        /// </summary>
        /// <returns>Returns an int representing the Point2d hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, X) ? X.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Y) ? Y.GetHashCode() : 0);
                return hash;
            }
        }

        #region Operators

        public static Vector2d operator +(Point2d point, Point2d point2) => new Vector2d(point.X + point2.X, point.Y + point2.Y);
        public static Vector2d operator -(Point2d point, Point2d point2) => new Vector2d(point.X - point2.X, point.Y - point2.Y);

        public static Point2d operator -(Point2d point) => new Point2d(-point.X, -point.Y);

        public static Point2d operator *(Point2d point, double scalar) => new Point2d(point.X * scalar, point.Y * scalar);
        public static Point2d operator *(double scalar, Point2d point) => new Point2d(point.X * scalar, point.Y * scalar);

        public static Point2d operator /(Point2d point, double scalar) => new Point2d(point.X / scalar, point.Y / scalar);
        public static Point2d operator /(double scalar, Point2d point) => new Point2d(point.X / scalar, point.Y / scalar);

        public static bool operator ==(Point2d point, Point2d point2) => point.Equals(point2);
        public static bool operator !=(Point2d point, Point2d point2) => !point.Equals(point2);

        public static Point2d operator +(Point2d point, Vector2d v) => new Point2d(point.X + v.X, point.Y + v.Y);

        // Implicit conversions
        public static implicit operator Point2d(Vector2d v) => v;
        public static explicit operator Vector2d(Point2d pt) => new Vector2d(pt);

        #endregion

    }

}
