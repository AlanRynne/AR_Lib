using System.Collections.Generic;

using AR_Lib.Geometry;

#pragma warning disable 1591

namespace AR_Lib.IO
{
    /// <summary>
    /// Struct containing the resulting mesh data extracted from an .OFF file.
    /// </summary>
    public struct OFFMeshData
    {
        /// <summary>
        /// Mesh vertices.
        /// </summary>
        public List<Point3d> Vertices;

        /// <summary>
        /// Mesh face indices.
        /// </summary>
        public List<List<int>> Faces;
    }
}