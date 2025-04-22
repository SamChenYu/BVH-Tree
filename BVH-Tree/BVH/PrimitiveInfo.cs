using BVH_Tree.Utils;

namespace BVH_Tree.BVH {
    public class PrimitiveInfo {
        
        /* BOUNDING BOX */
        public Bounds3 Bounds { get; set; }
        public Vector3 Centroid { get; set; }
        
        public int PrimitiveIndex { get; set; }

        public PrimitiveInfo(Bounds3 bounds, int primitiveIndex) {
            this.Bounds = bounds;
            this.Centroid = bounds.GetCentroid();
            this.PrimitiveIndex = primitiveIndex;
        }
    }
}