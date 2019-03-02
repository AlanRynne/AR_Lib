namespace AR_Lib.Geometry
{
    public abstract class BasePoint
    {
        // Public properties
        public double X { get { isUnset = false; return x; } set => x = value; }
        public double Y { get { isUnset = false; return y; } set => y = value; }
        public double Z { get { isUnset = false; return z; } set => z = value; }
        public bool IsUnset { get => isUnset; }

        // Private parameters
        private double x;
        private double y;
        private double z;
        private bool isUnset;

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

    }
}