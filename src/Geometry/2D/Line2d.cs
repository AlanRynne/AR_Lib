using AR_Lib.Collections;

namespace AR_Lib.Geometry
{
    public class Line2d
    {
        private Point2d startPoint;
        private Point2d endPoint;
        private Interval domain;

        public Point2d StartPoint { get => startPoint; set => startPoint = value; }
        public Point2d EndPoint { get => endPoint; set => endPoint = value; }
        public Interval Domain { get => domain; set => domain = value; }
        public Vector2d Vector => this; // Implicit line to vector conversion (this property exists just for convenience and readability)
        public double Length => this.Vector.Length;


        public Line2d(Point2d startPoint, Point2d endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.domain = new Interval(0, Length);
        }
        public Line2d(Point2d startPoint, Vector2d direction) : this(startPoint, startPoint + direction) { }
        public Line2d(Point2d startPoint, Vector2d direction, double length) : this(startPoint, direction.Unit() * length) { }

        /// <summary>
        /// Computes if a given point is at the left, right or on the current line
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <returns>
        /// >0 for point left of the line
        /// =0 for point on the line
        /// bigger 0 for point right of the line
        /// </returns>
        public double IsLeft(Point2d point)
        {
            Vector2d v1 = this;
            Vector2d v2 = point - this.startPoint;
            return v1.PerpProduct(v2);
        }


        public static implicit operator Vector2d(Line2d line) => line.endPoint - line.startPoint;

    }
}