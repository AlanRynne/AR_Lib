using System.Collections.Generic;

using AR_Lib.Geometry;

#pragma warning disable 1591

namespace AR_Lib.IO
{
    public struct OBJMeshData
    {
        public List<Point3d> Vertices { get; }

        public List<List<int>> Faces { get; }

        public List<List<int>> Edges { get; }

        public List<List<double>> TextureCoords { get; }

        public List<List<int>> FaceTextureCoords { get; }

        public List<Vector3d> Normals { get; }
    }
}