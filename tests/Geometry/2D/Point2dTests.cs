using AR_Lib.Geometry;
using Xunit;

namespace AR_Lib.Tests.Geometry
{
    public class Point2dTests
    {
        [Fact]
        public void CanBe_Added()
        {
            var a = 3.3;
            var b = 2.2;
            var c = 4.11;
            var ptA = new Point2d(a, b);
            var ptB = new Point2d(b, c);
            var s = ptA - ptB;
            var ptResult = new Point2d(a + b, b + c);
            Assert.True(ptA + ptB == ptResult);
        }

        [Fact]
        public void CanBe_Substracted()
        {
            var a = 3.3;
            var b = 2.2;
            var c = 4.11;
            var ptA = new Point2d(a, b);
            var ptB = new Point2d(b, c);
            var ptResult = new Point2d(a - b, b - c);
            Assert.True(ptA - ptB == ptResult);
        }

        [Fact]
        public void CanBe_Multiplied()
        {
            var a = 3.3;
            var b = 2.2;
            var m = 1.45;
            var ptA = new Point2d(a, b);
            var ptResult = new Point2d(a * m, b * m);
            Assert.True(ptA * m == ptResult);
        }

        [Fact]
        public void CanBe_Divided()
        {
            var a = 3.3;
            var b = 2.2;
            var m = 1.45;
            var ptA = new Point2d(a, b);
            var ptResult = new Point2d(a / m, b / m);
            Assert.True(ptA / m == ptResult);
        }

        [Fact]
        public void CanBe_Negated()
        {
            var a = 3.3;
            var b = 2.2;
            var ptA = new Point2d(a, b);
            var ptResult = new Point2d(-a, -b);
            Assert.True(-ptA == ptResult);
        }


        [Fact]
        public void EqualsAndHashCode_HaveConsistentResults()
        {
            var pt = new Point2d(1.00000009, -1);
            var pt2 = new Point2d(1, -1);
            bool b1 = pt == pt2;
            bool b2 = pt.GetHashCode() == pt2.GetHashCode();

            Assert.True(b1 && b1 == b2);
        }
    }
}
