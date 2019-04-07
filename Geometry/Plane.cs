namespace AR_Lib.Geometry
{

    public class Plane
    {
        public Point3d Origin { get => _origin; set => _origin = value; }
        public Vector3d XAxis { get => _xAxis; set => _xAxis = value; }
        public Vector3d YAxis { get => _yAxis; set => _yAxis = value; }
        public Vector3d ZAxis { get => _zAxis; set => _zAxis = value; }

        private Point3d _origin;
        private Vector3d _xAxis;
        private Vector3d _yAxis;
        private Vector3d _zAxis;

        public Plane(Point3d origin, Vector3d xAxis, Vector3d yAxis, Vector3d zAxis)
        {
            _origin = origin;
            _xAxis = xAxis;
            _yAxis = yAxis;
            _zAxis = zAxis;
        }

        public static Plane WorldXYZ => new Plane(Point3d.WorldOrigin, Vector3d.WorldX, Vector3d.WorldY, Vector3d.WorldZ);

        //TODO: Add utility methods to Plane class  (flip Axis, relative coordinates...)
    }
}
