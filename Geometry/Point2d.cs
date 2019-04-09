namespace AR_Lib.Geometry
{
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
        public Point2d(double x, double y) : base(x, y, 0)
        {
        }

        /// <summary>
        /// Constructs a new 2D point out of an existing point
        /// </summary>
        /// <param name="pt">A 2D point</param>
        public Point2d(Point2d pt)
        {
            this.x = pt.X;
            this.y = pt.Y;
            this.z = 0;
        }

        // Overrided methods

        public override string ToString()
        {
            return "Point2d{ " + this.X + "; " + this.Y + "}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
