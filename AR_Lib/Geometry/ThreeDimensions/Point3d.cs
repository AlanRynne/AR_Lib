using System;
using AR_Lib.HalfEdgeMesh;

namespace AR_Lib
{
    namespace Geometry
    {
        public class Point3d : BasePoint
        {
            #region Constructors
            public Point3d() : base() { }

            public Point3d(double xCoord, double yCoord, double zCoord) : base(xCoord, yCoord, zCoord) { }

            public Point3d(Point2d point) { throw new NotImplementedException(); }

            public Point3d(Point3d point) : base(point) { }

            public Point3d(Point4d point) : this(point.X / point.Weight, point.Y / point.Weight, point.Z / point.Weight) { }
            
            #endregion

            public static Point3d Unset => new Point3d();

            /// Returns a point with X,Y,Z = 0, and Unset = False
            public static Point3d WorldOrigin => new Point3d(0, 0, 0);

            /// Computes the euclidean distance between this point and the provided one.
            public double DistanceTo(Point3d point) => Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2) + Math.Pow(point.Z - Z, 2));

            #region Overriden methods 

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return "Point3d" + base.ToString();
            }

            #endregion

            #region Operators

            public static Vector3d operator +(Point3d point, Point3d point2) => new Vector3d(point.X + point2.X, point.Y + point2.Y, point.Z + point2.Z);
            public static Vector3d operator -(Point3d point, Point3d point2) => new Vector3d(point.X - point2.X, point.Y - point2.Y, point.Z - point2.Z);

            public static Point3d operator -(Point3d point) => new Point3d(-point.X, -point.Y, -point.Z);

            public static Point3d operator *(Point3d point, double scalar) => new Point3d(point.X * scalar, point.Y * scalar, point.Z * scalar);
            public static Point3d operator *(double scalar, Point3d point) => new Point3d(point.X * scalar, point.Y * scalar, point.Z * scalar);

            public static Point3d operator /(Point3d point, double scalar) => new Point3d(point.X / scalar, point.Y / scalar, point.Z / scalar);
            public static Point3d operator /(double scalar, Point3d point) => new Point3d(point.X / scalar, point.Y / scalar, point.Z / scalar);

            public static bool operator ==(Point3d point, Point3d point2) => point.Equals(point2);
            public static bool operator !=(Point3d point, Point3d point2) => !point.Equals(point2);

            public static Point3d operator +(Point3d point, Vector3d v) => new Point3d(point.X + v.X, point.Y + v.Y, point.Z + v.Z);

            // Implicit conversions
            public static implicit operator Point3d(Vector3d v) => v;
            public static explicit operator Vector3d(Point3d pt) => new Vector3d(pt);
            
            // Explicit conversions
            public static explicit operator Point3d(Point4d point) => new Point3d(point);
            
            #endregion
        }

    }
}
