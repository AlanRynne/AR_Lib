using System.Collections.Generic;
using System.Collections;
using AR_Lib.Geometry;

namespace AR_Lib.HalfEdgeMesh
{
    /// <summary>
    /// Represents a vertex of a mesh.
    /// </summary>
    public class MeshVertex : Point3d
    {
        /// <summary>
        /// The half-edge this vertex is attached to.
        /// </summary>
        public MeshHalfEdge HalfEdge;
        /// <summary>
        /// The index of the vertex.
        /// </summary>
        public int Index;

        /// <summary>
        /// Dictionary of user values
        /// </summary>
        /// <value></value>
        public Dictionary<string, double> UserValues
        {
            get
            {
                // Set private property to auto initialize if null.
                if (userValues == null)
                    return new Dictionary<string, double>();
                else
                    return userValues;
            }
            set
            {
                userValues = value;
            }
        }

        private Dictionary<string, double> userValues;

        // Constructor

        /// <summary>
        /// Constructs an empty mesh vertex.
        /// </summary>
        public MeshVertex() : base() { userValues = new Dictionary<string, double>(); }

        /// <summary>
        /// Constructs a new mesh vertex from a 3D point.
        /// </summary>
        /// <param name="pt">Point to copy coordinates from.</param>
        public MeshVertex(Point3d pt) : base(pt) { userValues = new Dictionary<string, double>(); }

        /// <summary>
        /// Constructs a new mesh vertex from it's cartesian coordiantes.
        /// </summary>
        /// <param name="x">X Coordiante.</param>
        /// <param name="y">Y Coordinate.</param>
        /// <param name="z">Z Coordinate.</param>
        public MeshVertex(double x, double y, double z) : base(x, y, z) { userValues = new Dictionary<string, double>(); }

        // Calculate the valence of a vertex

        /// <summary>
        /// Computes the valence of the vertex
        /// </summary>
        public int Valence() => AdjacentHalfEdges().Count;

        /// <summary>
        /// Check if vertex is isolated, meaning corresponding half-edge is null
        /// </summary>
        public bool IsIsolated() => this.HalfEdge == null;

        /// <summary>
        /// Check if vertex is on mesh boundary
        /// </summary>
        public bool OnBoundary()
        {
            foreach (MeshHalfEdge halfEdge in AdjacentHalfEdges())
            {
                if (halfEdge.onBoundary)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a list with all adjacent HE_HalfEdge of this vertex
        /// </summary>
        public List<MeshHalfEdge> AdjacentHalfEdges()
        {
            MeshHalfEdge _halfEdge = this.HalfEdge;
            List<MeshHalfEdge> _halfEdges = new List<MeshHalfEdge>();
            do
            {
                _halfEdges.Add(_halfEdge);
                _halfEdge = _halfEdge.Twin.Next;
            }
            while (_halfEdge != this.HalfEdge);

            return _halfEdges;

        }

        /// <summary>
        /// Returns a list with all adjacent HE_Face of a vertex
        /// </summary>
        public List<MeshFace> AdjacentFaces()
        {
            MeshHalfEdge _halfEdge = this.HalfEdge;
            List<MeshFace> _faces = new List<MeshFace>();
            do
            {
                if (!_halfEdge.onBoundary)
                    _faces.Add(_halfEdge.Face);
                _halfEdge = _halfEdge.Twin.Next;
            }
            while (_halfEdge != this.HalfEdge);

            return _faces;
        }

        /// <summary>
        /// Returns a list with all the adjacent HE_Vertex of this vertex
        /// </summary>
        public List<MeshVertex> AdjacentVertices()
        {
            List<MeshVertex> _vertices = new List<MeshVertex>();
            MeshHalfEdge _halfEdge = this.HalfEdge;
            do
            {
                _vertices.Add(_halfEdge.Twin.Vertex);
                _halfEdge = _halfEdge.Twin.Next;
            } while (_halfEdge != this.HalfEdge);

            return _vertices;
        }

        /// <summary>
        /// Returns a list with all the adjacent HE_Edge of this vertex
        /// </summary>
        public List<MeshEdge> AdjacentEdges()
        {
            List<MeshEdge> _edges = new List<MeshEdge>();
            MeshHalfEdge _halfEdge = this.HalfEdge;
            do
            {
                _edges.Add(_halfEdge.Edge);
                _halfEdge = _halfEdge.Twin.Next;

            } while (_halfEdge != this.HalfEdge);

            return _edges;
        }

        /// <summary>
        /// Returns a list with all the adjacent HE_Corners of this vertex
        /// </summary>
        public List<MeshCorner> AdjacentCorners()
        {
            List<MeshCorner> _corners = new List<MeshCorner>();
            MeshHalfEdge _halfEdge = this.HalfEdge;
            do
            {
                if (!_halfEdge.onBoundary)
                    _corners.Add(_halfEdge.Next.Corner);
                _halfEdge = _halfEdge.Twin.Next;

            } while (_halfEdge != this.HalfEdge);

            return _corners;
        }

        /// <summary>
        /// Returns the string representation of this vertex.
        /// </summary>
        public override string ToString()
        {
            return "V " + Index;
        }
    }

}
