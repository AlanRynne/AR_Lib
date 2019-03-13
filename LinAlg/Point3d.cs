using System;
using AR_Lib.HalfEdgeMesh;

namespace AR_Lib
{
    namespace Geometry
    {
        public class Point3d : BasePoint
        {
            //Public properties
            public bool isUnset;

            //Constructors
            public Point3d() : base() { }

            public Point3d(double xCoord, double yCoord, double zCoord) : base(xCoord, yCoord, zCoord)
            {
            }

            public Point3d(Point3d point) : base(point)
            {
            }

            public double DistanceTo(Point3d point)
            {
                return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2) + Math.Pow(point.Z - Z, 2));
            }

            // Operators
            public static Point3d operator +(Point3d point, Point3d point2) => new Point3d(point.X + point2.X, point.Y + point2.Y, point.Z + point2.Z);
            public static Point3d operator -(Point3d point, Point3d point2) => new Point3d(point.X - point2.X, point.Y - point2.Y, point.Z - point2.Z);

            public static Point3d operator -(Point3d point) => new Point3d(-point.X, -point.Y, -point.Z);

            public static Point3d operator *(Point3d point, double scalar) => new Point3d(point.X * scalar, point.Y * scalar, point.Z * scalar);
            public static Point3d operator *(double scalar, Point3d point) => new Point3d(point.X * scalar, point.Y * scalar, point.Z * scalar);

            public static Point3d operator /(Point3d point, double scalar) => new Point3d(point.X / scalar, point.Y / scalar, point.Z / scalar);
            public static Point3d operator /(double scalar, Point3d point) => new Point3d(point.X / scalar, point.Y / scalar, point.Z / scalar);

            public static bool operator ==(Point3d point, Point3d point2) => point.Equals(point2);
            public static bool operator !=(Point3d point, Point3d point2) => !point.Equals(point2);

            public static Point3d operator +(Point3d point, Vector3d v) => new Point3d(point.X+v.X,point.Y+v.Y,point.Z+v.Z);
            // Static methods

            public static Point3d Unset => new Point3d();
            public static Point3d WorldOrigin => new Point3d(0, 0, 0);

            // Override Methods
            public override bool Equals(object obj)
            {
                if (obj is Point3d)
                {
                    Point3d pt = (Point3d)obj;
                    if (Math.Abs(this.X - pt.X) < 0.000001 && Math.Abs(this.Y - pt.Y) < 0.000001 && Math.Abs(this.Z - pt.Z) < 0.000001) { return true; }
                    else { return false; }
                }
                else
                {
                    return false;
                }
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
                    hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Z) ? Z.GetHashCode() : 0);
                    return hash;
                }
            }

            public override string ToString()
            {
                return base.ToString();
            }


            // Implicit conversions
            public static implicit operator Point3d(Vector3d v) => v;
            
            public static implicit operator Vector3d(Point3d pt) => new Vector3d(pt);

        }

    }
}