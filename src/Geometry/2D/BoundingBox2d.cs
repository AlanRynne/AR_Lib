using AR_Lib.Collections;

namespace AR_Lib.Geometry
{
    public class BoundingBox2d
    {
        private Interval xDomain;
        private Interval yDomain;

        public Interval XDomain { get => xDomain; set => xDomain = value; }
        public Interval YDomain { get => yDomain; set => yDomain = value; }

        public Point2d BottomLeft => new Point2d(xDomain.Start, yDomain.Start);
        public Point2d BottomRight => new Point2d(xDomain.End, yDomain.Start);
        public Point2d TopLeft => new Point2d(xDomain.Start, yDomain.End);
        public Point2d TopRight => new Point2d(xDomain.End, yDomain.End);

        public BoundingBox2d(Point2d bottomLeftCorner, Point2d topRightCorner)
        {
            this.xDomain = new Interval(bottomLeftCorner.X, topRightCorner.X);
            this.yDomain = new Interval(bottomLeftCorner.Y, topRightCorner.Y);
        }

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