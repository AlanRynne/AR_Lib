using System;
using System.Collections;
using System.Collections.Generic;
using AR_Lib.Geometry;
using AR_Lib.LinearAlgebra;


namespace AR_Lib.Geometry
{
    public class Point4d : Point3d
    {
        public double Weight { get => weight; set { weight = value; if (isUnset) isUnset = false; } }

        private double weight;

        #region Constructors

        public Point4d() : base()
        {
            weight = 0;
        }

        public Point4d(double x, double y, double z, double w) : base(x, y, z)
        {
            weight = w;
        }

        public Point4d(Point3d pt, double w) : base(pt)
        {
            weight = w;
        }

        #endregion

        #region Operators

        public static Point4d operator +(Point4d point, Point4d point2) => new Point4d(point.X + point2.X, point.Y + point2.Y, point.Z + point2.Z, point.Weight + point2.Weight);
        public static Point4d operator -(Point4d point, Point4d point2) => new Point4d(point.X - point2.X, point.Y - point2.Y, point.Z - point2.Z, point.Weight - point2.Weight);

        public static Point4d operator -(Point4d point) => new Point4d(-point.X, -point.Y, -point.Z, point.Weight);

        public static Point4d operator *(Point4d point, double scalar) => new Point4d(point.X * scalar, point.Y * scalar, point.Z * scalar, point.Weight * scalar);
        public static Point4d operator *(double scalar, Point4d point) => new Point4d(point.X * scalar, point.Y * scalar, point.Z * scalar, point.Weight * scalar);

        public static Point4d operator /(Point4d point, double scalar) => new Point4d(point.X / scalar, point.Y / scalar, point.Z / scalar, point.Weight / scalar);
        public static Point4d operator /(double scalar, Point4d point) => new Point4d(point.X / scalar, point.Y / scalar, point.Z / scalar, point.Weight / scalar);

        public static bool operator ==(Point4d point, Point4d point2) => point.Equals(point2);
        public static bool operator !=(Point4d point, Point4d point2) => !point.Equals(point2);

        public static Point4d operator +(Point4d point, Vector3d v) => new Point4d(point.X + v.X, point.Y + v.Y, point.Z + v.Z, point.Weight);

        #endregion

        #region Overriden methods

        public override bool Equals(object obj)
        {
            if (obj is Point4d)
            {
                Point4d pt = (Point4d)obj;
                return base.Equals(obj) && this.Weight == pt.Weight;

            }
            else return false;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        //TODO: Add hasWeightedCoordinates boolean and implement a weightCoordinates() method

    }

}
