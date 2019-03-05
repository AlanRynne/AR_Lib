using System;
using System.Collections.Generic;

namespace AR_Lib.HalfEdgeMesh
{
    public class HE_MeshTopology
    {
        // Per Vertex adjacency index lists
        // Returns 2 dimensional array: 1 array per vertex index containing an array with the corresponding adjacent member index
        private HE_Mesh mesh;
        public HE_MeshTopology(HE_Mesh _mesh){
            mesh = _mesh;
            VertexVertex = new Dictionary<int, List<int>>();
            VertexFaces = new Dictionary<int, List<int>>();
            VertexEdges = new Dictionary<int, List<int>>();
        }

        public Dictionary<int,List<int>> VertexVertex;

        public Dictionary<int,List<int>> VertexFaces;
        
        public Dictionary<int,List<int>> VertexEdges;

        public Dictionary<int,List<int>> EdgeEdge;

        public Dictionary<int,List<int>> FaceFace;

        public void computeVertexAdjacency()
        {
            foreach (HE_Vertex vertex in mesh.Vertices)
            {
                foreach (HE_Vertex adjacent in vertex.adjacentVertices()){
                    if(!VertexVertex.ContainsKey(vertex.Index))
                    {
                        VertexVertex.Add(vertex.Index,new List<int>(){adjacent.Index});
                    } else {
                        VertexVertex[vertex.Index].Add(adjacent.Index);
                    }
                }
                foreach (HE_Face adjacent in vertex.adjacentFaces()){
                    if(!VertexFaces.ContainsKey(vertex.Index))
                    {
                        VertexFaces.Add(vertex.Index,new List<int>(){adjacent.Index});
                    } else {
                        VertexFaces[vertex.Index].Add(adjacent.Index);
                    }
                }
                foreach (HE_Edge adjacent in vertex.adjacentEdges()){
                    if(!VertexEdges.ContainsKey(vertex.Index))
                    {
                        VertexEdges.Add(vertex.Index,new List<int>(){adjacent.Index});
                    } else {
                        VertexEdges[vertex.Index].Add(adjacent.Index);
                    }
                }
            }
        }
    }
}
