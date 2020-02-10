using Paramdigma.Core.Geometry;
using Xunit;

namespace Paramdigma.Core.Tests.Geometry
{
    public class Point2dTests
    {
        [Fact]
        public void CanBe_Added()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var ptA = new Point2d(a, b);
            var ptB = new Point2d(b, c);
            var s = ptA - ptB;
            var ptResult = new Point2d(a + b, b + c);
            Assert.True(ptA + ptB == ptResult);
        }

        [Fact]
        public void CanBe_Substracted()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var ptA = new Point2d(a, b);
            var ptB = new Point2d(b, c);
            var ptResult = new Point2d(a - b, b - c);
            Assert.True(ptA - ptB == ptResult);
        }

        [Fact]
        public void CanBe_Multiplied()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double m = 1.45;
            var ptA = new Point2d(a, b);
            var ptResult = new Point2d(a * m, b * m);
            Assert.True(ptA * m == ptResult);
        }

        [Fact]
        public void CanBe_Divided()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double m = 1.45;
            var ptA = new Point2d(a, b);
            var ptResult = new Point2d(a / m, b / m);
            Assert.True(ptA / m == ptResult);
        }

        [Fact]
        public void CanBe_Negated()
        {
            const double a = 3.3;
            const double b = 2.2;
            var ptA = new Point2d(a, b);
            var ptResult = new Point2d(-a, -b);
            Assert.True(-ptA == ptResult);
        }

        [Fact]
        public void EqualsAndHashCode_HaveConsistentResults()
        {
            var pt = new Point2d(1.00000009, -1);
            var pt2 = new Point2d(1, -1);
            var b1 = pt == pt2;
            var b2 = pt.GetHashCode() == pt2.GetHashCode();

            Assert.True(b1 && b1 == b2);
        }
    }
}
