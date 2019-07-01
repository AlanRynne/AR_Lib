using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using AR_Lib.Geometry;

namespace AR_Lib.SpatialSearch
{
    /// <summary>
    /// Represents a collection of points with a color assigned to them.
    /// </summary>
    public class PointCloud
    {
        List<PointCloudMember> _points;

        /// <summary>
        /// Collection of points in the point cloud.
        /// </summary>
        public List<PointCloudMember> Points { get => _points; private set => _points = value; }

    }

    /// <summary>
    /// Class representing a point contained in a point cloud
    /// </summary>
    public class PointCloudMember : BasePoint
    {
        private Color _color;

        /// <summary>
        /// Color at this point
        /// </summary>
        /// <value>The current color if set, defaults to white.</value>
        public Color Color { get => _color; set => _color = value; }
    }
}