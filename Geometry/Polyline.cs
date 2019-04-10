using System;
using System.Collections.Generic;

namespace AR_Lib.Geometry
{

    public class Polyline : BaseCurve
    {
        private List<Point3d> _knots;
        private List<Line> _segments;

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

    }

}