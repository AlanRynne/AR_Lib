﻿using System;
using System.Collections.Generic;

namespace Paramdigma.Core.HalfEdgeMesh
{
    /// <summary>
    /// Edge class representing a full edge of a half-edge mesh.
    /// A full edge contains 2 half-edges.
    /// </summary>
    public class MeshEdge
    {
        /// <summary>
        /// Gets or sets the half-edge linked to this edge.
        /// </summary>
        public MeshHalfEdge HalfEdge { get; set; }

        /// <summary>
        /// Gets or sets the index of this Mesh Edge.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshEdge"/> class.
        /// </summary>
        public MeshEdge()
        {
            Index = -1;
        }

        /// <summary>
        /// Gets a value indicating whether the mesh edge lies on a boundary.
        /// </summary>
        public bool OnBoundary => this.HalfEdge.OnBoundary || this.HalfEdge.Twin.OnBoundary;

        /// <summary>
        /// Gets the adjacent vertices of this given edge.
        /// </summary>
        /// <returns></returns>
        public List<MeshVertex> AdjacentVertices()
        {
            List<MeshVertex> vertices = new List<MeshVertex>
                {
                    this.HalfEdge.Vertex,
                    this.HalfEdge.Twin.Vertex,
                };
            return vertices;
        }

        /// <summary>
        /// Gets the adjacent faces of this edge.
        /// </summary>
        /// <returns></returns>
        public List<MeshFace> AdjacentFaces()
        {
            List<MeshFace> faces = new List<MeshFace>
                {
                    this.HalfEdge.AdjacentFace,
                    this.HalfEdge.Twin.AdjacentFace,
                };
            return faces;
        }

        /// <summary>
        /// Gets the adjacent edges of this edge.
        /// </summary>
        /// <returns></returns>
        public List<MeshEdge> AdjacentEdges()
        {
            List<MeshEdge> edges = new List<MeshEdge>();
            edges.AddRange(this.HalfEdge.Vertex.AdjacentEdges());
            edges.AddRange(this.HalfEdge.Twin.Vertex.AdjacentEdges());
            return edges;
        }
    }
}