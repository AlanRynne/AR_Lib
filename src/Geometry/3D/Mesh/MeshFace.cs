﻿using System.Collections.Generic;
using AR_Lib.Geometry;

namespace AR_Lib.HalfEdgeMesh
{
    /// <summary>
    /// Half-edge mesh face class.
    /// </summary>
    public class MeshFace
    {
        /// <summary>
        /// Gets or sets one of the  half-edges surrounding the face.
        /// </summary>
        public MeshHalfEdge HalfEdge { get; set; }

        /// <summary>
        /// Gets or sets the face index on the mesh face list.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets the area of the face.
        /// </summary>
        /// <returns>Returns the value of the face area.</returns>
        public double Area => MeshGeometry.Area(this);

        /// <summary>
        /// Gets the normal vector of the face.
        /// </summary>
        /// <returns>Returns the perpendicular vector to the face.</returns>
        public Vector3d Normal => MeshGeometry.FaceNormal(this);

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshFace"/> class.
        /// </summary>
        public MeshFace()
        {
            HalfEdge = null;
            Index = -1;
        }

        /// <summary>
        /// Get all adjacent edges to this face.
        /// </summary>
        /// <returns>Returns a list of all adjacent edges in order.</returns>
        public List<MeshEdge> AdjacentEdges()
        {
            MeshHalfEdge edge = this.HalfEdge;
            List<MeshEdge> edges = new List<MeshEdge>();
            do
            {
                edges.Add(edge.Edge);
                edge = edge.Next;
            }
            while (edge != this.HalfEdge);

            return edges;
        }

        /// <summary>
        /// Get all adjacent half-edges to this face.
        /// </summary>
        /// <returns>Returns a list of all adjacent half-edges in order.</returns>
        public List<MeshHalfEdge> AdjacentHalfEdges()
        {
            MeshHalfEdge edge = this.HalfEdge;
            List<MeshHalfEdge> halfEdges = new List<MeshHalfEdge>();
            do
            {
                halfEdges.Add(edge);
                edge = edge.Next;
            }
            while (edge != this.HalfEdge);

            return halfEdges;
        }

        /// <summary>
        /// Get all adjacent vertices to this face.
        /// </summary>
        /// <returns>Returns a list of all adjacent vertices in order.</returns>
        public List<MeshVertex> AdjacentVertices()
        {
            List<MeshVertex> vertices = new List<MeshVertex>();
            MeshHalfEdge edge = this.HalfEdge;
            do
            {
                vertices.Add(edge.Vertex);
                edge = edge.Next;
            }
            while (edge != this.HalfEdge);
            return vertices;
        }

        /// <summary>
        /// Get all adjacent faces to this face.
        /// </summary>
        /// <returns>Returns a list of all adjacent faces in order.</returns>
        public List<MeshFace> AdjacentFaces()
        {
            List<MeshFace> faces = new List<MeshFace>();
            MeshHalfEdge edge = this.HalfEdge;
            do
            {
                faces.Add(edge.Twin.Face);
                edge = edge.Next;
            }
            while (edge != this.HalfEdge);
            return faces;
        }

        /// <summary>
        /// Get all adjacent corners to this face.
        /// </summary>
        /// <returns>Returns a list of all adjacent corners in order.</returns>
        public List<MeshCorner> AdjacentCorners()
        {
            List<MeshCorner> corners = new List<MeshCorner>();
            MeshHalfEdge edge = this.HalfEdge;
            do
            {
                corners.Add(edge.Corner);
                edge = edge.Next;
            }
            while (edge != this.HalfEdge);
            return corners;
        }

        /// <summary>
        /// Checks if the current face is a boundary face.
        /// </summary>
        /// <returns>Returns true if the face is a boundary face, false if not.</returns>
        public bool IsBoundaryLoop() => this.HalfEdge.OnBoundary;

        /// <summary>
        /// Convert the mesh face to string.
        /// </summary>
        /// <returns>Returns the string representation of the mesh face.</returns>
        public override string ToString()
        {
            List<MeshVertex> faceVertices = this.AdjacentVertices();
            string text = "F";
            foreach (MeshVertex v in faceVertices)
            {
                text += " ";
                text += v.Index;
            }

            return text;
        }
    }
}
