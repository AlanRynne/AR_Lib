using System;
using System.Collections.Generic;
namespace AR_Lib
{
    namespace HalfEdgeMesh
    {
        /// <summary>
        /// Edge class representing a full edge of a half-edge mesh.
        /// A full edge contains 2 half-edges
        /// </summary>
        public class HE_Edge
        {
            public HE_HalfEdge HalfEdge;
            public int Index;

            public HE_Edge()
            {
                Index = -1;
            }

            public bool onBoundary => (this.HalfEdge.onBoundary || this.HalfEdge.Twin.onBoundary);

            public List<HE_Vertex> adjacentVertices(){
                List<HE_Vertex> vertices = new List<HE_Vertex>();
                vertices.Add(this.HalfEdge.Vertex);
                vertices.Add(this.HalfEdge.Twin.Vertex);
                return vertices;
            }
            public List<HE_Face> adjacentFaces(){
                List<HE_Face> faces = new List<HE_Face>();
                faces.Add(this.HalfEdge.AdjacentFace);
                faces.Add(this.HalfEdge.Twin.AdjacentFace);
                return faces;
            }
            public List<HE_Edge> adjacentEdges()
            {
                List<HE_Edge> edges = new List<HE_Edge>();
                edges.AddRange(this.HalfEdge.Vertex.adjacentEdges());
                edges.AddRange(this.HalfEdge.Twin.Vertex.adjacentEdges());
                return edges;
            }
        }
    }
}
