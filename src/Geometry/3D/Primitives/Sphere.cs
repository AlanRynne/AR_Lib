using Paramdigma.Core.Collections;
using Paramdigma.Core.Geometry.Interfaces;

namespace Paramdigma.Core.Geometry
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
        public Interval DomainU { get; set; }

        /// <inheritdoc/>
        public Interval DomainV { get; set; }

        /// <inheritdoc/>
        public double DistanceTo(Point3d point) => Plane.DistanceTo(point) - Radius;

        public Point3d ClosestPointTo(Point3d point) => Plane.Origin + ((point - Plane.Origin).Unit() * Radius);

        /// <inheritdoc/>
        public Plane FrameAt(double u, double v) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Vector3d NormalAt(double u, double v) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Point3d PointAt(double u, double v) => throw new System.NotImplementedException();
    }
}