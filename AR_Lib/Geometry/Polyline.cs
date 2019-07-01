using System;
using System.Collections.Generic;

namespace AR_Lib.Geometry
{

    public class Polyline : BaseCurve
    {
        private List<Point3d> _knots;
        private List<Line> _segments;

        public bool IsClosed => _knots[0] == _knots[_knots.Count - 1];

        #region Constructors

        public Polyline()
        {
            _knots = new List<Point3d>();
            _segments = new List<Line>();
        }
        public Polyline(List<Point3d> knots)
        {
            _knots = knots;
            RebuildSegments();
        }

        #endregion

        #region Polyline specific methods
        
        public void AddKnot(Point3d knot)
        {
            _knots.Add(knot); // Add knot to list
            _segments.Add(new Line(_knots[_knots.Count - 1], knot)); //Add the corresponding segment
        }
        public void AddKnot(Point3d knot, int index)
        {
            _knots.Insert(index, knot); // Add knot to list
            RebuildSegments();

        }
        public void RemoveKnot(Point3d knot)
        {
            if (_knots.Contains(knot))
            {
                _knots.Remove(knot);
                RebuildSegments();
            }

        }
        private void RebuildSegments()
        {
            _segments = new List<Line>(_knots.Count - 1);
            double t = 0;
            for (int i = 1; i < _knots.Count; i++)
            {
                Line l = new Line(_knots[i - 1], _knots[i]);
                // Assign parameter values
                l.T0 = t;
                t += l.Length;
                l.T1 = t;
                // Add segment to list.
                _segments.Add(l);
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
            _segments.ForEach(segment => length += segment.Length);
            return length;
        }

        public override void CheckValidity() => throw new NotImplementedException();
        #endregion

    }

}