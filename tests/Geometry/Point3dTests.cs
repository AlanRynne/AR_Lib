using AR_Lib.Geometry;
using Xunit;

namespace AR_Lib.Tests.Geometry
{
    public class Point3dTests
    {
        [Fact]
        public void CanBe_Added()
        {
            var a = 3.3;
            var b = 2.2;
            var c = 4.11;
            var ptA = new Point3d(a, b, c);
            var ptB = new Point3d(b, c, a);
            var s = ptA - ptB;
            var ptResult = new Point3d(a + b, b + c, c + a);
            Assert.True(ptA + ptB == ptResult);
        }

        [Fact]
        public void CanBe_Substracted()
        {
            var a = 3.3;
            var b = 2.2;
            var c = 4.11;
            var ptA = new Point3d(a, b, c);
            var ptB = new Point3d(b, c, a);
            var ptResult = new Point3d(a - b, b - c, c - a);
            var subs = ptA - ptB;
            var s1 = ptA;
            Assert.True(subs == ptResult);
        }

        [Fact]
        public void CanBe_Multiplied()
        {
            var a = 3.3;
            var b = 2.2;
            var c = 4.11;
            var m = 1.45;
            var ptA = new Point3d(a, b, c);
            var ptResult = new Point3d(a * m, b * m, c * m);
            Assert.True(ptA * m == ptResult);
        }

        [Fact]
        public void CanBe_Divided()
        {
            var a = 3.3;
            var b = 2.2;
            var c = 4.11;
            var m = 1.45;
            var ptA = new Point3d(a, b, c);
            var ptResult = new Point3d(a / m, b / m, c / m);
            Assert.True(ptA / m == ptResult);
        }

        [Theory]
        [ClassData(typeof(Point3dEqualDataset))]
        public void EqualsAndHashCode_HaveConsistentResults(Point3d pt, Point3d pt2)
        {
            bool b1 = pt == pt2;
            bool b2 = pt.GetHashCode() == pt2.GetHashCode();

            Assert.True(b1 && b1 == b2);
        }
    }
}
