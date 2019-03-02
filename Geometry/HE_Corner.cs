

namespace AR_Lib.HalfEdgeMesh
{
    public class HE_Corner 
    {
        public HE_HalfEdge HalfEdge;
        public int Index;

        public HE_Corner()
        {
            Index = -1;
        }

        public HE_Vertex Vertex => this.HalfEdge.Prev.Vertex;
        public HE_Face Face => this.HalfEdge.Face;
        public HE_Corner Next => this.HalfEdge.Next.Corner;
        public HE_Corner Prev => this.HalfEdge.Prev.Corner;

    }
}
