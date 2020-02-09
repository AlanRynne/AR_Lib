using System;
using System.Collections.Generic;

namespace AR_Lib.Geometry
{

    public class Polyline : BaseCurve
    {
        private List<Point3d> knots;
        private List<Line> segments;
        private bool segmentsNeedUpdate;

        public List<Line> Segments
        {
            get
            {
                if (segmentsNeedUpdate)
                    RebuildSegments();
                return segments;
            }
        }
        public bool IsClosed => knots[0] == knots[knots.Count - 1];
        public bool IsUnset { get; }

        #region Constructors

        public Polyline()
        {
            knots = new List<Point3d>();
            segments = new List<Line>();
            IsUnset = true;
            segmentsNeedUpdate = false;
        }
        public Polyline(List<Point3d> knots)
        {
            this.knots = knots;
            segments = new List<Line>();
            segmentsNeedUpdate = true;
            IsUnset = false;
        }

        #endregion

        #region Polyline specific methods

        public void AddKnot(Point3d knot)
        {
            knots.Add(knot); // Add knot to list
            segmentsNeedUpdate = true;
        }
        public void AddKnot(Point3d knot, int index)
        {
            knots.Insert(index, knot); // Add knot to list
            segmentsNeedUpdate = true;

        }
        public void RemoveKnot(Point3d knot)
        {
            if (knots.Contains(knot))
            {
                knots.Remove(knot);
                segmentsNeedUpdate = true;
            }

        }

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
                // Assign parameter values
                l.T0 = t;
                t += l.Length;
                l.T1 = t;
                // Add segment to list.
                segments.Add(l);
            }
        }

        #endregion

        #region  Overriden Methods
        public override Vector3d BinormalAt(double t) => throw new NotImplementedException();
        public override Vector3d NormalAt(double t) => throw new NotImplementedException();
        public override Point3d PointAt(double t) => throw new NotImplementedException();
        public override Vector3d TangentAt(double t) => throw new NotImplementedException();
        public override Plane FrameAt(double t) => throw new NotImplementedException();
        protected override double ComputeLength()
        {
            double length = 0;
            segments.ForEach(segment => length += segment.Length);
            return length;
        }

        public override bool CheckValidity() => throw new NotImplementedException();
        #endregion

    }

}