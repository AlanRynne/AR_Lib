namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents a 3D Line.
    /// </summary>
    public class Line : BaseCurve
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class from two points.
        /// </summary>
        /// <param name="startPoint">Start point.</param>
        /// <param name="endPoint">End point.</param>
        public Line(Point3d startPoint, Point3d endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        /// <summary>
        /// Checks if line is valid.
        /// </summary>
        /// <returns>True if valid.</returns>
        public override bool CheckValidity() => this.Length >= Settings.Tolerance;

        /// <summary>
        /// Computes thepoint at the given parameter.
        /// </summary>
        /// <param name="t">Parameter of the point. Must be between 0 and 1.</param>
        /// <returns>Point at specified parameter.</returns>
        public override Point3d PointAt(double t) => startPoint + (t * (endPoint - startPoint));

        /// <summary>
        /// Computes the tangent at the given parameter.
        /// </summary>
        /// <param name="t">Parameter of the tangent. Must be between 0 and 1.</param>
        /// <returns>Tangent at specified parameter.</returns>
        public override Vector3d TangentAt(double t)
        {
            Vector3d tangent = endPoint - startPoint;
            tangent.Unitize();
            return tangent;
        }

        /// <summary>
        /// Computes the normal at the given parameter.
        /// </summary>
        /// <param name="t">Parameter of the normal vector. Must be between 0 and 1.</param>
        /// <returns>Normal vector at specified parameter.</returns>
        public override Vector3d NormalAt(double t)
        {
            Vector3d tangent = TangentAt(t);
            Vector3d v = new Vector3d();

            if (tangent.Dot(Vector3d.UnitZ) == 1)
                v = Vector3d.UnitX;
            else
                v = Vector3d.UnitZ;

            return tangent.Cross(v);
        }

        /// <summary>
        /// Computes the bi-normal vector at the given parameter.
        /// </summary>
        /// <param name="t">Parameter of the bi-normal vector. Must be between 0 and 1.</param>
        /// <returns>Bi-normal vector at specified parameter.</returns>
        public override Vector3d BinormalAt(double t) => Vector3d.CrossProduct(TangentAt(t), NormalAt(t));

        /// <summary>
        /// Computes the perpendicular frame at the given parameter.
        /// </summary>
        /// <param name="t">Parameter of the frame. Must be between 0 and 1.</param>
        /// <returns>Frame at specified parameter.</returns>
        public override Plane FrameAt(double t) => new Plane(PointAt(t), TangentAt(t), NormalAt(t), BinormalAt(t));

        /// <summary>
        /// Computes the length of the line.
        /// </summary>
        /// <returns>Line length.</returns>
        protected override double ComputeLength() => startPoint.DistanceTo(endPoint);
    }
}