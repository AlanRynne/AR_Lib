using System.Collections.Generic;

#pragma warning disable 1591

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Represents a generic curve. This class is abstract and all curve classes should inherit from it.
    /// </summary>
    public abstract class BaseCurve
    {
        // Public properties

        /// <summary>
        /// Gets or sets the curve's start point.
        /// </summary>
        public virtual Point3d StartPoint { get => startPoint; set => startPoint = value; }

        /// <summary>
        /// Gets or sets the curves end point.
        /// </summary>
        public virtual Point3d EndPoint { get => endPoint; set => endPoint = value; }

        /// <summary>
        /// Gets or sets the curves initial parameter.
        /// </summary>
        public virtual double T0 { get => t0; set => t0 = value; }

        /// <summary>
        /// Gets or sets the curves final parameter.
        /// </summary>
        public virtual double T1 { get => t1; set => t1 = value; }

        /// <summary>
        /// Checks if the curve is valid.
        /// </summary>
        /// <returns>True if Valid.</returns>
        public virtual bool IsValid => CheckValidity();

        protected Point3d startPoint;

        protected Point3d endPoint;

        protected double t0;

        protected double t1;

        public double Length => ComputeLength();

        // Private fields
        protected bool isValid;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCurve"/> class.
        /// </summary>
        protected BaseCurve()
        {
            isValid = false;
        }

        /// <summary>
        /// Compute a point along the curve at a specified parameter.
        /// </summary>
        /// <param name="t">Parameter.</param>
        /// <returns>Point at the parameter specified.</returns>
        public abstract Point3d PointAt(double t);

        /// <summary>
        /// Compute the tangent vector along the curve at a specified parameter.
        /// </summary>
        /// <param name="t">Parameter.</param>
        /// <returns>Tangent vector at the parameter specified.</returns>
        public abstract Vector3d TangentAt(double t);

        /// <summary>
        /// Compute normal vector along the curve at a specified parameter.
        /// </summary>
        /// <param name="t">Parameter.</param>
        /// <returns>Normal vector at the parameter specified.</returns>
        public abstract Vector3d NormalAt(double t);

        /// <summary>
        /// Compute a binormal vector along the curve at a specified parameter.
        /// </summary>
        /// <param name="t">Parameter.</param>
        /// <returns>Binormal vector at the parameter specified.</returns>
        public abstract Vector3d BinormalAt(double t);

        /// <summary>
        /// Compute the perpendicular frame along the curve at a specified parameter.
        /// </summary>
        /// <param name="t">Parameter.</param>
        /// <returns>Perpendicular plane at the parameter specified.</returns>
        public abstract Plane FrameAt(double t);

        /// <summary>
        /// Checks the validity of the curve.
        /// </summary>
        /// <returns>True if valid.</returns>
        public abstract bool CheckValidity();

        /// <summary>
        /// Computes the length of the curve.
        /// </summary>
        /// <returns>Length as number.</returns>
        protected abstract double ComputeLength();
    }
}