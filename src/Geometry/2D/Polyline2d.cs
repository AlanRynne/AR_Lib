using System;
using System.Collections.Generic;
using AR_Lib.Collections;

namespace AR_Lib.Geometry
{
    public class Polyline2d
    {

        #region Fields

        private List<Point2d> vertices;
        private List<Line2d> segments;
        private Interval domain;
        private bool isClosed;
        private bool segmentsNeedUpdate;

        #endregion

        #region Constructor

        public Polyline2d(List<Point2d> vertices, bool closed)
        {
            this.vertices = vertices;
            this.IsClosed = closed; // Call the property (not the field), to have if add the first point at the end if necessary.
            RebuildSegments();
        }

        #endregion

        #region Properties

        public List<Point2d> Vertices
        {
            get
            {
                return vertices;
            }
            set
            {
                vertices = value;
                segmentsNeedUpdate = true;
            }
        }
        public List<Line2d> Segments
        {
            get
            {
                if (segmentsNeedUpdate)
                    RebuildSegments();
                return segments;
            }
        }
        public Interval Domain => domain;
        public BoundingBox2d BoundingBox => new BoundingBox2d(this);
        public double Length
        {
            get
            {
                double length = 0;
                Segments.ForEach(segment =>
                {
                    length += segment.Length;
                });
                return length;
            }
        }
        public bool IsClosed
        {
            get => isClosed;
            set
            {
                if (isClosed == value)
                    return; // Do nothing

                if (isClosed)
                {
                    vertices.RemoveAt(vertices.Count - 1);
                }
                else
                {
                    vertices.Add(vertices[0]);
                }
                isClosed = value;
                RebuildSegments();
            }
        }

        #endregion

        public double Area()
        {
            if (!isClosed)
                return 0; // Return 0 if polyline is not closed
            List<Point2d> V = vertices;
            int n = vertices.Count;
            double area = 0;
            int i, j, k;

            if (n < 3)
                return 0;  // a degenerate polygon

            for (i = 1, j = 2, k = 0; i < n; i++, j++, k++)
            {
                area += V[i].X * (V[j].Y - V[k].Y);
            }
            area += V[n].X * (V[1].Y - V[n - 1].Y);  // wrap-around term
            return area / 2.0;
        }

        /// <summary>
        /// Checks if the current polyline is CW or CCW.
        /// </summary>
        /// <returns>
        /// 
        /// TRUE if the polyline is CW.
        /// FALSE if the polyline is CCW.
        /// </returns>
        /// <exception cref="Exception">Throws an exception if the polyline is not closed or is degenerate.</exception>
        public bool IsClockwise()
        {
            if (!isClosed)
                throw new Exception("Cannot compute orientation in an Open polyline");
            // first find rightmost lowest vertex of the polygon
            int rmin = 0;
            double xmin = vertices[0].X;
            double ymin = vertices[0].Y;

            for (int i = 1; i < vertices.Count; i++)
            {
                if (vertices[i].Y > ymin)
                    continue;
                if (vertices[i].Y == ymin)
                {   // just as low
                    if (vertices[i].X < xmin)  // and to left
                        continue;
                }
                rmin = i;      // a new rightmost lowest vertex
                xmin = vertices[i].X;
                ymin = vertices[i].Y;
            }

            // test orientation at the rmin vertex
            // ccw <=> the edge leaving V[rmin] is left of the entering edge
            double result;
            if (rmin == 0)
                result = new Line2d(vertices[vertices.Count - 1], vertices[0]).IsLeft(vertices[1]);
            else
                result = new Line2d(vertices[rmin - 1], vertices[rmin]).IsLeft(vertices[rmin + 1]);

            if (result == 0)
                throw new Exception("Polyline is degenerate, cannot compute orientation.");
            return result < 0 ? true : false;
        }

        public void Reparametrize()
        {

            double maxParameter = domain.End;
            double ratio = 1 / domain.End;

            double currentParam = 0;

            segments.ForEach(segment =>
            {
                double nextParam = currentParam + segment.Domain.Domain * ratio;
                segment.Domain = new Interval(currentParam, nextParam);
                currentParam = nextParam;
            });

            this.domain = Interval.Unit;
        }

        private void RebuildSegments()
        {
            segments = new List<Line2d>();
            double currentParam = 0;
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                Point2d vertA = vertices[i];
                Point2d vertB = vertices[i + 1];
                Line2d line = BuildSegment(ref currentParam, vertA, vertB);
                segments.Add(line);
            }
            domain = new Interval(0, currentParam);

        }
        private Line2d BuildSegment(ref double currentParam, Point2d vertA, Point2d vertB)
        {
            Line2d line = new Line2d(vertA, vertB);
            double nextParam = currentParam + line.Length;
            line.Domain = new Interval(currentParam, nextParam);
            currentParam = nextParam;
            return line;
        }
    }
}