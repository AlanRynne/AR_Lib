using System;
using System.Collections.Generic;
using AR_Lib;
using AR_Lib.Geometry;
using AR_Lib.HalfEdgeMesh;

namespace AR_Lib.Curves
{
    /// <summary>
    /// Static class to compute geodeesics on triangular meshes.
    /// </summary>
    public static class Geodesics
    {
        /// <summary>
        /// Computes a geodesic on a mesh given a starting point and an initial direction.
        /// Returns true if successfull and false if something went wrong.
        /// </summary>
        /// <param name="meshPoint">Point.</param>
        /// <param name="vector">Direction.</param>
        /// <param name="mesh">Mesh.</param>
        /// <param name="maxIter">Maximum iterations.</param>
        /// <param name="geodesic">Geodesic curves.</param>
        /// <returns>True if successful.</returns>
        public static bool StartDir(MeshPoint meshPoint, Vector3d vector, Mesh mesh, int maxIter, out List<Point3d> geodesic)
        {
            // Get initial face on the mesh
            MeshFace initialFace = mesh.Faces[meshPoint.FaceIndex];

            // Start iteration
            // Create variables for current iteration step
            MeshFace thisFace = initialFace;
            Point3d thisPoint = new Point3d();
            Vector3d thisDirection = vector;

            int iter = 0;
            List<Point3d> geodPoints = new List<Point3d>();
            do
            {
                Ray ray = new Ray(thisPoint, thisDirection);

                // Find intersection between ray and boundary
                Intersect3D.RayFacePerimeter(ray, thisFace, out Point3d nextPoint, out MeshHalfEdge halfEdge);

                // Intersection method should check for correct direction using sign of dot product

                // Add point to pointlist
                geodPoints.Add(nextPoint);

                // Walk to next face
                MeshFace nextFace = halfEdge.Twin.Face;

                // Flip vector to next face
                Vector3d perpVector = Vector3d.CrossProduct(thisDirection, MeshGeometry.FaceNormal(thisFace));
                Vector3d nextVector = Vector3d.CrossProduct(MeshGeometry.FaceNormal(nextFace), perpVector);

                // Assign iteration variables to current
                thisPoint = nextPoint;
                thisFace = nextFace;
                thisDirection = nextVector;

                // Increase counter
                iter++;
            }
            while (iter < maxIter);

            // Assign outputs
            geodesic = geodPoints;
            return true;
        }
    }
}