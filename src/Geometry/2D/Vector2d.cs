using System;

namespace AR_Lib.Geometry
{
    public class Vector2d
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2d(Vector2d vector) : this(vector.X, vector.Y) { }
        public Vector2d(Point2d point) : this(point.X, point.Y) { }
        public Vector2d(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double LengthSquared => X * X + Y * Y;
        public double Length => Math.Sqrt(this.LengthSquared);

        public Vector2d Unit() => new Vector2d(this / this.Length);
        public void Unitize()
        {
            double length = this.Length;
            this.X /= length;
            this.Y /= length;
        }


        /// <summary>
        /// Returns a CCW perpendicular vector to the current instance.
        /// </summary>
        public Vector2d Perp() => new Vector2d(-Y, X);

        public double DotProduct(Vector2d vector) => X * X + Y * Y;
        public double PerpProduct(Vector2d vector) => X * vector.Y - Y * vector.X;


        #region Operators

        public static Vector2d operator +(Vector2d v, Vector2d point2) => new Vector2d(v.X + point2.X, v.Y + point2.Y);
        public static Vector2d operator -(Vector2d v, Vector2d point2) => new Vector2d(v.X - point2.X, v.Y - point2.Y);
        public static Vector2d operator *(Vector2d v, double scalar) => new Vector2d(v.X * scalar, v.Y * scalar);
        public static Vector2d operator *(double scalar, Vector2d v) => new Vector2d(v.X * scalar, v.Y * scalar);
        public static Vector2d operator -(Vector2d v) => new Vector2d(-v.X, -v.Y);
        public static Vector2d operator /(Vector2d v, double scalar) => new Vector2d(v.X / scalar, v.Y / scalar);
        public static bool operator ==(Vector2d v, Vector2d w) => v.Equals(w);
        public static bool operator !=(Vector2d v, Vector2d w) => !v.Equals(w);

        #endregion

        #region Overridden methods

        public override string ToString()
        {
            return @"Vector3d [{X}, {Y}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2d)
            {
                Vector2d vect = obj as Vector2d;
                return this.X == vect.X && this.Y == vect.Y ? true : false;
            }
            return false;
        }

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

        #endregion
    }
}