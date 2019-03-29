namespace AR_Lib.Geometry
{
    public class Point2d: BasePoint
    {
        /// <summary>
        /// Constructs an empty 2D point.
        /// </summary>
        public Point2d(): base() { }

        /// <summary>
        /// Constructs a new 2D point out of x and y coordinates
        /// </summary>
        /// <param name="x">X coordinate of the point</param>
        /// <param name="y">Y coordinate of the point</param>
        public Point2d(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }

        /// <summary>
        /// Constructs a new 2D point out of an existing point
        /// </summary>
        /// <param name="pt">A 2D point</param>
        public Point2d(Point2d pt)
        {
            this.X = pt.X;
            this.Y = pt.Y;
        }
    
    }
}