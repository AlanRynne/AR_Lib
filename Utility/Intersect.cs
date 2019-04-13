using System;
using System.Collections;
using System.Collections.Generic;
using AR_Lib.Curve;
using AR_Lib.Geometry;
using AR_Lib.HalfEdgeMesh;
using AR_Lib.LinearAlgebra;

namespace AR_Lib
{
    //INFO: Error enums for all classes are in file IntersectErrors.cs

    public static partial class Intersect3D
    {
        public static ISLinePlane LinePlane(Line S, Plane Pn, out Point3d I)
        {
            Vector3d u = S.EndPoint - S.StartPoint;
            Vector3d w = S.StartPoint - Pn.Origin;

            double D = Vector3d.DotProduct(Pn.ZAxis, u);
            double N = -Vector3d.DotProduct(Pn.ZAxis, w);

            if (D <= 0.000001) // Segment is parallel to plane
            {
                if (N == 0) // Segment lies in plane
                {
                    I = null;
                    return ISLinePlane.OnPlane;
                }
                else
                {
                    I = null;
                    return ISLinePlane.NoIntersection;
                }
            }

            // They are not parallel
            // Compute intersect param
            double sI = N / D;
            if (sI < 0 || sI > 1)
            {
                I = null;
                return ISLinePlane.NoIntersection;
            }

            I = S.StartPoint + u * sI; // Compute segment intersection point
            return ISLinePlane.Point;
        }
        public static ISRayFacePerimeter RayFacePerimeter(Point3d RayOrigin, Vector3d RayDir, HE_Face Face, out Point3d result, out HE_HalfEdge halfEdge)
        {
            Vector3d faceNormal = HE_MeshGeometry.FaceNormal(Face);
            Vector3d biNormal = Vector3d.CrossProduct(RayDir, faceNormal);

            Plane perpPlane = new Plane(RayOrigin, RayDir, faceNormal, biNormal);

            List<HE_Vertex> vertices = Face.adjacentVertices();

            Point3d temp = new Point3d();

            Line line = new Line(vertices[0], vertices[1]);
            if (LinePlane(line, perpPlane, out temp) != ISLinePlane.Point) { result = null; halfEdge = null; return ISRayFacePerimeter.Point; } // No intersection found
            if (temp != RayOrigin && temp != null) { result = temp; halfEdge = null; return ISRayFacePerimeter.Point; } // Intersection found

            line = new Line(vertices[1], vertices[2]);
            if (LinePlane(line, perpPlane, out temp) != ISLinePlane.Point) { result = null; halfEdge = null; return ISRayFacePerimeter.NoIntersection; } // No intersection found
            if (temp != RayOrigin && temp != null) { result = temp; halfEdge = null; return ISRayFacePerimeter.Point; } // Intersection found

            line = new Line(vertices[2], vertices[0]);
            if (LinePlane(line, perpPlane, out temp) != ISLinePlane.Point) { result = null; halfEdge = null; return ISRayFacePerimeter.NoIntersection; }
            if (temp != RayOrigin && temp != null) { result = temp; halfEdge = null; return ISRayFacePerimeter.Point; } else { result = null; halfEdge = null; return ISRayFacePerimeter.Error; }
        }

        public static ISLineLine LineLine(Line lineA, Line lineB, out IRLineLine result)
        {

            Vector3d u = lineA.EndPoint - lineA.StartPoint;
            Vector3d v = lineB.EndPoint - lineB.StartPoint;
            Vector3d w = lineA.StartPoint - lineB.StartPoint;
            double a = u.Dot(u); // always >= 0
            double b = u.Dot(v);
            double c = v.Dot(v); // always >= 0
            double d = u.Dot(w);
            double e = v.Dot(w);
            double D = a * c - b * b; // always >= 0
            double sc, sN, sD = D; // sc = sN / sD, default sD = D >= 0
            double tc, tN, tD = D; // tc = tN / tD, default tD = D >= 0

            // compute the line parameters of the two closest points
            if (D < Settings.Tolerance)
            { // the lines are almost parallel
                sN = 0.0; // force using point P0 on segment S1
                sD = 1.0; // to prevent possible division by 0.0 later
                tN = e;
                tD = c;
            }
            else
            { // get the closest points on the infinite lines
                sN = (b * e - c * d);
                tN = (a * e - b * d);
                if (sN < 0.0)
                { // sc < 0 => the s=0 edge is visible
                    sN = 0.0;
                    tN = e;
                    tD = c;
                }
                else if (sN > sD)
                { // sc > 1  => the s=1 edge is visible
                    sN = sD;
                    tN = e + b;
                    tD = c;
                }
            }

            if (tN < 0.0)
            { // tc < 0 => the t=0 edge is visible
                tN = 0.0;
                // recompute sc for this edge
                if (-d < 0.0)
                    sN = 0.0;
                else if (-d > a)
                    sN = sD;
                else
                {
                    sN = -d;
                    sD = a;
                }
            }
            else if (tN > tD)
            { // tc > 1  => the t=1 edge is visible
                tN = tD;
                // recompute sc for this edge
                if ((-d + b) < 0.0)
                    sN = 0;
                else if ((-d + b) > a)
                    sN = sD;
                else
                {
                    sN = (-d + b);
                    sD = a;
                }
            }
            // finally do the division to get sc and tc
            sc = (Math.Abs(sN) < Settings.Tolerance ? 0.0 : sN / sD);
            tc = (Math.Abs(tN) < Settings.Tolerance ? 0.0 : tN / tD);

            // get the difference of the two closest points
            Vector3d dP = w + (sc * u) - (tc * v); // =  S1(sc) - S2(tc)

            result.Distance = dP.Length; // return the closest distance
            result.tA = sc;
            result.tB = tc;
            result.pointA = lineA.PointAt(sc);
            result.pointB = lineB.PointAt(tc);

            if (result.Distance <= Settings.Tolerance) return ISLineLine.Point;
            else if (result.Distance > Settings.Tolerance) return ISLineLine.NoIntersection;
            else return ISLineLine.Error;
        }
    }
}