using AR_Lib.Collections;
using AR_Lib.Geometry.Interfaces;

namespace AR_Lib.Geometry
{
    public class Cylinder : ISurface
    {
        public Cylinder(Plane plane, double radius, Interval domain)
        {
            Plane = plane;
            Radius = radius;
            Domain = domain;
        }

        public Plane Plane { get; set; }
        public double Radius { get; set; }
        public Interval Domain { get; set; }

        public double Height => Domain.Length;

        public Plane FrameAt(double u, double v)
        {
            throw new System.NotImplementedException();
        }

        public Vector3d NormalAt(double u, double v)
        {
            throw new System.NotImplementedException();
        }

        public Point3d PointAt(double u, double v)
        {
            throw new System.NotImplementedException();
        }
    }
}