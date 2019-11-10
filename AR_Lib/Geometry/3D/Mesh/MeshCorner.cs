

namespace AR_Lib.HalfEdgeMesh
{
    /// <summary>
    /// Represents a corner of a given mesh face.
    /// </summary>
    public class MeshCorner
    {
        /// <summary>
        /// The corner's first half-edge
        /// </summary>
        public MeshHalfEdge HalfEdge;
        public int Index;

        public MeshCorner()
        {
            Index = -1;
        }

        public MeshVertex Vertex => this.HalfEdge.Prev.Vertex;
        public MeshFace Face => this.HalfEdge.Face;
        public MeshCorner Next => this.HalfEdge.Next.Corner;
        public MeshCorner Prev => this.HalfEdge.Prev.Corner;

    }
}
