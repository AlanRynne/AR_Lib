using System;
using System.Collections;
using System.Collections.Generic;

using AR_Lib.HalfEdgeMesh;
using AR_Lib.Geometry;
using AR_Lib.LinearAlgebra;
using AR_Lib.Curve;


namespace AR_Lib
{
    public static class Intersect3D
    {

        // Line Plane 3D intersection
        //    Input:  S = a segment, and Pn = a plane = {Point V0;  Vector n;}
        //    Output: *I0 = the intersect point (when it exists)
        //    Return: 0 = disjoint (no intersection)
        //            1 =  intersection in the unique point *I0
        //            2 = the  segment lies in the plane
        public static int LinePlane(Line S, Plane Pn, out Point3d I)
        {
            Vector3d u = S.endPoint - S.startPoint;
            Vector3d w = S.startPoint - Pn.Origin;

            double D = Vector3d.DotProduct(Pn.ZAxis,u);
            double N = -Vector3d.DotProduct(Pn.ZAxis,w);

            if (D <= 0.000001) // Segment is parallel to plane
            {
                if(N == 0) // Segment lies in plane
                {
                    I = null;
                    return 2;
                }
                else
                {
                    I = null;
                    return 0;  // No intersection
                }
            }

            // They are not parallel
            // Compute intersect param
            double sI = N / D;
            if (sI < 0 || sI > 1)
            {
                I = null;
                return 0; // No intersection
            }

            I = S.startPoint + u * sI; // Compute segment intersection point
            return 1;
        }
    
        public static int RayFacePerimeter(Point3d RayOrigin, Vector3d RayDir, HE_Face Face, out Point3d result, out HE_HalfEdge halfEdge)
        {
            Vector3d faceNormal = HE_MeshGeometry.FaceNormal(Face);
            Vector3d biNormal = Vector3d.CrossProduct(RayDir,faceNormal);

            Plane perpPlane = new Plane(RayOrigin, RayDir, faceNormal, biNormal);

            List<HE_Vertex> vertices = Face.adjacentVertices();

            Point3d temp = new Point3d();

            Line line = new Line(vertices[0], vertices[1]);
            if(LinePlane(line, perpPlane, out temp) != 1) { result = null; halfEdge = null; return 0; } // No intersection found
            if(temp != RayOrigin && temp != null){ result = temp; halfEdge = null; return 1; } // Intersection found

            line = new Line(vertices[1], vertices[2]);
            if(LinePlane(line, perpPlane, out temp) != 1) { result = null; halfEdge = null; return 0; } // No intersection found
            if(temp != RayOrigin && temp != null){ result = temp; halfEdge = null; return 1; } // Intersection found

            line = new Line(vertices[2], vertices[0]);
            if(LinePlane(line, perpPlane, out temp) != 1) { result = null; halfEdge = null; return 0; } // No intersection found
            if(temp != RayOrigin && temp != null){ result = temp; halfEdge = null; return 1; } // Intersection found

            else{ result = null; halfEdge = null; return 4; } // Error 4 means something weird happened!
        }
    }
}