using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using AR_Lib.Geometry;

namespace AR_Lib.SpatialSearch
{
    /// <summary>
    /// Class representing a point contained in a point cloud.
    /// </summary>
    public class PointCloudMember : BasePoint
    {
        /// <summary>
        /// Gets or sets the color at this point.
        /// </summary>
        /// <value>The current color if set, defaults to white.</value>
        public Color Color { get; set; }
    }
}