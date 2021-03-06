using Paramdigma.Core.Collections;
using Paramdigma.Core.Geometry.Interfaces;

namespace Paramdigma.Core.Geometry
{
    /// <summary>
    /// Represents a cylindrical surface.
    /// </summary>
    public class Cylinder : ISurface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cylinder"/> class from it's individual components.
        /// </summary>
        /// <param name="plane">The plane of the cylinder.</param>
        /// <param name="radius">The radius of the cylinder.</param>
        /// <param name="domain">The cylinder height range.</param>
        public Cylinder(Plane plane, double radius, Interval domain)
        {
            Plane = plane;
            Radius = radius;
            HeightRange = domain;
        }

        /// <summary>
        /// Gets or sets the base plane of the cylinder.
        /// </summary>
        /// <value><see cref="Plane"/>.</value>
        public Plane Plane { get; set; }

        /// <summary>
        /// Gets or sets the radius of the cylinder.
        /// </summary>
        /// <value><see cref="double"/>.</value>
        public double Radius { get; set; }

        /// <summary>
        /// Gets or sets the height range of the cylinder.
        /// </summary>
        /// <value><see cref="Interval"/>.</value>
        public Interval HeightRange { get; set; }

        /// <summary>
        /// Gets the cylinder height.
        /// </summary>
        public double Height => HeightRange.Length;

        /// <inheritdoc/>
        public Interval DomainU { get; set; }

        /// <inheritdoc/>
        public Interval DomainV { get; set; }

        /// <inheritdoc/>
        public Plane FrameAt(double u, double v)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Vector3d NormalAt(double u, double v)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Point3d PointAt(double u, double v)
        {
            throw new System.NotImplementedException();
        }
    }
}