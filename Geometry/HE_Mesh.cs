using System.Collections.Generic;
using AR_Lib.Geometry;
using AR_Lib;

namespace AR_Lib
{
    namespace HalfEdgeMesh
    {
        /// <summary>
        /// The Half-Edge Mesh class
        /// </summary>
        public class HE_Mesh
        {
            public List<HE_Vertex> Vertices;
            public List<HE_Edge> Edges;
            public List<HE_Face> Faces;
            public List<HE_Corner> Corners;
            public List<HE_HalfEdge> HalfEdges;
            public List<HE_Face> Boundaries;
            public List<HE_Generators> Generators;

            public HE_Mesh()
            {
                Vertices = new List<HE_Vertex>();
                Edges = new List<HE_Edge>();
                Faces = new List<HE_Face>();
                Corners = new List<HE_Corner>();
                HalfEdges = new List<HE_HalfEdge>();
                Boundaries = new List<HE_Face>();
                Generators = new List<HE_Generators>();
            }

            public HE_Mesh(List<Point3d> vertices, List<List<int>> faceIndexes): this()
            {
                //There are 3 steps for this process
                //- Iterate through vertices, create vertex objects
                createVertices(vertices);
                //- Iterate through faces, creating face, edge, and halfedge objects (and connecting where possible)
                createFaces(faceIndexes);
            }
            void createVertices(List<Point3d> points)
            {
                List<HE_Vertex> verts = new List<HE_Vertex>(points.Count);

                foreach (Point3d pt in points)
                {
                    HE_Vertex vertex = new HE_Vertex(pt.X, pt.Y, pt.Z);
                    verts.Add(vertex);
                }
                Vertices = verts;
            }

            // Takes a List containing another List per face with the vertex indexes belonging to that face
            bool createFaces(List<List<int>> faceIndexes)
            {
                Dictionary<string, int> edgeCount = new Dictionary<string, int>();
                Dictionary<string, HE_HalfEdge> existingHalfEdges = new Dictionary<string, HE_HalfEdge>();
                Dictionary<HE_HalfEdge, bool> hasTwinHalfEdge = new Dictionary<HE_HalfEdge, bool>();
                // Create the faces, edges and half-edges, non-boundary loops and link references when possible;
                foreach ( List<int> indexes in faceIndexes )
                {
                    HE_Face f = new HE_Face();
                    Faces.Add(f);

                    List<HE_HalfEdge> tempHEdges = new List<HE_HalfEdge>(indexes.Count);
                    //Create empty half-edges
                    for (int i = 0 ;i < indexes.Count; i++)
                    {
                        HE_HalfEdge h = new HE_HalfEdge();
                        tempHEdges.Add(h);
                    }

                    //Fill out each half edge
                    for (int i = 0; i < indexes.Count; i++)
                    {
                        //Edge goes from v0 to v1
                        int v0 = indexes[i];
                        int v1 = indexes[(i + 1) % indexes.Count];

                        HE_HalfEdge h = tempHEdges[i];
                        // Set previous and next
                        h.Next = tempHEdges[(i + 1) % indexes.Count];
                        h.Prev = tempHEdges[(i + indexes.Count - 1) % indexes.Count];

                        h.onBoundary = false;
                        hasTwinHalfEdge.Add(h, false);

                        // Set half-edge & vertex mutually
                        h.Vertex = Vertices[v0];
                        Vertices[v0].HalfEdge = h;

                        // Set half-edge face & vice versa
                        h.Face = f;
                        f.HalfEdge = h;

                        // Reverse v0 and v1 if v0 > v1
                        if (v0 > v1) 
                        { 
                            int temp = v0; 
                            v0 = v1; 
                            v1 = temp; 
                        }
                        string key = v0 + " " + v1;
                        if (existingHalfEdges.ContainsKey(key))
                        {
                            // If this half-edge key already exists, it is the twin of this current half-edge
                            HE_HalfEdge twin = existingHalfEdges[key];
                            h.Twin = twin;
                            twin.Twin = h;
                            h.Edge = twin.Edge;
                            hasTwinHalfEdge[h] = true;
                            hasTwinHalfEdge[twin] = true;
                            edgeCount[key] += 1;
                        } else {
                            // Create an edge and set its half-edge
                            HE_Edge e = new HE_Edge();
                            Edges.Add(e);
                            h.Edge = e;
                            e.HalfEdge = h;

                            // Record the newly created half-edge
                            existingHalfEdges.Add(key, h);
                            edgeCount.Add(key, 1);
                        }

                    }

                    HalfEdges.AddRange(tempHEdges);
                }

                // Create boundary edges
                for (int i = 0; i < HalfEdges.Count; i++)
                {
                    HE_HalfEdge h = HalfEdges[i];
                    if (!hasTwinHalfEdge[h])
                    {
                        HE_Face f = new HE_Face();
                        Boundaries.Add(f);

                        List<HE_HalfEdge> boundaryCycle = new List<HE_HalfEdge>();
                        HE_HalfEdge hE = h;
                        do
                        {
                            HE_HalfEdge bH = new HE_HalfEdge();
                            HalfEdges.Add(bH);
                            boundaryCycle.Add(bH);

                            HE_HalfEdge nextHE = hE.Next;
                            while (hasTwinHalfEdge[nextHE]) nextHE = nextHE.Twin.Next;

                            bH.Vertex = nextHE.Vertex;
                            bH.Edge = hE.Edge;
                            bH.onBoundary = true;

                            bH.Face = f;
                            f.HalfEdge = bH;

                            bH.Twin = hE;
                            hE.Twin = bH;

                            hE = nextHE;
                        } while (hE != h);

                        int n = boundaryCycle.Count;
                        for (int j = 0; j < n; j++) 
                        {
                            boundaryCycle[j].Next = boundaryCycle[(j + n - 1) % n];
                            boundaryCycle[j].Prev = boundaryCycle[(j + 1) % n];
                            hasTwinHalfEdge[boundaryCycle[j]] = true;
                            hasTwinHalfEdge[boundaryCycle[j].Twin] = true;
                        }
                    }

                    if (!h.onBoundary)
                    {
                        HE_Corner corner = new HE_Corner();
                        corner.HalfEdge = h;
                        h.Corner = corner;
                        Corners.Add(corner);
                    }
                }

                if ( HasIsolatedFaces() || HasIsolatedVertices()|| HasNonManifoldEdges() )
                {
                    return false;
                }

                indexElements();

                return true;
            }

