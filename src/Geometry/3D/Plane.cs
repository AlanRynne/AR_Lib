using System;

namespace AR_Lib.Geometry
{
    //TODO: Must ensure that vectors are always perpendicular to each other

    /// <summary>
    /// Represents a 3-Dimensional plane
    /// </summary>
    public class Plane
    {
        #region Fields
        private Point3d _origin;
        private Vector3d _xAxis;
        private Vector3d _yAxis;
        private Vector3d _zAxis;

        #endregion

        #region Properties

        /// Gets or sets the plane origin
        public Point3d Origin
        {
            get => _origin;
            set
            {
                _origin = value;
            }
        }

        /// Gets or sets the plane X axis
        public Vector3d XAxis
        {
            get => _xAxis;
            set
            {
                _xAxis = value;
            }
        }

        /// Gets or sets the plane Y axis
        public Vector3d YAxis
        {
            get => _yAxis;
            set
            {
                _yAxis = value;
            }
        }

        /// Gets or sets the plane Z axis
        public Vector3d ZAxis
        {
            get => _zAxis;
            set
            {
                _zAxis = value;
            }
        }

        /// Plane with axis' UnitX and UnitY.
        public static Plane WorldXY => new Plane(Point3d.WorldOrigin, Vector3d.UnitX, Vector3d.UnitY);

        /// Plane with axis' UnitX and UnitZ.
        public static Plane WorldXZ => new Plane(Point3d.WorldOrigin, Vector3d.UnitX, Vector3d.UnitZ);

        /// Plane with axis' UnitY and UnitZ.
        public static Plane WorldYZ => new Plane(Point3d.WorldOrigin, Vector3d.UnitY, Vector3d.UnitZ);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new plane given another Plane instance.
        /// Basically it makes a shallow copy. If you need a deep copy, use Clone() method.
        /// </summary>
        /// <param name="plane">Plane to copy values from.</param>
        public Plane(Plane plane) : this(plane.Origin, plane.XAxis, plane.YAxis, plane.ZAxis) { }

        /// <summary>
        /// Constructs an XY Plane with it's origin at the specified point.
        /// </summary>
        /// <param name="origin">Point to act as origin.</param>
        public Plane(Point3d origin) : this(origin, Vector3d.UnitX, Vector3d.UnitY) { }

        /// <summary>
        /// Constructs a new Plane instance given a point and two vectors. 
        /// Vectors do not necessarily have to be perpendicular.
        /// Will throw an error if vectors are parallel or close to parallel.
        /// </summary>
        /// <param name="origin">An origin point</param>
        /// <param name="xAxis">Vector to act as X axis.</param>
        /// <param name="yAxis">Vector to act as Y axis.</param>
        public Plane(Point3d origin, Vector3d xAxis, Vector3d yAxis) : this(origin, xAxis, yAxis, xAxis.Cross(yAxis)) { }

        /// <summary>
        /// Constructs a new Plane instance given a point and three vectors. 
        /// Will throw an error if vectors are not perpendicular to each other.
        /// </summary>
        /// <param name="origin">An origin point</param>
        /// <param name="xAxis">Vector to act as X axis.</param>
        /// <param name="yAxis">Vector to act as Y axis.</param>
        /// <param name="zAxis">Vector to act as Z axis.</param>
        public Plane(Point3d origin, Vector3d xAxis, Vector3d yAxis, Vector3d zAxis)
        {
            _origin = origin;
            _xAxis = xAxis;
            _yAxis = yAxis;
            _zAxis = zAxis;
        }

        /// <summary>
        /// Constructs a Plane instance given 3 non co-linear points.
        /// </summary>
        /// <param name="ptA">First point. Will be considered the plane origin</param>
        /// <param name="ptB">Second point. Marks the X axis direction of the plane.</param>
        /// <param name="ptC">Third point. Roughly points the direction of the Y axis</param>
        public Plane(Point3d ptA, Point3d ptB, Point3d ptC)
        {
            Vector3d tempX = ptB - ptA;
            Vector3d tempY = ptC - ptA;
            tempX.Unitize();
            tempY.Unitize();

            Vector3d normal = tempX.Cross(tempY);
            double colinearCheck = Math.Abs(1 - tempY.Dot(tempX));
            // Ensure points are not co-linear
            if (tempY.Dot(tempX) == 1)
                throw new System.Exception("Cannot create plane out of co-linear points.");

            _origin = ptA;
            _xAxis = tempX;
            _yAxis = normal.Cross(_xAxis);
            _zAxis = normal;


        }

        /// <summary>
        /// Constructs a new plane given its equation Ax + By + Cz + D = 0
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        public Plane(double A, double B, double C, double D)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public methods

        //TODO: Add utility methods to Plane class  (flip Axis, relative coordinates...)

        /// <summary>
        /// Flips the plane by interchanging the X and Y axis and negating the Z axis.
        /// </summary>
        public void Flip()
        {
            Vector3d temp = _yAxis;
            _yAxis = _xAxis;
            _xAxis = temp;
            _zAxis = -_zAxis;
        }

        /// <summary>
        /// Computes the point at the specified Plane parameters
        /// </summary>
        /// <param name="u">U coordinate</param>
        /// <param name="v">V coordinate</param>
        public Point3d PointAt(double u, double v) => PointAt(u, v, 0);
        public Point3d PointAt(double u, double v, double w) => _origin + (u * _xAxis + v * _yAxis + w * _zAxis);
        public Point3d RemapToPlaneSpace(Point3d point)
        {
            Vector3d vec = point - _origin;
            double u = vec.Dot(_xAxis);
            double v = vec.Dot(_yAxis);
            double w = vec.Dot(_zAxis);

            return new Point3d(u, v, w);
        }
        public Point3d RemapToWorldXYSpace(Point3d point) => _origin + point.X * _xAxis + point.Y * _yAxis + point.Z * _zAxis;
        public Point3d ClosestPoint(Point3d point)
        {
            Vector3d vec = point - _origin;
            double u = vec.Dot(_xAxis);
            double v = vec.Dot(_yAxis);

            return PointAt(u, v);
        }
        public double DistanceTo(Point3d point) => ((Vector3d)(point - _origin)).Dot(_zAxis);
        public double[] GetPlaneEquation()
        {
            throw new NotImplementedException();
        }
        public Plane Clone() => new Plane(new Point3d(_origin), new Vector3d(_xAxis), new Vector3d(_yAxis), new Vector3d(_zAxis));

        public override string ToString() => base.ToString();

        #endregion

    }
}
