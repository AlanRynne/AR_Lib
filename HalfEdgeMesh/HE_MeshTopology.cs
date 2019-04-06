using System;
using System.Collections.Generic;

namespace AR_Lib.HalfEdgeMesh
{
    public class HE_MeshTopology
    {
        // Per Vertex adjacency index lists
        // Returns 2 dimensional array: 1 array per vertex index containing an array with the corresponding adjacent member index
        private HE_Mesh mesh;
        public HE_MeshTopology(HE_Mesh _mesh)
        {
            mesh = _mesh;

            VertexVertex = new Dictionary<int, List<int>>();
            VertexFaces = new Dictionary<int, List<int>>();
            VertexEdges = new Dictionary<int, List<int>>();
            FaceVertex = new Dictionary<int, List<int>>();
            FaceFace = new Dictionary<int, List<int>>();
            FaceEdge = new Dictionary<int, List<int>>();
            EdgeVertex = new Dictionary<int, List<int>>();
            EdgeFace = new Dictionary<int, List<int>>();
            EdgeEdge = new Dictionary<int, List<int>>();
        }

        public Dictionary<int, List<int>> VertexVertex;
        public Dictionary<int, List<int>> VertexFaces;
        public Dictionary<int, List<int>> VertexEdges;

        public Dictionary<int, List<int>> EdgeEdge;
        public Dictionary<int, List<int>> EdgeVertex;
        public Dictionary<int, List<int>> EdgeFace;

        public Dictionary<int, List<int>> FaceVertex;
        public Dictionary<int, List<int>> FaceEdge;
        public Dictionary<int, List<int>> FaceFace;

        public void computeVertexAdjacency()
        {
            foreach (HE_Vertex vertex in mesh.Vertices)
            {
                foreach (HE_Vertex adjacent in vertex.adjacentVertices())
                {
                    if (!VertexVertex.ContainsKey(vertex.Index))
                    {
                        VertexVertex.Add(vertex.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        VertexVertex[vertex.Index].Add(adjacent.Index);
                    }
                }
                foreach (HE_Face adjacent in vertex.adjacentFaces())
                {
                    if (!VertexFaces.ContainsKey(vertex.Index))
                    {
                        VertexFaces.Add(vertex.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        VertexFaces[vertex.Index].Add(adjacent.Index);
                    }
                }
                foreach (HE_Edge adjacent in vertex.adjacentEdges())
                {
                    if (!VertexEdges.ContainsKey(vertex.Index))
                    {
                        VertexEdges.Add(vertex.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        VertexEdges[vertex.Index].Add(adjacent.Index);
                    }
                }
            }
        }

        public void computeFaceAdjacency()
        {
            foreach (HE_Face face in mesh.Faces)
            {
                foreach (HE_Vertex adjacent in face.adjacentVertices())
                {
                    if (!FaceVertex.ContainsKey(face.Index))
                    {
                        FaceVertex.Add(face.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        FaceVertex[face.Index].Add(adjacent.Index);
                    }
                }
                foreach (HE_Face adjacent in face.adjacentFaces())
                {
                    if (!FaceFace.ContainsKey(face.Index))
                    {
                        FaceFace.Add(face.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        FaceFace[face.Index].Add(adjacent.Index);
                    }
                }
                foreach (HE_Edge adjacent in face.adjacentEdges())
                {                    
                    if (!FaceEdge.ContainsKey(face.Index))
                    {
                        FaceEdge.Add(face.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        FaceEdge[face.Index].Add(adjacent.Index);
                    }
                }
            }
        }

        public void computeEdgeAdjacency()
        {
            foreach (HE_Edge edge in mesh.Edges)
            {
                foreach (HE_Vertex adjacent in edge.adjacentVertices())
                {
                    if (!EdgeVertex.ContainsKey(edge.Index)) EdgeVertex.Add(edge.Index, new List<int>() { adjacent.Index });
                    else EdgeVertex[edge.Index].Add(adjacent.Index);
                    
                }
                foreach (HE_Face adjacent in edge.adjacentFaces())
                {
                    if (!EdgeFace.ContainsKey(edge.Index))
                    {
                        EdgeFace.Add(edge.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        EdgeFace[edge.Index].Add(adjacent.Index);
                    }
                }
                foreach (HE_Edge adjacent in edge.adjacentEdges())
                {                    
                    if (!EdgeEdge.ContainsKey(edge.Index))
                    {
                        EdgeEdge.Add(edge.Index, new List<int>() { adjacent.Index });
                    }
                    else
                    {
                        EdgeEdge[edge.Index].Add(adjacent.Index);
                    }
                }
            }
        }

        public string TopologyDictToString(Dictionary<int, List<int>> dict)
        {
            string finalString = "";

            foreach(KeyValuePair<int, List<int>> pair in dict){
                string tmpString = "Key: " + pair.Key.ToString() + " --> ";
                foreach(int i in pair.Value)
                {
                    tmpString += i + " ";
                }
                tmpString += "\n";
                finalString += tmpString;
            }
            return finalString;
        }

    }


}
