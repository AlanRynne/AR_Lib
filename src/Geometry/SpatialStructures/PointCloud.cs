using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using AR_Lib.Geometry;

namespace AR_Lib.SpatialSearch
{
    /// <summary>
    /// Represents a collection of points with a color assigned to them.
    /// TODO: This is only a basic data structure for now
    /// </summary>
    public class PointCloud
    {
        /// <summary>
        /// Collection of points in the point cloud.
        /// </summary>
        public List<PointCloudMember> Points { get; private set; }

    }

    /// <summary>
    /// Class representing a point contained in a point cloud
    /// </summary>
    public class PointCloudMember : BasePoint
    {
        /// <summary>
        /// Color at this point
        /// </summary>
        /// <value>The current color if set, defaults to white.</value>
        public Color Color { get; set; }
    }
}