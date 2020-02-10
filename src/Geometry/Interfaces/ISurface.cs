using Paramdigma.Core.Collections;

namespace Paramdigma.Core.Geometry.Interfaces
{
    /// <summary>
    /// Base interface that all surface interface must implement.
    /// </summary>
    public interface ISurface
    {
        /// <summary>
        /// Gets the domain in the U direction.
        /// </summary>
        /// <value><see cref="Interval"/>.</value>
        Interval DomainU { get; }

        /// <summary>
        /// Gets the domain in the V direction.
        /// </summary>
        /// <value><see cref="Interval"/>.</value>
        Interval DomainV { get; }

        /// <summary>
        /// Compute a point at the specified surface coordinates.
        /// </summary>
        /// <param name="u">U coordinate.</param>
        /// <param name="v">V coordinate.</param>
        /// <returns><see cref="Point3d"/>.</returns>
        Point3d PointAt(double u, double v);

        /// <summary>
        /// Compute the normal at the specified surface coordinates.
        /// </summary>
        /// <param name="u">U coordiante.</param>
        /// <param name="v">V coordinate.</param>
        /// <returns>Normal vector.</returns>
        Vector3d NormalAt(double u, double v);

        /// <summary>
        /// Compute the tangent plane at the specified surface coordinates.
        /// </summary>
        /// <param name="u">U coordiante.</param>
        /// <param name="v">V coordinate.</param>
        /// <returns>Tangent plane.</returns>
        Plane FrameAt(double u, double v);
    }
}