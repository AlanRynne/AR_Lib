﻿using System;
using System.Collections.Generic;

namespace AR_Lib.HalfEdgeMesh
{
    public class MeshTopology
    {
        // Per Vertex adjacency index lists
        // Returns 2 dimensional array: 1 array per vertex index containing an array with the corresponding adjacent member index
        private Mesh mesh;
        public MeshTopology(Mesh _mesh)
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

        public void ComputeVertexAdjacency()
        {
            foreach (MeshVertex vertex in mesh.Vertices)
            {
                foreach (MeshVertex adjacent in vertex.AdjacentVertices())
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
                foreach (MeshFace adjacent in vertex.AdjacentFaces())
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
                foreach (MeshEdge adjacent in vertex.AdjacentEdges())
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

        public void ComputeFaceAdjacency()
        {
            foreach (MeshFace face in mesh.Faces)
            {
                foreach (MeshVertex adjacent in face.AdjacentVertices())
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
                foreach (MeshFace adjacent in face.AdjacentFaces())
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
                foreach (MeshEdge adjacent in face.AdjacentEdges())
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

        public void ComputeEdgeAdjacency()
        {
            foreach (MeshEdge edge in mesh.Edges)
            {
                foreach (MeshVertex adjacent in edge.AdjacentVertices())
                {
                    if (!EdgeVertex.ContainsKey(edge.Index))
                        EdgeVertex.Add(edge.Index, new List<int>() { adjacent.Index });
                    else
                        EdgeVertex[edge.Index].Add(adjacent.Index);

                }
                foreach (MeshFace adjacent in edge.AdjacentFaces())
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
                foreach (MeshEdge adjacent in edge.AdjacentEdges())
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

            foreach (KeyValuePair<int, List<int>> pair in dict)
            {
                string tmpString = "Key: " + pair.Key.ToString() + " --> ";
                foreach (int i in pair.Value)
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
