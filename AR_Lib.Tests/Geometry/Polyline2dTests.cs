using System.Collections;
using System.Collections.Generic;
using AR_Lib.Geometry;
using Xunit;

namespace AR_Lib.Tests.Geometry
{
    public class Polyline2dUnitSquareAndSegments : IEnumerable<object[]>
    {
            Point2d pt1 = new Point2d(0, 0);
            Point2d pt2 = new Point2d(1, 0);
            Point2d pt3 = new Point2d(1, 1);
            Point2d pt4 = new Point2d(0, 1);
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Polyline2d(new List<Point2d> { pt1, pt2, pt3, pt4 }, false), 3 };
            yield return new object[] { new Polyline2d(new List<Point2d> { pt1, pt2, pt3, pt4 }, true), 4 };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Polyline2dDataSet : IEnumerable<object[]>
    {
        Point2d pt1 = new Point2d(0, 0);
        Point2d pt2 = new Point2d(1, 0);
        Point2d pt3 = new Point2d(1, 1);
        Point2d pt4 = new Point2d(0, 1);
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Polyline2d(new List<Point2d> { pt1, pt2, pt3, pt4 }, false) };
            yield return new object[] { new Polyline2d(new List<Point2d> { pt1, pt2, pt3, pt4 }, true) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public class Polyline2dTests
    {
        [Theory]
        [ClassData(typeof(Polyline2dUnitSquareAndSegments))]
        public void Constructor_ClosedOption_AddsVertexAndSegment(Polyline2d polyline, int expectedSegments)
        {
            Assert.True(polyline.Vertices.Count == expectedSegments + 1 );
            Assert.True(polyline.Segments.Count == expectedSegments);
        }
        
        [Theory]
        [ClassData(typeof(Polyline2dUnitSquareAndSegments))] 
        public void DefaultDomain_IsParametrizedByArcLength(Polyline2d polyline, double expectedLength)
        {

            Assert.True(polyline.Domain.End == expectedLength);
        }

        [Theory]
        [ClassData(typeof(Polyline2dDataSet))]
        public void Reparametrize_SetsAllSegmentsDomain(Polyline2d polyline)
        {
            polyline.Reparametrize();

            Assert.True(polyline.Domain.End == polyline.Segments[polyline.Segments.Count - 1].Domain.End);
            Assert.True(polyline.Domain.End == 1);
        }
    }
}