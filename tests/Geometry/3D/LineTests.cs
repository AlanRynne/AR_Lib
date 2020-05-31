using System;
using Paramdigma.Core.Geometry;
using Xunit;

namespace Paramdigma.Core.Tests.Geometry
{
    public class Line3dTests : CurveBaseTests<Line>
    {
        internal Line TestLine = new Line(Point3d.WorldOrigin, new Point3d(1, 1, 1));

        [Fact]
        public override void CanCheck_Validity()
        {
            Assert.True(TestLine.IsValid);
        }

        [Fact]
        public override void CanGet_BiNormal()
        {
            var biNorm = TestLine.BinormalAt(.5);
            Assert.True(biNorm != null);
        }

        [Fact]
        public override void CanGet_Length()
        {
            Assert.True(TestLine.Length == Math.Sqrt(3));
        }

        [Fact]
        public override void CanGet_Normal()
        {
            var norm = TestLine.NormalAt(.5);
            Assert.True(norm != null);

            var line = new Line(Point3d.WorldOrigin, Vector3d.UnitZ, 1);
            line.NormalAt(0.5);

        }

        [Fact]
        public override void CanGet_PerpFrame()
        {
            var biNorm = TestLine.FrameAt(.5);
            Assert.True(biNorm != null);
        }

        [Fact]
        public override void CanGet_PointAt()
        {
            var pt = TestLine.PointAt(.5);
            Assert.True(pt == new Point3d(0.5, 0.5, 0.5));
        }

        [Fact]
        public override void CanGet_Tangent()
        {
            var biNorm = TestLine.TangentAt(.5);
            Assert.True(biNorm == new Vector3d(1, 1, 1).Unit());
        }
    }
}