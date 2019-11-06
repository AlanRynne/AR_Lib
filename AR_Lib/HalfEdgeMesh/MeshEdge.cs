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
            public MeshHalfEdge HalfEdge;
            public int Index;

            public MeshEdge()
            {
                Index = -1;
            }

            public bool OnBoundary => (this.HalfEdge.onBoundary || this.HalfEdge.Twin.onBoundary);

            public List<MeshVertex> AdjacentVertices(){
                List<MeshVertex> vertices = new List<MeshVertex>();
                vertices.Add(this.HalfEdge.Vertex);
                vertices.Add(this.HalfEdge.Twin.Vertex);
                return vertices;
            }
            public List<MeshFace> AdjacentFaces(){
                List<MeshFace> faces = new List<MeshFace>();
                faces.Add(this.HalfEdge.AdjacentFace);
                faces.Add(this.HalfEdge.Twin.AdjacentFace);
                return faces;
            }
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
