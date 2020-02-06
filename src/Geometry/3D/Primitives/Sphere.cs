using AR_Lib.Geometry.Interfaces;

namespace AR_Lib.Geometry
{
    public class Sphere : ISurface
    {
        public Point3d Origin { get; set; }
        public double Radius { get; set; }

        public double DistanceTo(Sphere sphere)
        {
            return DistanceTo(sphere.Origin) - sphere.Radius;
        }
        public double DistanceTo(Point3d point)
        {
            return Origin.DistanceTo(point) - Radius;
        }

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