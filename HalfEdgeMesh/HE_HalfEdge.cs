using System;

namespace AR_Lib.HalfEdgeMesh
{
    public class HE_HalfEdge
    {
        
        public HE_Vertex Vertex; //Vertex at the base of half-edge
        public HE_Edge Edge; // Corresponding Edge
        public HE_Face Face; // Adjacent face
        public HE_Corner Corner; // Corresponding corner
        public HE_HalfEdge Next; // Next haf-edge in face
        public HE_HalfEdge Prev; // Previous half-edge in face
        public HE_HalfEdge Twin; // Opposite half-edge 
        public bool onBoundary; // Check if it is on boundary or not
        public int Index; // Half-edge index

        // Constructor
        public HE_HalfEdge()
        {
            Index = -1;
        }

        // Calculated fields
        public HE_Vertex PreviousVertex => Twin.Vertex;
        public HE_Face AdjacentFace => Twin.Face;

        // Utility methods
        public override string ToString()
        {
            return "Half-edge " + this.Index;
        }
    }
}
