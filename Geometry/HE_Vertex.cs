using System.Collections.Generic;
using System.Collections;
using AR_Lib.Geometry;

namespace AR_Lib.HalfEdgeMesh
{
    /// <summary>
    /// Vertex class 
    /// Inherits from AR_Point
    /// </summary>
   public class HE_Vertex : Point3d
    {

        public HE_HalfEdge HalfEdge; //One of the half-edges connected to the vertex
        public int Index;

        public Dictionary<string, double> UserValues
        {
            get
            {
                // Set private property to auto initialize if null.
                if (_userValues == null) return new Dictionary<string, double>();
                else return _userValues;
            }
            set
            {
                _userValues = value;
            }
        }

        private Dictionary<string, double> _userValues;

        // Constructor
        public HE_Vertex(): base() { _userValues = new Dictionary<string, double>(); }
        public HE_Vertex(Point3d pt) : base(pt){ _userValues = new Dictionary<string, double>(); }
        public HE_Vertex(double X, double Y, double Z) : base(X, Y, Z){ _userValues = new Dictionary<string, double>(); }

        // Calculate the valence of a vertex
        public int Degree() => adjacentHalfEdges().Count;

        // Check if vertex is isolated, meaning corresponding half-edge is null
        public bool IsIsolated() => this.HalfEdge == null;

        // Check if vertex is on mesh boundary
        public bool OnBoundary()
        {
            foreach(HE_HalfEdge halfEdge in adjacentHalfEdges())
            {
                if (halfEdge.onBoundary) return true;
            }
            return false;
        }

        // Returns a list with all adjacent HE_HalfEdge of this vertex
        public List<HE_HalfEdge> adjacentHalfEdges()
        {
            HE_HalfEdge _halfEdge = this.HalfEdge;
            List<HE_HalfEdge> _halfEdges = new List<HE_HalfEdge>();
            do
            {
                _halfEdges.Add(_halfEdge);
                _halfEdge = _halfEdge.Twin.Next;
            }
            while (_halfEdge != this.HalfEdge);

            return _halfEdges;
            
        }

        // Returns a list with all adjacent HE_Face of a vertex
        public List<HE_Face> adjacentFaces()
        {
            HE_HalfEdge _halfEdge = this.HalfEdge;
            List<HE_Face> _faces = new List<HE_Face>();
            do
            {
                if (!_halfEdge.onBoundary) _faces.Add(_halfEdge.Face);
                _halfEdge = _halfEdge.Twin.Next;
            }
            while (_halfEdge != this.HalfEdge);

            return _faces;
        }

        // Returns a list with all the adjacent HE_Vertex of this vertex
        public List<HE_Vertex> adjacentVertices()
        {
            List<HE_Vertex> _vertices = new List<HE_Vertex>();
            HE_HalfEdge _halfEdge = this.HalfEdge;
            do
            {
                _vertices.Add(_halfEdge.Twin.Vertex);
                _halfEdge = _halfEdge.Twin.Next;
            } while (_halfEdge != this.HalfEdge);
              
            return _vertices;
        }

        // Returns a list with all the adjacent HE_Edge of this vertex
        public List<HE_Edge> adjacentEdges()
        {
            List<HE_Edge> _edges = new List<HE_Edge>();
            HE_HalfEdge _halfEdge = this.HalfEdge;
            do
            {
                _edges.Add(_halfEdge.Edge);
                _halfEdge = _halfEdge.Twin.Next;

            } while (_halfEdge != this.HalfEdge);

            return _edges;
        }

        // Returns a list with all the adjacent HE_Corners of this vertex
        public List<HE_Corner> adjacentCorners()
        {
            List<HE_Corner> _corners = new List<HE_Corner>();
            HE_HalfEdge _halfEdge = this.HalfEdge;
            do
            {
                if (!_halfEdge.onBoundary) _corners.Add(_halfEdge.Next.Corner);
                _halfEdge = _halfEdge.Twin.Next;

            } while (_halfEdge != this.HalfEdge);

            return _corners;
        }

        public override string ToString()
        {
            return "V "+ Index;
        }
    }

}
