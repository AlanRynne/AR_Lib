namespace AR_Lib.Tests.Geometry
{
    public abstract class CurveBaseTests<T>
    {
        public abstract void CanGet_Length();

        public abstract void CanGet_PointAt();

        public abstract void CanCheck_Validity();

        public abstract void CanGet_Tangent();

        public abstract void CanGet_Normal();

        public abstract void CanGet_BiNormal();

        public abstract void CanGet_PerpFrame();
    }
}