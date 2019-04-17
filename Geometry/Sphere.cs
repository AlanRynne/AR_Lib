namespace AR_Lib.Geometry
{
    public class Sphere
    {
        private Point3d _origin;
        private double _radius;

        public Point3d Origin { get => _origin; set => _origin = value; }
        public double Radius { get => _radius; set => _radius = value; }

        public double DistanceTo(Sphere sphere)
        {
            return this.DistanceTo(sphere.Origin) - sphere.Radius;
        }
        public double DistanceTo(Point3d point)
        {
            return _origin.DistanceTo(point) - _radius;
        }
    }

}