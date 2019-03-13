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
    }
}