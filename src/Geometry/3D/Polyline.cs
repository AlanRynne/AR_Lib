using System;
using System.Collections;
using System.Collections.Generic;

namespace Paramdigma.Core.Geometry
{
    /// <summary>
    /// Represents a polyline of 3-dimensional points.
    /// </summary>
    public class Polyline : BaseCurve, IEnumerable<Point3d>
    {
        private readonly List<Point3d> knots;
        private List<Line> segments;
        private bool segmentsNeedUpdate;

        /// <summary>
        /// Gets the segment lines of the polyline.
        /// </summary>
        /// <value><see cref="Line"/>.</value>
        public List<Line> Segments
        {
            get
            {
                if (segmentsNeedUpdate)
                    RebuildSegments();
                return segments;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the polyline is closed (first point == last point).
        /// </summary>
        public bool IsClosed => knots[0] == knots[^1];

        /// <summary>
        /// Gets a value indicating whether the polyline is unset.
        /// </summary>
        public bool IsUnset => knots.Count == 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline"/> class.
        /// </summary>
        public Polyline()
        {
            knots = new List<Point3d>();
            segments = new List<Line>();
            segmentsNeedUpdate = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline"/> class from a list of points.
        /// </summary>
        /// <param name="knots">List of points.</param>
        public Polyline(List<Point3d> knots)
        {
            this.knots = knots;
            segments = new List<Line>();
            segmentsNeedUpdate = true;
        }

        /// <summary>
        /// Add a new knot vertex at the end of the polyline.
        /// </summary>
        /// <param name="knot">Point to add.</param>
        public void AddKnot(Point3d knot)
        {
            knots.Add(knot); // Add knot to list
            segmentsNeedUpdate = true;
        }

        /// <summary>
        /// Add a new knot vertex at the specified index.
        /// </summary>
        /// <param name="knot">Point to add.</param>
        /// <param name="index">Location to add at.</param>
        public void AddKnot(Point3d knot, int index)
        {
            knots.Insert(index, knot); // Add knot to list
            segmentsNeedUpdate = true;
        }

        /// <summary>
        /// Delete a specific knot if it exists in the polyline.
        /// </summary>
        /// <param name="knot">Point to delete.</param>
        public void RemoveKnot(Point3d knot)
        {
            if (knots.Contains(knot))
            {
                knots.Remove(knot);
                segmentsNeedUpdate = true;
            }
        }

        /// <summary>
        /// Delete a knot at a specific index.
        /// </summary>
        /// <param name="index">Index to delete knot at.</param>
        public void RemoveKnot(int index)
        {
            if (IsUnset)
                throw new Exception("Cannot erase knot from an Unset polyline");
            if (index < 0 || index > segments.Count - 1)
                throw new IndexOutOfRangeException("Knot index must be within the Knot list count");
        }

        private void RebuildSegments()
        {
            segments = new List<Line>(knots.Count - 1);
            double t = 0;
            for (int i = 1; i < knots.Count; i++)
            {
                Line l = new Line(knots[i - 1], knots[i]);
                var t0 = t;
                t += l.Length;
                var t1 = t;
                l.Domain = new Collections.Interval(t0, t1);

                // Add segment to list.
                segments.Add(l);
            }
        }

        /// <inheritdoc/>
        public override Vector3d BinormalAt(double t) => throw new NotImplementedException();

        /// <inheritdoc/>
        public override Vector3d NormalAt(double t) => throw new NotImplementedException();

        /// <inheritdoc/>
        public override Point3d PointAt(double t) => throw new NotImplementedException();

        /// <inheritdoc/>
        public override Vector3d TangentAt(double t) => throw new NotImplementedException();

        /// <inheritdoc/>
        public override Plane FrameAt(double t) => throw new NotImplementedException();

        /// <inheritdoc/>
        protected override double ComputeLength()
        {
            double length = 0;
            segments.ForEach(segment => length += segment.Length);
            return length;
        }

        /// <inheritdoc/>
        public override bool CheckValidity() => throw new NotImplementedException();

        /// <inheritdoc/>
        public IEnumerator<Point3d> GetEnumerator() => ((IEnumerable<Point3d>)knots).GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Point3d>)knots).GetEnumerator();
    }
}