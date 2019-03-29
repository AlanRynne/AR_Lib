using System;
using System.Collections.Generic;
using AR_Lib;
using AR_Lib.Geometry;
using AR_Lib.Curve;
using AR_Lib.HalfEdgeMesh;

namespace AR_Lib.Curve
{
    public static class Geodesics
    {
        /// <summary>
        /// Computes a geodesic on a mesh given a starting point and an initial direction.
        /// Returns true if successfull and false if something went wrong.
        /// </summary>
        public static bool StartDir(HE_MeshPoint meshPoint, Vector3d vector, HE_Mesh mesh, int maxIter, out List<Point3d> geodesic)
        {
            // Get initial face on the mesh
            HE_Face initialFace = mesh.Faces[meshPoint.FaceIndex];
            // Start iteration

            // Create variables for current iteration step
            HE_Face thisFace = new HE_Face();
            Point3d thisPoint = new Point3d();
            Vector3d thisDirection = new Vector3d();

            int iter = 0;
            List<Point3d> geodPoints = new List<Point3d>();
            do 
            {
                // Find intersection between ray and boundary
                AR_Lib.Intersect3D.RayFacePerimeter(thisPoint,thisDirection,thisFace, out Point3d nextPoint, out HE_HalfEdge halfEdge);

                // Intersection method should check for correct direction using sign of dot product

                // Add point to pointlist
                geodPoints.Add(nextPoint);

                // Walk to next face
                HE_Face nextFace = halfEdge.Twin.Face;

                // Flip vector to next face
                Vector3d perpVector = Vector3d.CrossProduct(thisDirection, HE_MeshGeometry.FaceNormal(thisFace));
                Vector3d nextVector = Vector3d.CrossProduct(HE_MeshGeometry.FaceNormal(nextFace),perpVector);

                // Assign iteration variables to current
                thisPoint = nextPoint;
                thisFace = nextFace;
                thisDirection = nextVector;

                // Increase counter
                iter++;
            } while (iter < maxIter);

            // Assign outputs
            geodesic = geodPoints;
            return true;

        }
    }
}