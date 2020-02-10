using AR_Lib.Collections;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents a 2D bounding box.
    /// </summary>
    public class BoundingBox2d
    {
        private Interval xDomain;
        private Interval yDomain;

        /// <summary>
        /// Gets or sets the Domain in the X direction.
        /// </summary>
        /// <value></value>
        public Interval XDomain { get => xDomain; set => xDomain = value; }

        /// <summary>
        /// Gets or sets the Domain in the Y direction.
        /// </summary>
        /// <value></value>
        public Interval YDomain { get => yDomain; set => yDomain = value; }

        /// <summary>
        /// Gets the Bottom left corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d BottomLeft => new Point2d(xDomain.Start, yDomain.Start);

        /// <summary>
        /// Gets the Bottom right corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d BottomRight => new Point2d(xDomain.End, yDomain.Start);

        /// <summary>
        /// Gets the top left corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d TopLeft => new Point2d(xDomain.Start, yDomain.End);

        /// <summary>
        /// Gets the top right corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d TopRight => new Point2d(xDomain.End, yDomain.End);

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox2d"/> class  from 2 points.
        /// </summary>
        /// <param name="bottomLeftCorner">Bottom left corner.</param>
        /// <param name="topRightCorner">Top right corner.</param>
        public BoundingBox2d(Point2d bottomLeftCorner, Point2d topRightCorner)
        {
            this.xDomain = new Interval(bottomLeftCorner.X, topRightCorner.X);
            this.yDomain = new Interval(bottomLeftCorner.Y, topRightCorner.Y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox2d"/> class from a polyline.
        /// </summary>
        /// <param name="polyline">Polyline.</param>
        public BoundingBox2d(Polyline2d polyline)
        {
            double xMin = polyline.Vertices[0].X;
            double yMin = polyline.Vertices[0].Y;
            double xMax = polyline.Vertices[0].X;
            double yMax = polyline.Vertices[0].Y;

            polyline.Vertices.ForEach(vertex =>
            {
                if (vertex.X < xMin)
                    xMin = vertex.X;
                if (vertex.X > xMax)
                    xMax = vertex.X;

                if (vertex.Y < yMin)
                    yMin = vertex.Y;
                if (vertex.X > yMax)
                    yMax = vertex.Y;
            });

            this.xDomain = new Interval(xMin, xMax);
            this.yDomain = new Interval(yMin, yMax);
        }
    }
}