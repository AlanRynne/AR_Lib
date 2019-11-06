using System.Collections.Generic;
using System.Collections;
using AR_Lib.Geometry;

namespace AR_Lib.HalfEdgeMesh
{
    /// <summary>
    /// Represents a point on a mesh as it's face index and barycentric coordinatees.
    /// </summary>
    public class MeshPoint
    {
        public int FaceIndex;
        public double U;
        public double V;
        public double W;

        public MeshPoint(int faceIndex, double u, double v, double w)
        {
            FaceIndex = faceIndex;
            U = u;
            V = v;
            W = w;
        }

        public MeshPoint(Point3d point, MeshFace face)
        {
            List<MeshVertex> adj = face.AdjacentVertices();
            double[] bary = Convert.Point3dToBarycentric(point,adj[0],adj[1],adj[2]);
            U = bary[0];
            V = bary[1];
            W = bary[2];
        }

        /// <summary>
        /// Converts a mesh point into a string.
        /// </summary>
        /// <returns>String representation of the mesh point.</returns>
        public override string ToString()
        {
            return "MeshPoint{ " + FaceIndex + "; " + U + ", " + V + ", " + W + " }";
        }


    }
}
