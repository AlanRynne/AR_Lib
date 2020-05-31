using System.Collections.Generic;
using Paramdigma.Core.Geometry;

namespace Paramdigma.Core.HalfEdgeMesh
{
    /// <summary>
    /// Represents a point on a mesh as it's face index and barycentric coordinatees.
    /// </summary>
    public class MeshPoint
    {
        /// <summary>
        /// Gets or sets the index of the face this point lies in.
        /// </summary>
        public int FaceIndex { get; set; }

        /// <summary>
        /// Gets or sets the U coordinate at the face.
        /// </summary>
        public double U { get; set; }

        /// <summary>
        /// Gets or sets the V coordinate at the face.
        /// </summary>
        public double V { get; set; }

        /// <summary>
        /// Gets or sets the W coordinate at the face.
        /// </summary>
        public double W { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshPoint"/> class.
        /// </summary>
        /// <param name="faceIndex">Face Index.</param>
        /// <param name="u">U coordinate.</param>
        /// <param name="v">V coordinate.</param>
        /// <param name="w">Z coordinate.</param>
        public MeshPoint(int faceIndex, double u, double v, double w)
        {
            FaceIndex = faceIndex;
            U = u;
            V = v;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshPoint"/> class.
        /// </summary>
        /// <param name="point">3D Point.</param>
        /// <param name="face">Mesh face.</param>
        public MeshPoint(Point3d point, MeshFace face)
        {
            List<MeshVertex> adj = face.AdjacentVertices();
            double[] bary = Convert.Point3dToBarycentric(point, adj[0], adj[1], adj[2]);
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