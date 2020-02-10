using System;
using System.Collections;
using System.Collections.Generic;
using AR_Lib.Geometry;
using Xunit;

namespace AR_Lib.Tests.Geometry
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
            var biNorm = TestLine.NormalAt(.5);
            Assert.True(biNorm != null);
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
            var biNorm = TestLine.PointAt(.5);
            Assert.True(biNorm == new Point3d(0.5, 0.5, 0.5));
        }

        [Fact]
        public override void CanGet_Tangent()
        {
            var biNorm = TestLine.TangentAt(.5);
            Assert.True(biNorm == new Vector3d(1, 1, 1).Unit());
        }
    }
}