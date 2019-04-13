namespace AR_Lib.Geometry
{
    /// <summary>
    /// Infinite 3d ray starting at a point.
    /// </summary>
    public class Ray
    {
        private Point3d _origin;
        private Vector3d _direction;

        public Point3d Origin { get => _origin; set => _origin = value; }
        public Vector3d Direction { get => _direction; set => _direction = value; }

        #region Constructors

        public Ray(Point3d origin, Vector3d direction)
        {
            _origin = origin;
            _direction = direction;
        }

        #endregion

        #region Public methods

        public Point3d PointAt(double t)
        {
            return _origin + t * _direction;
        }

        #endregion
    }

}