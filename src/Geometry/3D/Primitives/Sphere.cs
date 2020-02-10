using AR_Lib.Collections;
using AR_Lib.Geometry.Interfaces;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents a spherical surface.
    /// </summary>
    public class Sphere : ISurface
    {
        /// <summary>
        /// Gets or sets the base plane of the sphere.
        /// </summary>
        /// <value><see cref="Plane"/>.</value>
        public Plane Plane { get; set; }

        /// <summary>
        /// Gets or sets the radius of the sphere.
        /// </summary>
        /// <value><see cref="double"/>.</value>
        public double Radius { get; set; }

        /// <inheritdoc/>
        public Interval DomainU => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Interval DomainV => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public double DistanceTo(Sphere sphere) => DistanceTo(sphere.Plane.Origin) - sphere.Radius;

        /// <inheritdoc/>
        public double DistanceTo(Point3d point) => Plane.DistanceTo(point) - Radius;

        /// <inheritdoc/>
        public Plane FrameAt(double u, double v) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Vector3d NormalAt(double u, double v) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Point3d PointAt(double u, double v) => throw new System.NotImplementedException();
    }
}