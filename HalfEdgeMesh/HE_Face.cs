using System.Collections.Generic;

namespace AR_Lib
{
    namespace HalfEdgeMesh
    {
        /// <summary>
        /// Half-edge mesh face class
        /// </summary>
        public class HE_Face
        {
            public HE_HalfEdge HalfEdge; //One of the half-edges surrounding the face
            public int Index;

            // Constructor

            public HE_Face()
            {
                HalfEdge = null;
                Index = -1;
            }

            public List<HE_Edge> adjacentEdges()
            {
                HE_HalfEdge _edge = this.HalfEdge;
                List<HE_Edge> _edges = new List<HE_Edge>();
                do
                {
                    _edges.Add(_edge.Edge);
                    _edge = _edge.Next;
                }
                while (_edge != this.HalfEdge);

                return _edges;
            }

            public List<HE_HalfEdge> adjacentHalfEdges()
            {
                HE_HalfEdge _edge = this.HalfEdge;
                List<HE_HalfEdge> _halfEdges = new List<HE_HalfEdge>();
                do
                {
                    _halfEdges.Add(_edge);
                    _edge = _edge.Next;
                }
                while (_edge != this.HalfEdge);

                return _halfEdges;
            }

            public List<HE_Vertex> adjacentVertices()
            {
                List<HE_Vertex> _vertices = new List<HE_Vertex>();
                HE_HalfEdge _edge = this.HalfEdge;
                do
                {
                    _vertices.Add(_edge.Vertex);
                    _edge = _edge.Next;

                } while (_edge != this.HalfEdge);
                return _vertices;

            }

            public List<HE_Face> adjacentFaces()
            {
                List<HE_Face> _faces = new List<HE_Face>();
                HE_HalfEdge _edge = this.HalfEdge;
                do
                {
                    _faces.Add(_edge.Twin.Face);
                    _edge = _edge.Next;
                } while (_edge != this.HalfEdge);
                return _faces;
            }

            public List<HE_Corner> adjacentCorners()
            {
                List<HE_Corner> _corners = new List<HE_Corner>();
                HE_HalfEdge _edge = this.HalfEdge;
                do
                {
                    _corners.Add(_edge.Corner);
                    _edge = _edge.Next;
                } while (_edge != this.HalfEdge);
                return _corners;
            }

            public bool isBoundaryLoop() => this.HalfEdge.onBoundary;

            public override string ToString()
            {
                List<HE_Vertex> faceVertices = this.adjacentVertices();
                string text = "F";
                foreach (HE_Vertex v in faceVertices)
                {
                    text += " ";
                    text += v.Index;

                }
                return text;
            }

        }
    }
}