            public int EulerCharacteristic => Vertices.Count - Edges.Count + Faces.Count;

            public bool HasIsolatedVertices()
            {
                foreach (HE_Vertex v in Vertices)
                {
                    if (v.IsIsolated())
                    {
                        return true;
                    }
                }
                return false;
            }
        
            public bool HasIsolatedFaces()
            {
                foreach (HE_Face  f in Faces)
                {
                    int boundaryEdges = 0;
                    List<HE_HalfEdge> adjacent = f.adjacentHalfEdges();
                    foreach (HE_HalfEdge e in adjacent)
                    {
                        if (e.onBoundary) boundaryEdges++;
                    }
                    if (boundaryEdges == adjacent.Count) return true;
                }
                return false;
            }

            public bool HasNonManifoldEdges()
            {
                return false;
            }

            public void indexElements()
            {
                int index = -1;
                foreach (HE_Vertex v in Vertices) v.Index = index++;

                index = -1;
                foreach (HE_Face f in Faces) f.Index = index++;

                index = -1;
                foreach (HE_HalfEdge hE in HalfEdges) hE.Index = index++;

                index = -1;
                foreach (HE_Edge e in Edges) e.Index = index++;

                index = -1;
                foreach (HE_Corner c in Corners) c.Index = index++;

                index = -1;
                foreach (HE_Face b in Boundaries) b.Index = index++;

            }

            public Dictionary<HE_Vertex,int> indexVertices()
            {
                int i = -1;
                Dictionary<HE_Vertex, int> index = new Dictionary<HE_Vertex, int>();
                foreach (var v in Vertices)
                    index[v] = i++;
                return index;
            }
            public Dictionary<HE_Face,int> indexFaces()
            {
                int i = -1;
                Dictionary<HE_Face, int> index = new Dictionary<HE_Face, int>();
                foreach (var v in Faces)
                    index[v] = i++;
                return index;
            }
            public Dictionary<HE_Edge,int> indexEdges()
            {
                int i = -1;
                Dictionary<HE_Edge, int> index = new Dictionary<HE_Edge, int>();
                foreach (var v in Edges)
                    index[v] = i++;
                return index;
            }
            public Dictionary<HE_HalfEdge,int> indexHalfEdes()
            {
                int i = -1;
                Dictionary<HE_HalfEdge, int> index = new Dictionary<HE_HalfEdge, int>();
                foreach (var f in HalfEdges)
                    index[f] = i++;
                return index;
            }
            public Dictionary<HE_Corner,int> indexCorners()
            {
                int i = -1;
                Dictionary<HE_Corner, int> index = new Dictionary<HE_Corner, int>();
                foreach (var f in Corners)
                    index[f] = i++;
                return index;
            }

            public override string ToString()
            {
                string head = "--- Mesh Info ---\n";
                string VEF = "V: " + Vertices.Count + "; F: " + Faces.Count + "; E:" + Edges.Count + "\n";
                string HEC = "Half-edges: " + HalfEdges.Count + "; Corners: " + Corners.Count + "\n";
                string Bounds = "Boundaries: " + Boundaries.Count + "\n";
                string euler = "Euler characteristic: " + EulerCharacteristic + "\n";
                string isoVert = "Isolated vertices: " + HasIsolatedVertices().ToString() + "\n";
                string isoFace = "Isolated faces: " + HasIsolatedFaces().ToString() + "\n";
                string manifold = "Has Non-Manifold Edges: " + HasNonManifoldEdges().ToString() + "\n";
                string tail = "-----       -----\n\n";



                return head + VEF + HEC + Bounds + euler + isoVert + isoFace + manifold + tail;
            }
        }
    }
}
