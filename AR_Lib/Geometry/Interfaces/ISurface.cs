namespace AR_Lib.Geometry.Interfaces
{
    public interface ISurface
    {
        Point3d PointAt(double u, double v);
        Vector3d NormalAt(double u, double v);
        Plane FrameAt(double u, double v);
    }
}