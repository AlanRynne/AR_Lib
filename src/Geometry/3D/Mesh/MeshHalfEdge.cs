using System;

namespace AR_Lib.HalfEdgeMesh
{
    public class MeshHalfEdge
    {

        public MeshVertex Vertex; //Vertex at the base of half-edge
        public MeshEdge Edge; // Corresponding Edge
        public MeshFace Face; // Adjacent face
        public MeshCorner Corner; // Corresponding corner
        public MeshHalfEdge Next; // Next haf-edge in face
        public MeshHalfEdge Prev; // Previous half-edge in face
        public MeshHalfEdge Twin; // Opposite half-edge
        public bool onBoundary; // Check if it is on boundary or not
        public int Index; // Half-edge index

        // Constructor
        public MeshHalfEdge()
        {
            Index = -1;
        }

        // Calculated fields
        public MeshVertex PreviousVertex => Twin.Vertex;
        public MeshFace AdjacentFace => Twin.Face;

        // Utility methods
        public override string ToString()
        {
            return "Half-edge " + this.Index;
        }
    }
}
