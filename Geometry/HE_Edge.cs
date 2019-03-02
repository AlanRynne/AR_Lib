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

        }
    }
}
