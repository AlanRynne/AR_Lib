using System.Collections.Generic;
using AR_Lib.Geometry;

namespace AR_Lib
{
    namespace HalfEdgeMesh
    {
        /// <summary>
        /// Half-edge mesh face class
        /// </summary>
        public class HE_Face
        {
            #region Properties & Fields

            /// <summary>
            /// One of the  half-edges surrounding the face
            /// </summary>
            public HE_HalfEdge HalfEdge;

            /// <summary>
            /// The face index on the mesh face list.
            /// </summary>
            public int Index;

            /// <summary>
            /// Computes the area of the face.
            /// </summary>
            /// <returns>Returns the value of the face area.</returns>
            public double Area => HE_MeshGeometry.Area(this);

            /// <summary>
            /// Compute the normal vector of the face.
            /// </summary>
            /// <returns>Returns the perpendicular vector to the face</returns>
            public Vector3d Normal => HE_MeshGeometry.FaceNormal(this);

            #endregion

            #region Constructors

            /// <summary>
            /// Initialize an empty half-edge mesh face.
            /// </summary>
            public HE_Face()
            {
                HalfEdge = null;
                Index = -1;
            }

            #endregion

            #region Topology related methods

            /// <summary>
            /// Get all adjacent edges to this face.
            /// </summary>
            /// <returns>Returns a list of all adjacent edges in order.</returns>
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

            /// <summary>
            /// Get all adjacent half-edges to this face.
            /// </summary>
            /// <returns>Returns a list of all adjacent half-edges in order.</returns>
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

            /// <summary>
            /// Get all adjacent vertices to this face.
            /// </summary>
            /// <returns>Returns a list of all adjacent vertices in order.</returns>
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

            /// <summary>
            /// Get all adjacent faces to this face.
            /// </summary>
            /// <returns>Returns a list of all adjacent faces in order.</returns>
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

            /// <summary>
            /// Get all adjacent corners to this face.
            /// </summary>
            /// <returns>Returns a list of all adjacent corners in order.</returns>
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

            /// <summary>
            /// Checks if the current face is a boundary face.
            /// </summary>
            /// <returns>Returns true if the face is a boundary face, false if not.</returns>
            public bool isBoundaryLoop() => this.HalfEdge.onBoundary;

            #endregion

            #region Overriden Methods

            /// <summary>
            /// Convert the mesh face to string.
            /// </summary>
            /// <returns>Returns the string representation of the mesh face.</returns>
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

            #endregion

        }
    }
}
