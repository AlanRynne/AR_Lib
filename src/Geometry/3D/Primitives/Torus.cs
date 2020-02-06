using AR_Lib.Geometry.Interfaces;

namespace AR_Lib.Geometry.Primitives
{
    public class Torus : ISurface
    {
        public Torus(Plane plane, double majorRadius, double minorRadius)
        {
            Plane = plane;
            MajorRadius = majorRadius;
            MinorRadius = minorRadius;
        }

        public Plane Plane { get; set; }

        public double MajorRadius { get; set; }

        public double MinorRadius { get; set; }

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