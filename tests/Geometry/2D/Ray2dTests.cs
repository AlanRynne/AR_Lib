using System;
using Paramdigma.Core.Geometry;
using Xunit;

namespace Paramdigma.Core.Tests.Geometry
{
    public class Ray2dTests
    {
        [Fact]
        public void CanCreate_AndThrowExceptions()
        {
            var ray = new Ray2d(Point2d.Origin, Vector2d.WorldX);
            Assert.Equal(Point2d.Origin, ray.Origin);
            Assert.Equal(Vector2d.WorldX, ray.Direction);

            Assert.Throws<ArgumentNullException>(() => new Ray2d(Point2d.Origin, null));
            Assert.Throws<ArgumentNullException>(() => new Ray2d(null, Vector2d.WorldY));
        }
    }
}