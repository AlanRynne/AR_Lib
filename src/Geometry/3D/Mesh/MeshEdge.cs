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
        public class MeshEdge
        {
            /// <summary>
            /// The half-edge linked to this edge.
            /// </summary>
            public MeshHalfEdge HalfEdge;
            /// <summary>
            /// The index of this Mesh Edge.
            /// </summary>
            public int Index;

            /// <summary>
            /// Constructs a new empty Mesh Edge
            /// </summary>
            public MeshEdge()
            {
                Index = -1;
            }

            /// <summary>
            /// Checks if the mesh edge lies on a boundary.
            /// </summary>
            public bool OnBoundary => (this.HalfEdge.onBoundary || this.HalfEdge.Twin.onBoundary);

            /// <summary>
            /// Gets the adjacent vertices of this given edge.
            /// </summary>
            /// <returns></returns>
            public List<MeshVertex> AdjacentVertices()
            {
                List<MeshVertex> vertices = new List<MeshVertex>
                {
                    this.HalfEdge.Vertex,
                    this.HalfEdge.Twin.Vertex
                };
                return vertices;
            }

            /// <summary>
            /// Gets the adjacent faces of this edge.
            /// </summary>
            public List<MeshFace> AdjacentFaces()
            {
                List<MeshFace> faces = new List<MeshFace>
                {
                    this.HalfEdge.AdjacentFace,
                    this.HalfEdge.Twin.AdjacentFace
                };
                return faces;
            }

            /// <summary>
            /// Gets the adjacent edges of this edge.
            /// </summary>
            public List<MeshEdge> AdjacentEdges()
            {
                List<MeshEdge> edges = new List<MeshEdge>();
                edges.AddRange(this.HalfEdge.Vertex.AdjacentEdges());
                edges.AddRange(this.HalfEdge.Twin.Vertex.AdjacentEdges());
                return edges;
            }
        }
    }
}
