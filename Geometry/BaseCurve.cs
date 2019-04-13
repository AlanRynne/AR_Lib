
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
        public virtual Point3d StartPoint { get => _startPoint; set => _startPoint = value; }
        public virtual Point3d EndPoint { get => _endPoint; set => _endPoint = value; }
        public virtual double T0 { get => _t0; set => _t0 = value; }
        public virtual double T1 { get => _t1; set => _t1 = value; }

        public virtual bool IsValid => _isValid;

        protected Point3d _startPoint;
        protected Point3d _endPoint;
        protected double _t0;
        protected double _t1;
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