using System;
using System.Collections.Generic;
using AR_Lib.HalfEdgeMesh;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents the geometry of a HE_Mesh such as positions at vertices
    /// </summary>
    public static class HE_MeshGeometry
    {

        /// <summary>
        /// Calculate the vector of a specified half-edge.
        /// </summary>
        /// <returns>The half-edge vector.</returns>
        /// <param name="halfEdge">Half edge.</param>
        public static Vector3d Vector(HE_HalfEdge halfEdge) => halfEdge.Vertex - halfEdge.Next.Vertex;

        /// <summary>
        /// Calculates the length of the specified edge.
        /// </summary>
        /// <returns>The length.</returns>
        /// <param name="edge">Edge.</param>
        public static double Length(HE_Edge edge) => Vector(edge.HalfEdge).Norm;

        /// <summary>
        /// Calculates the midpoint of the specifiec edge.
        /// </summary>
        /// <returns>The point.</returns>
        /// <param name="edge">Edge.</param>
        public static Point3d MidPoint(HE_Edge edge)
        {
            HE_HalfEdge halfEdge = edge.HalfEdge;
            Point3d a = halfEdge.Vertex;
            Point3d b = halfEdge.Twin.Vertex;
            return ((a + b) / 2);
        }

        /// <summary>
        /// Calculates the mean edge length of the mesh
        /// </summary>
        /// <returns>The mean edge length of the mesh</returns>
        public static double MeanEdgeLength(HE_Mesh Mesh)
        {
            double sum = 0.0;
            foreach (HE_Edge e in Mesh.Edges) sum += Length(e);
            return sum / Mesh.Edges.Count;
        }

        /// <summary>
        /// Computes the area of the specified face
        /// </summary>
        /// <returns>The face area</returns>
        /// <param name="face">Face.</param>
        public static double Area(HE_Face face)
        {
            if (face.isBoundaryLoop()) return 0.0;

            Vector3d u = Vector(face.HalfEdge);
            Vector3d v = -Vector(face.HalfEdge.Prev);
            return 0.5 * u.Cross(v).Norm;
        }

        /// <summary>
        /// Computes the total area of the mesh
        /// </summary>
        /// <returns>The mesh area.</returns>
        public static double TotalArea(HE_Mesh Mesh)
        {
            double sum = 0.0;
            foreach (HE_Face f in Mesh.Faces) sum += Area(f);
            return sum;
        }

        /// <summary>
        /// Compute the normal vector of the specified face
        /// </summary>
        /// <returns>The normal.</returns>
        /// <param name="face">Face.</param>
        public static Vector3d FaceNormal(HE_Face face)
        {
            if (face.isBoundaryLoop()) return null;

            Vector3d u = Vector(face.HalfEdge);
            Vector3d v = -Vector(face.HalfEdge.Prev);
            return u.Cross(v).Unit();
        }

        /// <summary>
        /// Compute the centroid of the specified face
        /// </summary>
        /// <returns>The centroid.</returns>
        /// <param name="face">Face.</param>
        public static Point3d Centroid(HE_Face face)
        {
            HE_HalfEdge hE = face.HalfEdge;
            Point3d a = hE.Vertex;
            Point3d b = hE.Next.Vertex;
            Point3d c = hE.Prev.Vertex;

            if (face.isBoundaryLoop()) return (a + b) / 2;

            return (a + b + c) / 3;
        }

        /// <summary>
        /// Compute the circumcenter the specified face.
        /// </summary>
        /// <returns>The circumcenter.</returns>
        /// <param name="face">Face.</param>
        public static Point3d Circumcenter(HE_Face face)
        {
            HE_HalfEdge hE = face.HalfEdge;

            Point3d a = hE.Vertex;
            Point3d b = hE.Next.Vertex;
            Point3d c = hE.Prev.Vertex;

            if (face.isBoundaryLoop()) return (a + b) / 2;

            Vector3d ac = c - a;
            Vector3d ab = b - a;
            Vector3d w = ab.Cross(ac);

            Vector3d u = (w.Cross(ab)) * ac.Norm2;
            Vector3d v = (ac.Cross(w)) * ab.Norm2;

            Point3d x = (u + v) / (2 * w.Norm2);

            return x + a;
        }

        /// <summary>
        /// Compute the orthonormal bases of the specified face
        /// </summary>
        /// <returns>Array containing the 2 Vector3d.</returns>
        /// <param name="face">Face.</param>
        public static Vector3d[] OrthonormalBases(HE_Face face)
        {
            Vector3d e1 = Vector(face.HalfEdge).Unit();
            Vector3d normal = FaceNormal(face);
            Vector3d e2 = normal.Cross(e1);

            return new Vector3d[]{ e1 , e2 };
        }

        /// <summary>
        /// Compute the angle (in radians) at the specified corner
        /// </summary>
        /// <returns>The angle (in radians(</returns>
        /// <param name="corner">Corner.</param>
        public static double Angle(HE_Corner corner)
        {
            Vector3d u = Vector(corner.HalfEdge.Prev).Unit();
            Vector3d v = (-Vector(corner.HalfEdge.Next).Unit());

            return Math.Acos(Math.Max(-1, Math.Min(1.0, u.Dot(v))));
        }

        /// <summary>
        /// Computes the cotangent of the angle opposite to a halfedge.
        /// </summary>
        /// <returns>The cotan.</returns>
        /// <param name="hE">H e.</param>
        public static double Cotan(HE_HalfEdge hE)
        {
            if (hE.onBoundary) return 0.0;

            Vector3d u = Vector(hE.Prev);
            Vector3d v = -Vector(hE.Next);

            return u.Dot(v) / u.Cross(v).Norm;

        }

        /// <summary>
        /// Computes the signed angle (in radians) between the faces adjacent to the specified half-edge.
        /// </summary>
        /// <returns>The angle (in radians) between faces.</returns>
        /// <param name="hE">H e.</param>
        public static double DihedralAngle(HE_HalfEdge hE)
        {
            if (hE.onBoundary || hE.Twin.onBoundary) return 0.0;

            Vector3d n1 = FaceNormal(hE.Face);
            Vector3d n2 = FaceNormal(hE.Twin.Face);
            Vector3d w = Vector(hE).Unit();

            double cosTheta = n1.Dot(n2);
            double sinTheta = n1.Cross(n2).Dot(w);

            return Math.Atan2(sinTheta, cosTheta);
        }

        /// <summary>
        /// Computes the barycentric dual area around a given mesh vertex.
        /// </summary>
        /// <returns>The dual area.</returns>
        /// <param name="vertex">Vertex.</param>
        public static double BarycentricDualArea(HE_Vertex vertex)
        {
            double area = 0.0;
            foreach (HE_Face f in vertex.adjacentFaces()) area += Area(f);
            return area;
        }

        /// <summary>
        /// Computes the circumcentric dual area around a given mesh vertex.
        /// </summary>
        /// <returns>The dualarea.</returns>
        /// <param name="vertex">Vertex.</param>
        public static double CircumcentricDualarea(HE_Vertex vertex)
        {
            double area = 0.0;
            foreach (HE_HalfEdge hE in vertex.adjacentHalfEdges())
            {
                double u2 = Vector(hE.Prev).Norm2;
                double v2 = Vector(hE).Norm2;
                double cotAlpha = Cotan(hE.Prev);
                double cotBeta = Cotan(hE);

                area += (u2 * cotAlpha + v2 * cotBeta) / 8;
            }
            return area;
        }

        /// <summary>
        /// Computes the equally weighted normal arround the specified vertex
        /// </summary>
        /// <returns>The normal vector.</returns>
        /// <param name="vertex">Vertex.</param>
        public static Vector3d VertexNormalEquallyWeighted(HE_Vertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (HE_Face f in vertex.adjacentFaces()) n += FaceNormal(f);

            return n.Unit();
        }

        public static Vector3d VertexNormalAreaWeighted(HE_Vertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach(HE_Face f in vertex.adjacentFaces())
            {
                Vector3d normal = FaceNormal(f);
                double area = Area(f);

                n += (normal * area);
            }
            return n.Unit();
        }

        public static Vector3d VertexNormalAngleWeighted(HE_Vertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (HE_Corner c in vertex.adjacentCorners())
            {
                Vector3d normal = FaceNormal(c.HalfEdge.Face);
                double angle = Angle(c);

                n += (normal * angle);
            }
            return n.Unit();
        }

        public static Vector3d VertexNormalGaussCurvature(HE_Vertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (HE_HalfEdge hE in vertex.adjacentHalfEdges())
            {
                double weight = 0.5 * DihedralAngle(hE) / Length(hE.Edge);
                n -= (Vector(hE) * weight);
            }
            return n.Unit();
        }

        public static Vector3d VertexNormalMeanCurvature(HE_Vertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (HE_HalfEdge hE in vertex.adjacentHalfEdges())
            {
                double weight = 0.5 * Cotan(hE) + Cotan(hE.Twin);
                n -= (Vector(hE) * weight);
            }
            return n.Unit();
        }

        public static Vector3d VertexNormalSphereInscribed(HE_Vertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (HE_Corner c in vertex.adjacentCorners())
            {
                Vector3d u = Vector(c.HalfEdge.Prev);
                Vector3d v = -Vector(c.HalfEdge.Next);

                n += ((u.Cross(v) / (u.Norm2 * v.Norm2)));
            }
            return n.Unit();
        }

        public static double AngleDefect(HE_Vertex vertex)
        {
            double angleSum = 0.0;
            foreach (HE_Corner c in vertex.adjacentCorners()) angleSum += Angle(c);
            if (vertex.OnBoundary()) angleSum = Math.PI - angleSum;

            return vertex.OnBoundary()  ? Math.PI - angleSum: 2 * Math.PI - angleSum;
        }

        public static double scalarGaussCurvature(HE_Vertex vertex)
        {
            return AngleDefect(vertex);
        }

        public static double scalarMeanCurvature(HE_Vertex vertex)
        {
            double sum = 0.0;
            foreach (HE_HalfEdge hE in vertex.adjacentHalfEdges())
                sum += 0.5 * Length(hE.Edge) * DihedralAngle(hE);
            return sum;
        }

        public static double TotalAngleDefect(HE_Mesh Mesh)
        {
            double totalDefect = 0.0;
            foreach (HE_Vertex v in Mesh.Vertices)
                totalDefect += AngleDefect(v);
            return totalDefect;
        }

        public static double[] PrincipalCurvatures(HE_Vertex vertex)
        {
            double A = CircumcentricDualarea(vertex);
            double H = scalarMeanCurvature(vertex) / A;
            double K = AngleDefect(vertex) / A;

            double discriminant = H * H - K;
            if (discriminant > 0) discriminant = Math.Sqrt(discriminant);
            else discriminant = 0;

            double k1 = H - discriminant;
            double k2 = H + discriminant;

            return new double[]{k1,k2};
        }

    }
}
