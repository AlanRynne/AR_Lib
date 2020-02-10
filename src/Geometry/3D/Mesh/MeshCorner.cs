

namespace AR_Lib.HalfEdgeMesh
{
    /// <summary>
    /// Represents a corner of a given mesh face.
    /// </summary>
    public class MeshCorner
    {
        /// <summary>
        /// Gets or sets the corner's first half-edge
        /// </summary>
        public MeshHalfEdge HalfEdge;

        /// <summary>
        /// Gets or sets the index of the mesh corner.
        /// </summary>
        public int Index;

        /// <summary>
        /// Constructs a new empty mesh corner.
        /// </summary>
        public MeshCorner()
        {
            Index = -1;
        }

        /// <summary>
        /// Gets the mesh corner vertex.
        /// </summary>
        public MeshVertex Vertex => this.HalfEdge.Prev.Vertex;

        /// <summary>
        /// Gets the face the mesh corner belongs to.
        /// </summary>
        public MeshFace Face => this.HalfEdge.Face;

        /// <summary>
        /// Gets the next corner.
        /// </summary>
        public MeshCorner Next => this.HalfEdge.Next.Corner;

        /// <summary>
        /// Gets the previous corner.
        /// </summary>
        public MeshCorner Prev => this.HalfEdge.Prev.Corner;

    }
}
