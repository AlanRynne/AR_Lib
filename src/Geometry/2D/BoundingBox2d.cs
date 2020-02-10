using Paramdigma.Core.Collections;

namespace Paramdigma.Core.Geometry
{
    /// <summary>
    /// Represents a 2D bounding box.
    /// </summary>
    public class BoundingBox2d
    {
        /// <summary>
        /// Gets or sets the Domain in the X direction.
        /// </summary>
        /// <value></value>
        public Interval XDomain { get; set; }

        /// <summary>
        /// Gets or sets the Domain in the Y direction.
        /// </summary>
        /// <value></value>
        public Interval YDomain { get; set; }

        /// <summary>
        /// Gets the Bottom left corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d BottomLeft => new Point2d(this.XDomain.Start, this.YDomain.Start);

        /// <summary>
        /// Gets the Bottom right corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d BottomRight => new Point2d(this.XDomain.End, this.YDomain.Start);

        /// <summary>
        /// Gets the top left corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d TopLeft => new Point2d(this.XDomain.Start, this.YDomain.End);

        /// <summary>
        /// Gets the top right corner of the BBox.
        /// </summary>
        /// <returns></returns>
        public Point2d TopRight => new Point2d(this.XDomain.End, this.YDomain.End);

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox2d"/> class  from 2 points.
        /// </summary>
        /// <param name="bottomLeftCorner">Bottom left corner.</param>
        /// <param name="topRightCorner">Top right corner.</param>
        public BoundingBox2d(Point2d bottomLeftCorner, Point2d topRightCorner)
        {
            this.XDomain = new Interval(bottomLeftCorner.X, topRightCorner.X);
            this.YDomain = new Interval(bottomLeftCorner.Y, topRightCorner.Y);
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

            this.XDomain = new Interval(xMin, xMax);
            this.YDomain = new Interval(yMin, yMax);
        }
    }
}