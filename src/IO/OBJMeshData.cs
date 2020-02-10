using System.Collections.Generic;

using AR_Lib.Geometry;

#pragma warning disable 1591

namespace AR_Lib.IO
{
    public struct OBJMeshData
    {
        public List<Point3d> Vertices;
        public List<List<int>> Faces;
        public List<List<int>> Edges;
        public List<List<double>> TextureCoords;
        public List<List<int>> FaceTextureCoords;
        public List<Vector3d> Normals;
    }
}