using System;

namespace AR_Lib.HalfEdgeMesh
{
    /// <summary>
    /// Represents a mesh half-edge.
    /// </summary>
    public class MeshHalfEdge
    {
        /// <summary>
        /// The vertex linked to this half-edge.
        /// </summary>
        public MeshVertex Vertex;

        /// <summary>
        /// The edge linked to this half-edge.
        /// </summary>
        public MeshEdge Edge;

        /// <summary>
        /// The face linked to this half-edge.
        /// </summary>
        public MeshFace Face;

        /// <summary>
        /// The corner linked to this half-edge.
        /// </summary>
        public MeshCorner Corner;

        /// <summary>
        /// The next half-edge in a face.
        /// </summary>
        public MeshHalfEdge Next;

        /// <summary>
        /// The previous half-edge in a face.
        /// </summary>
        public MeshHalfEdge Prev;

        /// <summary>
        /// The opposite half-edge.
        /// </summary>
        public MeshHalfEdge Twin;

        /// <summary>
        /// Checks if the half-edge lies on a boundary.
        /// </summary>
        public bool OnBoundary;

        /// <summary>
        /// The half-edge index.
        /// </summary>
        public int Index;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshHalfEdge"/> class.
        /// </summary>
        public MeshHalfEdge()
        {
            Index = -1;
        }

        /// <summary>
        /// Gets the previous vertex of the half-edge.
        /// </summary>
        public MeshVertex PreviousVertex => Twin.Vertex;

        /// <summary>
        /// Gets the opposite face of the half-edge.
        /// </summary>
        public MeshFace AdjacentFace => Twin.Face;

        /// <summary>
        /// Gets the string representation of the half-edge.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Half-edge " + this.Index;
        }
    }
}
