using Paramdigma.Core.Geometry;
using Xunit;

namespace Paramdigma.Core.Tests.Geometry
{
    public class Vector3dTests
    {
        [Fact]
        public void CanBe_Added()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var vA = new Vector3d(a, b, c);
            var vB = new Vector3d(b, c, a);
            var s = vA - vB;
            var ptResult = new Vector3d(a + b, b + c, c + a);
            Assert.True(vA + vB == ptResult);
        }

        [Fact]
        public void CanBe_Substracted()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var vA = new Vector3d(a, b, c);
            var vB = new Vector3d(b, c, a);
            var ptResult = new Vector3d(a - b, b - c, c - a);
            Assert.True(vA - vB == ptResult);
        }

        [Fact]
        public void CanBe_Multiplied()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            const double m = 1.45;
            var v = new Vector3d(a, b, c);
            var ptResult = new Vector3d(a * m, b * m, c * m);
            Assert.True(v * m == ptResult);
        }

        [Fact]
        public void CanBe_Divided()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            const double m = 1.45;
            var v = new Vector3d(a, b, c);
            var ptResult = new Vector3d(a / m, b / m, c / m);
            Assert.True(v / m == ptResult);
        }

        [Fact]
        public void CanBe_Negated()
        {
            const double a = 3.3;
            const double b = 2.2;
            const double c = 4.11;
            var v = new Vector3d(a, b, c);
            var ptResult = new Vector3d(-a, -b, -c);
            Assert.True(-v == ptResult);
        }

        [Fact]
        public void EqualsAndHashCode_HaveConsistentResults()
        {
            var v = new Vector3d(-1, 1, 1);
            var v2 = new Vector3d(-1.000000005, 1, 1);
            var b1 = v == v2;
            var b2 = v.GetHashCode() == v2.GetHashCode();

            Assert.True(b1 && b1 == b2);
        }
    }
}
