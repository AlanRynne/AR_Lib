using AR_Lib.Geometry;

namespace AR_Lib
{
    public static partial class Intersect3D
    {
        //INFO: IS prefix stands for Intersection Status

        //INFO: IR prefix stands for Intersection Result
        public enum ISLinePlane
        {
            NoIntersection,
            Point,
            OnPlane
        }

        public enum ISRayFacePerimeter
        {
            NoIntersection,
            Point,
            Error
        }

        public enum ISLineLine
        {
            NoIntersection,
            Point,
            Error
        }

        public struct IRLineLine
        {
            public double Distance;
            public double tA;
            public double tB;
            public Point3d pointA;
            public Point3d pointB;
        }

    }
}