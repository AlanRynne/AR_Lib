using System.Collections.Generic;

using AR_Lib.Geometry;

#pragma warning disable 1591

namespace AR_Lib.IO
{
    #region To/From Files

    /// <summary>
    /// Struct containing the resulting mesh data extracted from an .OFF file.
    /// </summary>
    public struct OFFMeshData
    {
        public List<Point3d> vertices;
        public List<List<int>> faces;
    }

    #endregion
}