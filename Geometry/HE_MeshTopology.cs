using System;
namespace AR_Lib.Geometry
{
    public class HE_MeshTopology
    {
        // Per Vertex adjacency index lists
        // Returns 2 dimensional array: 1 array per vertex index containing an array with the corresponding adjacent member index
        public int[,] VertexVertex() { throw new NotImplementedException(); }
        public int[,] VertexFaces() { throw new NotImplementedException(); }
        public int[,] VertexEdges() { throw new NotImplementedException(); }

        // Per Edge adjacency index lists
        public int[,] EdgeEdge() { throw new NotImplementedException(); }

        // Per Face adjacency index lists
        public int[,] FaceFace() { throw new NotImplementedException(); }
    }
}
