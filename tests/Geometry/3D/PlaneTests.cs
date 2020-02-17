using System;
using Paramdigma.Core.Geometry;
using Xunit;

namespace Paramdigma.Core.Tests.Geometry
{
    public class PlaneTests
    {
        [Fact]
        public void CanBe_Created()
        {
            var plane = new Plane(Point3d.WorldOrigin);
            var planeB = new Plane(Point3d.WorldOrigin, Vector3d.UnitZ, Vector3d.UnitX);

            var ptA = new Point3d(1, 0, 0);
            var ptB = new Point3d(0, 1, 0);
            var ptC = new Point3d(0, 0, 0);
            var planeC = new Plane(ptC, ptA, ptB);
        }

        [Fact]
        public void CanCreate_SpecialPlanes()
        {
            var ptXY = Plane.WorldXY;
            var ptYZ = Plane.WorldYZ;
            var ptXZ = Plane.WorldXZ;
        }

        [Fact]
        public void CanConvert_ToString()
        {
            var pln = Plane.WorldXY;
            var s = pln.ToString();
            Console.WriteLine(s);
        }

        [Fact]
        public void CanCompute_Points()
        {
            var pln = Plane.WorldXY;
            var pt = pln.PointAt(1, 1);
            var expectedPt = new Point3d(1, 1, 0);
            var ptB = pln.PointAt(1, 1, 1);
            var expectedPtB = new Point3d(1, 1, 1);
            Assert.True(pt == expectedPt);
            Assert.True(ptB == expectedPtB);
        }

        [Fact]
        public void CanBe_Cloned()
        {
            var pln = Plane.WorldXY;
            var plnB = pln.Clone();
            var plnC = new Plane(pln);
            Assert.True(!ReferenceEquals(pln, plnB));
            Assert.True(!ReferenceEquals(pln, plnC));
        }

        [Fact]
        public void CanCompute_ClosestPoint()
        {
            var pln = Plane.WorldXY;
            var pt = new Point3d(1, 1, 1);
            var expected = new Point3d(1, 1, 0);
            var result = pln.ClosestPoint(pt);
            var dist = pln.DistanceTo(pt);
            Assert.True(result == expected);
            Assert.True(dist == 1);
        }
    }
}