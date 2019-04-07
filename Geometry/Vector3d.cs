using System;
using System.Collections.Generic;

namespace AR_Lib.Geometry
{
    public class Vector3d : BasePoint
    {
        #region Constructors
        public Vector3d() : base() { }
        public Vector3d(Vector3d vector) : base(vector) { }
        public Vector3d(Point3d point) : base(point) { }
        public Vector3d(double xCoord, double yCoord, double zCoord) : base(xCoord, yCoord, zCoord) { }

        #endregion


        #region Operators

        public static Vector3d operator +(Vector3d v, Vector3d point2) => new Vector3d(v.X + point2.X, v.Y + point2.Y, v.Z + point2.Z);
        public static Vector3d operator -(Vector3d v, Vector3d point2) => new Vector3d(v.X - point2.X, v.Y - point2.Y, v.Z - point2.Z);
        public static Vector3d operator *(Vector3d v, double scalar) => new Vector3d(v.X * scalar, v.Y * scalar, v.Z * scalar);
        public static Vector3d operator *(double scalar, Vector3d v) => new Vector3d(v.X * scalar, v.Y * scalar, v.Z * scalar);
        public static Vector3d operator -(Vector3d v) => new Vector3d(-v.X, -v.Y, -v.Z);
        public static Vector3d operator /(Vector3d v, double scalar) => new Vector3d(v.X / scalar, v.Y / scalar, v.Z / scalar);
        public static bool operator ==(Vector3d v, Vector3d w) => v.Equals(w);
        public static bool operator !=(Vector3d v, Vector3d w) => !v.Equals(w);

        #endregion


        #region Utility properties/methods

        // Computes the Euclidiean length squared of this vector
        public double Norm2 => DotProduct(this, this);
        // Computes the Euclidean length of this vector
        public double Norm => Math.Sqrt(Norm2);

        // Divides this vector by it's euclidean length
        public void Normalize()
        {
            double length = Norm;
            X /= length;
            Y /= length;
            Z /= length;
        }

        // Returns a normalized copy of this vector
        public Vector3d Unit()
        {
            double length = Norm;
            double x = X / length;
            double y = Y / length;
            double z = Z / length;
            return new Vector3d(x, y, z);
        }

        // Returns the dot product of this vector and v
        public double Dot(Vector3d v) => Vector3d.DotProduct(this, v);
        // Returns the cross product of this vector and v
        public Vector3d Cross(Vector3d v) => Vector3d.CrossProduct(this, v);

        #endregion


        #region Static Methods
        public static double DotProduct(Vector3d u, Vector3d v)
        {
            // Dot product = u1*v1 + u2*v2 + u3*v3
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }
        public static Vector3d CrossProduct(Vector3d u, Vector3d v)
        {
            double x = u.Y * v.Z - u.Z * v.Y;
            double y = u.Z * v.X - u.X * v.Z;
            double z = u.X * v.Y - u.Y * v.X;

            return new Vector3d(x, y, z);
        }
        public static double Angle(Vector3d u, Vector3d v)
        {
            // Angle = Arcosine of the CrossProduct of U & V divided with their multiplied lengths.
            return Math.Acos(Vector3d.DotProduct(u, v) / (u.Norm * v.Norm));
        }

        // Global unit vectors
        public static Vector3d WorldX => new Vector3d(1, 0, 0);
        public static Vector3d WorldY => new Vector3d(0, 1, 0);
        public static Vector3d WorldZ => new Vector3d(0, 0, 1);

        #endregion


        #region Overridden methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return "Vector3d" + base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion


    }
}
