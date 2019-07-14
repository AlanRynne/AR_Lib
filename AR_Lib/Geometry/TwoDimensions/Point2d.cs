namespace AR_Lib.Geometry
{
    /// <summary>
    /// 2-Dimensional point
    /// </summary>
    public class Point2d : BasePoint
    {
        /// <summary>
        /// Getter only Z value for 2D point
        /// </summary>
        /// <value>Should always return 0</value>
        public new double Z { get { return z; } }

        /// <summary>
        /// Constructs an empty 2D point.
        /// </summary>
        public Point2d() : base() { }

        /// <summary>
        /// Constructs a new 2D point out of x and y coordinates
        /// </summary>
        /// <param name="x">X coordinate of the point</param>
        /// <param name="y">Y coordinate of the point</param>
        public Point2d(double x, double y) : base(x, y, 0) { }

        /// <summary>
        /// Constructs a new 2D point out of an existing point
        /// </summary>
        /// <param name="pt">A 2D point</param>
        public Point2d(Point2d pt) : base(pt.X, pt.Y, pt.Z) { }

        // Overrided methods

        /// <summary>
        /// String representation of a 2-dimensional point instance
        /// </summary>
        /// <returns>Returns string representation of this Point2d instance.</returns>
        public override string ToString()
        {
            return "Point2d{ " + this.X + "; " + this.Y + "}";
        }

        /// <summary>
        /// Compares a Point2d instance to the given objects.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>Returns true if object is equals, false if not.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// Gets the hash code for the corresponding Point2d instance.
        /// </summary>
        /// <returns>Returns an int representing the Point2d hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
