using System;
using System.Collections;
using System.Collections.Generic;

using AR_Lib.HalfEdgeMesh;
using AR_Lib.Geometry;
using AR_Lib.LinearAlgebra;
using AR_Lib.Curve;


namespace AR_Lib
{
    public static class Convert
    {

        /// <summary>
        /// Compute barycentric coordinates (u, v, w) for
        /// point p with respect to triangle (a, b, c)
        /// </summary>
        /// <param name="p">Point to convert</param>
        /// <param name="a">First point of triangle</param>
        /// <param name="b">Second point of triangle</param>
        /// <param name="c">Third point of triangle</param>
        /// <returns></returns>
        public static double[] Point3dToBarycentric(Point3d p, Point3d a, Point3d b, Point3d c)
        {
            Vector3d v0 = b - a, v1 = c - a, v2 = p - a;

            double den = v0.X * v1.Y - v1.X * v0.Y;

            double v = (v2.X * v1.Y - v1.X * v2.Y) / den;
            double w = (v0.X * v2.Y - v2.X * v0.Y) / den;
            double u = 1.0 - v - w;

            double[] result = { u, v, w };

            return result;
        }

        /// <summary>
        /// Translate the coordinates of a given 3d point from the World XYZ into the relative coordinates of a given plane.
        /// </summary>
        /// <param name="point">The point to convert.</param>
        /// <param name="plane">The plane of reference to convert into.</param>
        /// <returns></returns>
        public static Point3d RelativeCoordinatesFromPlane(Point3d point, Plane plane)
        {
            Vector3d relativeVector = point - plane.Origin;

            double x = Vector3d.DotProduct(relativeVector,plane.XAxis);
            double y = Vector3d.DotProduct(relativeVector,plane.YAxis);
            double z = Vector3d.DotProduct(relativeVector,plane.ZAxis);

            return new Point3d(x,y,z);
        }

        
    }
}