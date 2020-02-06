using AR_Lib.Collections;

namespace AR_Lib.Geometry.Primitives
{
    public class Box
    {
        public Box(Plane plane, Interval domainX, Interval domainY, Interval domainZ)
        {
            Plane = plane;
            DomainX = domainX;
            DomainY = domainY;
            DomainZ = domainZ;
        }

        public Box(Point3d lower, Point3d upper)
        {
            this.Plane = Plane.WorldXY;
            this.DomainX = new Interval(lower.X, upper.X);
            this.DomainY = new Interval(lower.Y, upper.Y);
            this.DomainZ = new Interval(lower.Z, upper.Z);
        }

        public Plane Plane { get; set; }
        public Interval DomainX { get; set; }
        public Interval DomainY { get; set; }
        public Interval DomainZ { get; set; }

        public Point3d Min => new Point3d(DomainX.Start, DomainY.Start, DomainZ.Start);
        public Point3d Max => new Point3d(DomainX.End, DomainY.End, DomainZ.End);

        public Point3d Center => new Point3d(DomainX.RemapFromUnit(0.5), DomainY.RemapFromUnit(0.5), DomainZ.RemapFromUnit(0.5));

    }
}