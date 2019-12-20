namespace AR_Lib.Geometry.Interfaces
{
    public interface ICurve
    {
        Point3d PointAt(double t);
        Vector3d TangentAt(double t);
        Vector3d NormalAt(double t);
        Vector3d BinormalAt(double t);
        Plane FrameAt(double t);
    }
}