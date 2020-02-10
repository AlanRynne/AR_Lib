using System;
using System.Collections.Generic;
using AR_Lib.HalfEdgeMesh;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents the geometry of a HE_Mesh such as positions at vertices.
    /// </summary>
    public static class MeshGeometry
    {
        /// <summary>
        /// Calculate the vector of a specified half-edge.
        /// </summary>
        /// <returns>The half-edge vector.</returns>
        /// <param name="halfEdge">Half edge.</param>
        public static Vector3d Vector(MeshHalfEdge halfEdge) => halfEdge.Vertex - halfEdge.Next.Vertex;

        /// <summary>
        /// Calculates the length of the specified edge.
        /// </summary>
        /// <returns>The length.</returns>
        /// <param name="edge">Edge.</param>
        public static double Length(MeshEdge edge) => Vector(edge.HalfEdge).Length;

        /// <summary>
        /// Calculates the midpoint of the specifiec edge.
        /// </summary>
        /// <returns>The point.</returns>
        /// <param name="edge">Edge.</param>
        public static Point3d MidPoint(MeshEdge edge)
        {
            MeshHalfEdge halfEdge = edge.HalfEdge;
            Point3d a = halfEdge.Vertex;
            Point3d b = halfEdge.Twin.Vertex;
            return (a + b) / 2;
        }

        /// <summary>
        /// Calculates the mean edge length of the mesh.
        /// </summary>
        /// <returns>The mean edge length of the mesh.</returns>
        public static double MeanEdgeLength(Mesh mesh)
        {
            double sum = 0.0;
            foreach (MeshEdge e in mesh.Edges)
                sum += Length(e);
            return sum / mesh.Edges.Count;
        }

        /// <summary>
        /// Computes the area of the specified face.
        /// </summary>
        /// <returns>The face area.</returns>
        /// <param name="face">Face.</param>
        public static double Area(MeshFace face)
        {
            if (face.IsBoundaryLoop())
                return 0.0;

            Vector3d u = Vector(face.HalfEdge);
            Vector3d v = -Vector(face.HalfEdge.Prev);
            return 0.5 * u.Cross(v).Length;
        }

        /// <summary>
        /// Computes the total area of the mesh.
        /// </summary>
        /// <returns>The mesh area.</returns>
        public static double TotalArea(Mesh mesh)
        {
            double sum = 0.0;
            foreach (MeshFace f in mesh.Faces)
                sum += Area(f);
            return sum;
        }

        /// <summary>
        /// Compute the normal vector of the specified face.
        /// </summary>
        /// <returns>The normal.</returns>
        /// <param name="face">Face.</param>
        public static Vector3d FaceNormal(MeshFace face)
        {
            if (face.IsBoundaryLoop())
                return null;

            Vector3d u = Vector(face.HalfEdge);
            Vector3d v = -Vector(face.HalfEdge.Prev);
            return u.Cross(v).Unit();
        }

        /// <summary>
        /// Compute the centroid of the specified face.
        /// </summary>
        /// <returns>The centroid.</returns>
        /// <param name="face">Face.</param>
        public static Point3d Centroid(MeshFace face)
        {
            MeshHalfEdge hE = face.HalfEdge;
            Point3d a = hE.Vertex;
            Point3d b = hE.Next.Vertex;
            Point3d c = hE.Prev.Vertex;

            if (face.IsBoundaryLoop())
                return (a + b) / 2;

            return (a + b + c) / 3;
        }

        /// <summary>
        /// Compute the circumcenter the specified face.
        /// </summary>
        /// <returns>The circumcenter.</returns>
        /// <param name="face">Face.</param>
        public static Point3d Circumcenter(MeshFace face)
        {
            MeshHalfEdge hE = face.HalfEdge;

            Point3d a = hE.Vertex;
            Point3d b = hE.Next.Vertex;
            Point3d c = hE.Prev.Vertex;

            if (face.IsBoundaryLoop())
                return (a + b) / 2;

            Vector3d ac = c - a;
            Vector3d ab = b - a;
            Vector3d w = ab.Cross(ac);

            Vector3d u = w.Cross(ab) * ac.LengthSquared;
            Vector3d v = ac.Cross(w) * ab.LengthSquared;

            Point3d x = (Point3d)(u + v) / (2 * w.LengthSquared);

            return x + a;
        }

        /// <summary>
        /// Compute the orthonormal bases of the specified face.
        /// </summary>
        /// <returns>Array containing the 2 Vector3d.</returns>
        /// <param name="face">Face.</param>
        public static Vector3d[] OrthonormalBases(MeshFace face)
        {
            Vector3d e1 = Vector(face.HalfEdge).Unit();
            Vector3d normal = FaceNormal(face);
            Vector3d e2 = normal.Cross(e1);

            return new Vector3d[] { e1, e2 };
        }

        /// <summary>
        /// Compute the angle (in radians) at the specified corner.
        /// </summary>
        /// <returns>The angle (in radians).</returns>
        /// <param name="corner">Corner.</param>
        public static double Angle(MeshCorner corner)
        {
            Vector3d u = Vector(corner.HalfEdge.Prev).Unit();
            Vector3d v = -Vector(corner.HalfEdge.Next).Unit();

            return Math.Acos(Math.Max(-1, Math.Min(1.0, u.Dot(v))));
        }

        /// <summary>
        /// Computes the cotangent of the angle opposite to a halfedge.
        /// </summary>
        /// <returns>The cotan.</returns>
        /// <param name="hE">H e.</param>
        public static double Cotan(MeshHalfEdge hE)
        {
            if (hE.OnBoundary)
                return 0.0;

            Vector3d u = Vector(hE.Prev);
            Vector3d v = -Vector(hE.Next);

            return u.Dot(v) / u.Cross(v).Length;
        }

        /// <summary>
        /// Computes the signed angle (in radians) between the faces adjacent to the specified half-edge.
        /// </summary>
        /// <returns>The angle (in radians) between faces.</returns>
        /// <param name="hE">H e.</param>
        public static double DihedralAngle(MeshHalfEdge hE)
        {
            if (hE.OnBoundary || hE.Twin.OnBoundary)
                return 0.0;

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
        public static double BarycentricDualArea(MeshVertex vertex)
        {
            double area = 0.0;
            foreach (MeshFace f in vertex.AdjacentFaces())
                area += Area(f);
            return area;
        }

        /// <summary>
        /// Computes the circumcentric dual area around a given mesh vertex.
        /// </summary>
        /// <returns>The dualarea.</returns>
        /// <param name="vertex">Vertex.</param>
        public static double CircumcentricDualarea(MeshVertex vertex)
        {
            double area = 0.0;
            foreach (MeshHalfEdge hE in vertex.AdjacentHalfEdges())
            {
                double u2 = Vector(hE.Prev).LengthSquared;
                double v2 = Vector(hE).LengthSquared;
                double cotAlpha = Cotan(hE.Prev);
                double cotBeta = Cotan(hE);

                area += ((u2 * cotAlpha) + (v2 * cotBeta)) / 8;
            }

            return area;
        }

        /// <summary>
        /// Computes the equally weighted normal arround the specified vertex.
        /// </summary>
        /// <returns>The normal vector at that vertex.</returns>
        /// <param name="vertex">Vertex.</param>
        public static Vector3d VertexNormalEquallyWeighted(MeshVertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (MeshFace f in vertex.AdjacentFaces())
                n += FaceNormal(f);

            return n.Unit();
        }

        /// <summary>
        /// Computes the area weighted normal arround the specified vertex.
        /// </summary>
        /// <returns>The normal vector at that vertex.</returns>
        /// <param name="vertex">Vertex.</param>
        public static Vector3d VertexNormalAreaWeighted(MeshVertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (MeshFace f in vertex.AdjacentFaces())
            {
                Vector3d normal = FaceNormal(f);
                double area = Area(f);

                n += normal * area;
            }

            return n.Unit();
        }

        /// <summary>
        /// Computes the angle weighted normal arround the specified vertex.
        /// </summary>
        /// <returns>The normal vector at that vertex.</returns>
        /// <param name="vertex">Vertex.</param>
        public static Vector3d VertexNormalAngleWeighted(MeshVertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (MeshCorner c in vertex.AdjacentCorners())
            {
                Vector3d normal = FaceNormal(c.HalfEdge.Face);
                double angle = Angle(c);

                n += normal * angle;
            }

            return n.Unit();
        }

        /// <summary>
        /// Computes the gauss curvature weighted normal arround the specified vertex.
        /// </summary>
        /// <returns>The normal vector at that vertex.</returns>
        /// <param name="vertex">Vertex.</param>
        public static Vector3d VertexNormalGaussCurvature(MeshVertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (MeshHalfEdge hE in vertex.AdjacentHalfEdges())
            {
                double weight = 0.5 * DihedralAngle(hE) / Length(hE.Edge);
                n -= Vector(hE) * weight;
            }

            return n.Unit();
        }

        /// <summary>
        /// Computes the mean curvature weighted normal arround the specified vertex.
        /// </summary>
        /// <returns>The normal vector at that vertex.</returns>
        /// <param name="vertex">Vertex.</param>
        public static Vector3d VertexNormalMeanCurvature(MeshVertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (MeshHalfEdge hE in vertex.AdjacentHalfEdges())
            {
                double weight = (0.5 * Cotan(hE)) + Cotan(hE.Twin);
                n -= Vector(hE) * weight;
            }

            return n.Unit();
        }

        /// <summary>
        /// Computes the sphere inscribed normal arround the specified vertex.
        /// </summary>
        /// <returns>The normal vector at that vertex.</returns>
        /// <param name="vertex">Vertex.</param>
        public static Vector3d VertexNormalSphereInscribed(MeshVertex vertex)
        {
            Vector3d n = new Vector3d();
            foreach (MeshCorner c in vertex.AdjacentCorners())
            {
                Vector3d u = Vector(c.HalfEdge.Prev);
                Vector3d v = -Vector(c.HalfEdge.Next);

                n += u.Cross(v) / (u.LengthSquared * v.LengthSquared);
            }

            return n.Unit();
        }

        /// <summary>
        /// Computes the angle defect at the given vertex.
        /// </summary>
        /// <param name="vertex">Vertex to compute angle defect.</param>
        /// <returns>Number representing the deviation of the current vertex from $2\PI$.</returns>
        public static double AngleDefect(MeshVertex vertex)
        {
            double angleSum = 0.0;
            foreach (MeshCorner c in vertex.AdjacentCorners())
                angleSum += Angle(c);

            // if (vertex.OnBoundary()) angleSum = Math.PI - angleSum;
            return vertex.OnBoundary() ? Math.PI - angleSum : (2 * Math.PI) - angleSum;
        }

        /// <summary>
        /// Compute the Gaussian curvature at the given vertex.
        /// </summary>
        /// <param name="vertex">Vertex to compute Gaussian curvature.</param>
        /// <returns>Number representing the gaussian curvature at that vertex.</returns>
        public static double ScalarGaussCurvature(MeshVertex vertex)
        {
            return AngleDefect(vertex) / MeshGeometry.CircumcentricDualarea(vertex);
        }

        /// <summary>
        /// Compute the Mean curvature at the given vertex.
        /// </summary>
        /// <param name="vertex">Vertex to compute Mean curvature.</param>
        /// <returns>Number representing the Mean curvature at that vertex.</returns>
        public static double ScalarMeanCurvature(MeshVertex vertex)
        {
            double sum = 0.0;
            foreach (MeshHalfEdge hE in vertex.AdjacentHalfEdges())
                sum += 0.5 * Length(hE.Edge) * DihedralAngle(hE);
            return sum;
        }

        /// <summary>
        /// Compute the total angle defect of the mesh.
        /// </summary>
        /// <param name="mesh">Mesh to compute angle defect.</param>
        /// <returns>Returns the total angle defect as a scalar value.</returns>
        public static double TotalAngleDefect(Mesh mesh)
        {
            double totalDefect = 0.0;
            foreach (MeshVertex v in mesh.Vertices)
                totalDefect += AngleDefect(v);
            return totalDefect;
        }

        /// <summary>
        /// Compute the principal curvature scalar values at a given vertes.
        /// </summary>
        /// <param name="vertex">Vertex to compute the curvature.</param>
        /// <returns>Returns an array of 2 values {k1, k2}.</returns>
        public static double[] PrincipalCurvatures(MeshVertex vertex)
        {
            double a = CircumcentricDualarea(vertex);
            double h = ScalarMeanCurvature(vertex) / a;
            double k = AngleDefect(vertex) / a;

            double discriminant = (h * h) - k;
            if (discriminant > 0)
                discriminant = Math.Sqrt(discriminant);
            else
                discriminant = 0;

            double k1 = h - discriminant;
            double k2 = h + discriminant;

            return new double[] { k1, k2 };
        }
    }
}
