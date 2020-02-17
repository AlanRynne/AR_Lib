using System;

namespace Paramdigma.Core.Geometry
{
    // TODO: Must ensure that vectors are always perpendicular to each other

    /// <summary>
    /// Represents a 3-Dimensional plane.
    /// </summary>
    public class Plane
    {
        /// <summary>
        /// Gets or sets the plane origin.
        /// </summary>
        /// <value></value>
        public Point3d Origin
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the plane X axis.
        /// </summary>
        /// <value></value>
        public Vector3d XAxis
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the plane Y axis.
        /// </summary>
        /// <value></value>
        public Vector3d YAxis
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the plane Z axis.
        /// </summary>
        /// <value></value>
        public Vector3d ZAxis
        {
            get; set;
        }

        /// <summary>
        /// Gets plane with axis' UnitX and UnitY.
        /// </summary>
        /// <returns></returns>
        public static Plane WorldXY => new Plane(Point3d.WorldOrigin, Vector3d.UnitX, Vector3d.UnitY);

        /// <summary>
        /// Gets plane with axis' UnitX and UnitZ.
        /// </summary>
        /// <returns></returns>
        public static Plane WorldXZ => new Plane(Point3d.WorldOrigin, Vector3d.UnitX, Vector3d.UnitZ);

        /// <summary>
        /// Gets plane with axis' UnitY and UnitZ.
        /// </summary>
        /// <returns></returns>
        public static Plane WorldYZ => new Plane(Point3d.WorldOrigin, Vector3d.UnitY, Vector3d.UnitZ);

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane"/> class.
        /// Basically it makes a shallow copy. If you need a deep copy, use Clone() method.
        /// </summary>
        /// <param name="plane">Plane to copy values from.</param>
        public Plane(Plane plane)
            : this(new Point3d(plane.Origin), new Vector3d(plane.XAxis), new Vector3d(plane.YAxis), new Vector3d(plane.ZAxis))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane"/> class given it's origin at the specified point.
        /// </summary>
        /// <param name="origin">Point to act as origin.</param>
        public Plane(Point3d origin)
        : this(origin, Vector3d.UnitX, Vector3d.UnitY)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane"/> class given a point and two vectors.
        /// Vectors do not necessarily have to be perpendicular.
        /// Will throw an error if vectors are parallel or close to parallel.
        /// </summary>
        /// <param name="origin">An origin point.</param>
        /// <param name="xAxis">Vector to act as X axis.</param>
        /// <param name="yAxis">Vector to act as Y axis.</param>
        public Plane(Point3d origin, Vector3d xAxis, Vector3d yAxis)
        : this(origin, xAxis, yAxis, xAxis.Cross(yAxis))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane"/> class given a point and three vectors.
        /// Will throw an error if vectors are not perpendicular to each other.
        /// </summary>
        /// <param name="origin">An origin point.</param>
        /// <param name="xAxis">Vector to act as X axis.</param>
        /// <param name="yAxis">Vector to act as Y axis.</param>
        /// <param name="zAxis">Vector to act as Z axis.</param>
        public Plane(Point3d origin, Vector3d xAxis, Vector3d yAxis, Vector3d zAxis)
        {
            this.Origin = origin;
            this.XAxis = xAxis;
            this.YAxis = yAxis;
            this.ZAxis = zAxis;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane"/> class given 3 non co-linear points.
        /// </summary>
        /// <param name="ptA">First point. Will be considered the plane origin.</param>
        /// <param name="ptB">Second point. Marks the X axis direction of the plane.</param>
        /// <param name="ptC">Third point. Roughly points the direction of the Y axis.</param>
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

            Origin = ptA;
            XAxis = tempX;
            YAxis = normal.Cross(XAxis);
            ZAxis = normal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane"/> class given its equation Ax + By + Cz + D = 0.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">B.</param>
        /// <param name="c">C.</param>
        /// <param name="d">D.</param>
        public Plane(double a, double b, double c, double d)
        {
            throw new NotImplementedException();
        }

        // TODO: Add utility methods to Plane class  (flip Axis, relative coordinates...)

        /// <summary>
        /// Flips the plane by interchanging the X and Y axis and negating the Z axis.
        /// </summary>
        public void Flip()
        {
            Vector3d temp = YAxis;
            YAxis = XAxis;
            XAxis = temp;
            ZAxis = -ZAxis;
        }

        /// <summary>
        /// Computes the point at the specified Plane parameters.
        /// </summary>
        /// <param name="u">U coordinate.</param>
        /// <param name="v">V coordinate.</param>
        /// <returns></returns>
        public Point3d PointAt(double u, double v) => PointAt(u, v, 0);

        /// <summary>
        /// Computes a 3D point in the coordinate space of this plane.
        /// </summary>
        /// <param name="u">Coordinate for the X axis.</param>
        /// <param name="v">Coordinate for the Y axis.</param>
        /// <param name="w">Coordinate for the Z axis.</param>
        /// <returns>Computed point.</returns>
        public Point3d PointAt(double u, double v, double w) => Origin + ((u * XAxis) + (v * YAxis) + (w * ZAxis));

        /// <summary>
        /// Remap a given point to this plane's coordinate system.
        /// </summary>
        /// <param name="point">Point to remap.</param>
        /// <returns>Point with relative coordinates to the plane.</returns>
        public Point3d RemapToPlaneSpace(Point3d point)
        {
            Vector3d vec = point - Origin;
            double u = vec.Dot(XAxis);
            double v = vec.Dot(YAxis);
            double w = vec.Dot(ZAxis);

            return new Point3d(u, v, w);
        }

        /// <summary>
        /// Remap a given point to the XY Plane coordiante system.
        /// </summary>
        /// <param name="point">Point to remap.</param>
        /// <returns>Point with relative coordinates to the plane.</returns>
        public Point3d RemapToWorldXYSpace(Point3d point) => Origin + (point.X * XAxis) + (point.Y * YAxis) + (point.Z * ZAxis);

        /// <summary>
        /// Project a point to the plane.
        /// </summary>
        /// <param name="point">Point to project.</param>
        /// <returns>Point projection.</returns>
        public Point3d ClosestPoint(Point3d point)
        {
            Vector3d vec = point - Origin;
            double u = vec.Dot(XAxis);
            double v = vec.Dot(YAxis);

            return PointAt(u, v);
        }

        /// <summary>
        /// Compute the distance from a point to the plane.
        /// </summary>
        /// <param name="point">Point to compute distance to.</param>
        /// <returns>Distance to point.</returns>
        public double DistanceTo(Point3d point) => ((Vector3d)(point - Origin)).Dot(ZAxis);

        /// <summary>
        /// Returns the parametric equation for this plane.
        /// </summary>
        /// <returns>List with equation values.</returns>
        public double[] GetPlaneEquation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs a deep copy of this plane.
        /// </summary>
        /// <returns>Plane clone.</returns>
        public Plane Clone() => new Plane(new Point3d(Origin), new Vector3d(XAxis), new Vector3d(YAxis), new Vector3d(ZAxis));

        /// <summary>
        /// Returns the string representation of the plane.
        /// </summary>
        /// <returns>Plane string.</returns>
        public override string ToString() => base.ToString();
    }
}
