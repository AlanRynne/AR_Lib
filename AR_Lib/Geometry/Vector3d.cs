using System;
using System.Collections.Generic;

namespace AR_Lib.Geometry
{
    public class Vector3d : BasePoint
    {
        #region Constructors

        /// <inheritDoc/>
        public Vector3d() : base() { }

        /// <summary>
        /// Constructs a new Vector instance with the same values as the provided vector.
        /// </summary>
        /// <param name="vector">Vector to copy values from.</param>
        public Vector3d(Vector3d vector) : base(vector) { }
        
        public Vector3d(Point3d point) : base(point) { }
        public Vector3d(double xCoord, double yCoord, double zCoord) : base(xCoord, yCoord, zCoord) { }

        #endregion


        #region Utility properties/methods

        // Computes the Euclidiean length squared of this vector
        public double LengthSquared => DotProduct(this, this);

        // Computes the Euclidean length of this vector
        public double Length => Math.Sqrt(LengthSquared);

        // Divides this vector by it's euclidean length
        public void Unitize()
        {
            double length = Length;
            X /= length;
            Y /= length;
            Z /= length;
        }

        // Returns a normalized copy of this vector
        public Vector3d Unit()
        {
            double length = Length;
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

        /// Computes the scalar product (dot product) of two given vectors
        /// Dot product = u1*v1 + u2*v2 + u3*v3
        public static double DotProduct(Vector3d u, Vector3d v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }

        /// Computes the vector product (cross product) of two given vectors
        /// Cross product = { u2 * v3 - u3 * v2; u3 * v1 - u1 * v3; u1 * v2 - u2 * v1 }
        public static Vector3d CrossProduct(Vector3d u, Vector3d v)
        {
            double x = u.Y * v.Z - u.Z * v.Y;
            double y = u.Z * v.X - u.X * v.Z;
            double z = u.X * v.Y - u.Y * v.X;

            return new Vector3d(x, y, z);
        }

        /// Computes the angle in Radians between two given vectors
        /// Angle = Arcosine of the CrossProduct of U&V divided with their multiplied lengths.
        public static double Angle(Vector3d u, Vector3d v)
        {
            return Math.Acos(Vector3d.DotProduct(u, v) / (u.Length * v.Length));
        }

        /// Represents a vector of unit length in the X direction.
        public static Vector3d UnitX => new Vector3d(1, 0, 0);

        /// Represents a vector of unit length in the Y direction.
        public static Vector3d UnitY => new Vector3d(0, 1, 0);

        /// Represents a vector of unit length in the Z direction.
        public static Vector3d UnitZ => new Vector3d(0, 0, 1);

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
