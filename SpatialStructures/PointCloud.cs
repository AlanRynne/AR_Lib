using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using AR_Lib.Geometry;

namespace AR_Lib.SpatialSearch
{
    public class PointCloud
    {
        List<PointCloudMember> _points;

        public List<PointCloudMember> Points { get => _points; set => _points = value; }

    }

    public class PointCloudMember : BasePoint
    {
        private Color _color;

        public Color Color { get => _color; set => _color = value; }
    }
}