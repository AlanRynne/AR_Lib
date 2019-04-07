using System;

namespace AR_Lib.Geometry
{
    public abstract class BasePoint
    {
        // Public properties
        public double X { get { return x; } set { if (isUnset) isUnset = false; x = value; } }
        public double Y { get { return y; } set { if (isUnset) isUnset = false; y = value; } }
        public double Z { get { return z; } set { if (isUnset) isUnset = false; z = value; } }
        public bool IsUnset { get => isUnset; }

        // Private parameters
        protected double x;
        protected double y;
        protected double z;
        protected bool isUnset;

        //Constructors

        protected BasePoint() : this(0, 0, 0) { isUnset = true; }

        protected BasePoint(BasePoint point) : this(point.x, point.y, point.z)
        {
        }

        protected BasePoint(double xCoord, double yCoord, double zCoord)
        {
            x = xCoord;
            y = yCoord;
            z = zCoord;
            isUnset = false;
        }


        // Mathematical operations

        public void Add(BasePoint point)
        {
            x += point.X;
            y += point.Y;
            z += point.Z;
            isUnset = false;
        }

        public void Substract(BasePoint point)
        {
            x -= point.X;
            y -= point.Y;
            z -= point.Z;
            isUnset = false;
        }

        public void Multiply(double scalar)
        {
            x *= scalar;
            y *= scalar;
            z *= scalar;
        }

        public void Divide(double scalar)
        {
            x /= scalar;
            y /= scalar;
            z /= scalar;
        }

        public void Negate()
        {
            x = -x;
            y = -y;
            z = -z;
        }

        public override string ToString() => "{ " + x + ", " + y + ", " + z + " }";
        public double[] ToArray() => new double[] { x, y, z };

        // Override Methods
        public override bool Equals(object obj)
        {
            if (obj is BasePoint)
            {
                BasePoint pt = (BasePoint)obj;
                if (Math.Abs(this.X - pt.X) < 0.000001 && Math.Abs(this.Y - pt.Y) < 0.000001 && Math.Abs(this.Z - pt.Z) < 0.000001) { return true; }
                else { return false; }
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, X) ? X.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Y) ? Y.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Z) ? Z.GetHashCode() : 0);
                return hash;
            }
        }

    }
}
