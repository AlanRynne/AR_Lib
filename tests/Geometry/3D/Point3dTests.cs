using AR_Lib.Geometry;
using Xunit;

namespace AR_Lib.Tests.Geometry
{
    public class Point3dTests
    {
        [Fact]
        public void CanBe_Added()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var ptA = new Point3d(a, b, c);
            var ptB = new Point3d(b, c, a);
            var s = ptA - ptB;
            var ptResult = new Point3d(a + b, b + c, c + a);
            Assert.True(ptA + ptB == ptResult);
        }

        [Fact]
        public void CanBe_Substracted()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var ptA = new Point3d(a, b, c);
            var ptB = new Point3d(b, c, a);
            var ptResult = new Point3d(a - b, b - c, c - a);
            Assert.True(ptA - ptB == ptResult);
        }

        [Fact]
        public void CanBe_Multiplied()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            const double m = 1.45;
            var ptA = new Point3d(a, b, c);
            var ptResult = new Point3d(a * m, b * m, c * m);
            Assert.True(ptA * m == ptResult);
        }

        [Fact]
        public void CanBe_Divided()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            const double m = 1.45;
            var ptA = new Point3d(a, b, c);
            var ptResult = new Point3d(a / m, b / m, c / m);
            Assert.True(ptA / m == ptResult);
        }

        [Fact]
        public void CanBe_Negated()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var ptA = new Point3d(a, b, c);
            var ptResult = new Point3d(-a, -b, -c);
            Assert.True(-ptA == ptResult);
        }

        [Theory]
        [ClassData(typeof(Point3dEqualDataset))]
        public void EqualsAndHashCode_HaveConsistentResults(Point3d pt, Point3d pt2)
        {
            var b1 = pt == pt2;
            var b2 = pt.GetHashCode() == pt2.GetHashCode();

            Assert.True(b1 && b1 == b2);
        }
    }
}
