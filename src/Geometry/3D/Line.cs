namespace AR_Lib.Geometry
{

    public class Line : BaseCurve
    {

        public Line() : this(new Point3d(), new Point3d()) { }
        public Line(Point3d startPoint, Point3d endPoint)
        {
            this._startPoint = startPoint;
            this._endPoint = endPoint;
        }

        public override bool CheckValidity()
        {
            if (this.Length < Settings.Tolerance)
                return false;
            return true;
        }

        public override Point3d PointAt(double t) => _startPoint + t * (_endPoint - _startPoint);
        public override Vector3d TangentAt(double t)
        {
            Vector3d tangent = _endPoint - _startPoint;
            tangent.Unitize();
            return tangent;
        }
        public override Vector3d NormalAt(double t)
        {
            Vector3d tangent = TangentAt(t);
            Vector3d v = new Vector3d();

            if (tangent.Dot(Vector3d.UnitZ) == 1)
                v = Vector3d.UnitX;
            else
                v = Vector3d.UnitZ;

            return tangent.Cross(v);
        }
        public override Vector3d BinormalAt(double t) => Vector3d.CrossProduct(TangentAt(t), NormalAt(t));
        public override Plane FrameAt(double t) => new Plane(PointAt(t), TangentAt(t), NormalAt(t), BinormalAt(t));
        protected override double ComputeLength() => _startPoint.DistanceTo(_endPoint);

    }

}