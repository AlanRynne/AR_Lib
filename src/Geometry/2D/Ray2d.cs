namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents a 2-dimensional ray.
    /// </summary>
    public class Ray2d
    {
        /// <summary>
        /// Gets or sets the origin of the ray.
        /// </summary>
        /// <value>Origin point.</value>
        public Point2d Origin { get; set; }

        /// <summary>
        /// Gets or sets the direction of the ray as a unit vector.
        /// </summary>
        /// <value>Direction vector.</value>
        public Vector2d Direction { get; set; }
    }
}