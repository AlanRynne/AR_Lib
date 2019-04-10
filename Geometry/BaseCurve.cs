
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using AR_Lib.Geometry;

namespace AR_Lib.Geometry
{

    // Excemptions
    public class InvalidCurveException : Exception
    {
        public InvalidCurveException()
        {
        }

        public InvalidCurveException(string message) : base(message)
        {
        }

        public InvalidCurveException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }

    public abstract class BaseCurve
    {
        // Public properties
        public Point3d StartPoint;
        public Point3d EndPoint;
        public Point3d T0;
        public Point3d T1;

        protected Point3d _startPoint;
        protected Point3d _endPoint;
        protected double _t0;
        protected double _t1;
        public bool IsValid => _isValid;
        public double Length => ComputeLength();

        // Private fields
        protected bool _isValid;

        protected BaseCurve()
        {
            _isValid = false;
        }


        // Abstract methods
        public abstract Point3d PointAt(double t);
        public abstract Vector3d TangentAt(double t);
        public abstract Vector3d NormalAt(double t);
        public abstract Vector3d BinormalAt(double t);
        public abstract Plane FrameAt(double t);
        public abstract void CheckValidity();
        protected abstract double ComputeLength();

    }

}