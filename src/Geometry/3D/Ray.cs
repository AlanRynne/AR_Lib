namespace AR_Lib.Geometry
{
    /// <summary>
    /// Infinite 3d ray starting at a point.
    /// </summary>
    public class Ray
    {

        /// <summary>
        /// Gets/sets the origin point of the ray.
        /// </summary>
        public Point3d Origin { get; set; }

        /// <summary>
        /// Gets/sets the direction vector of the ray.
        /// </summary>
        /// <value></value>
        public Vector3d Direction { get; set; }

        #region Constructors

        /// <summary>
        /// Initialize an Ray instance with origin and direction.
        /// </summary>
        /// <param name="origin">Point representing the origin of the ray.</param>
        /// <param name="direction">Vector representing the direction of the ray.</param>
        public Ray(Point3d origin, Vector3d direction)
        {
            Origin = origin;
            Direction = direction;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Computes a point in the ray at the given parameter.
        /// </summary>
        /// <param name="t">Parameter to obtain point.</param>
        /// <returns>Returns a point at the specified parameter of the Ray.</returns>
        public Point3d PointAt(double t)
        {
            return Origin + t * Direction;
        }

        #endregion
    }

}