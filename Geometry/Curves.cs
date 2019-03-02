using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using AR_Lib.Geometry;

namespace AR_Lib.Curve
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
        public Point3d startPoint;
        public Point3d endPoint;
        public double T0;
        public double T1;
        public bool IsValid { get => isValid; }


        // Private fields
        private bool isValid;

        protected BaseCurve()
        {
            isValid = false;
        }


        // Abstract methods
        public abstract double Length { get; set; }
        public abstract Point3d PointAt(double t);
        public abstract Vector3d TangentAt(double t);
        public abstract Vector3d NormalAt(double t);
        public abstract Vector3d BinormalAt(double t);
        public abstract Plane TNBFrameAt(double t);

        public abstract void CheckValidity();

    }

    public class Line : BaseCurve
    {

        public override void CheckValidity()
        {
            // Check validity should change IsValid state depending on conditions
            // i.e.: a line with the same point at start and end.
            throw new System.NotImplementedException();
        }

        public override Point3d PointAt(double t) => endPoint + t * (endPoint - startPoint);

        public override Vector3d TangentAt(double t) => throw new NotImplementedException();

        public override Vector3d NormalAt(double t) => throw new NotImplementedException();

        public override Vector3d BinormalAt(double t) => Vector3d.CrossProduct(TangentAt(t), NormalAt(t));

        public override Plane TNBFrameAt(double t) => new Plane(PointAt(t),TangentAt(t), NormalAt(t),BinormalAt(t));

        public override double Length { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }

    public class Polyline : BaseCurve
    {
        public List<Point3d> knots;
        public List<Line> segments;

        public override Vector3d BinormalAt(double t) => throw new NotImplementedException();

        public override void CheckValidity() => throw new NotImplementedException();

        public override Vector3d NormalAt(double t) => throw new NotImplementedException();

        public override Point3d PointAt(double t) => throw new NotImplementedException();

        public override Vector3d TangentAt(double t) => throw new NotImplementedException();

        public override Plane TNBFrameAt(double t) => throw new NotImplementedException();

        public override double Length { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }

}