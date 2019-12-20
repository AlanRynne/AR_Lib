using AR_Lib.Geometry;
using Xunit;

namespace AR_Lib.Tests.Geometry
{
    public class Point3dTests
    {
        [Theory]
        [ClassData(typeof(Point3dEqualDataset))]
        public void EqualsAndHashCode_HaveConsistentResults(Point3d pt, Point3d pt2)
        {
            bool b1 = (pt == pt2);
            bool b2 = (pt.GetHashCode() == pt2.GetHashCode());

            Assert.True(b1 && b1 == b2);
        }
    }
}
