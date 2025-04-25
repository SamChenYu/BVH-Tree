using BVH_Tree.Utils;

namespace BVH_Tree.BVH {
    public class PrimitiveInfo {
        
        /* BOUNDING BOX */
        public Bounds3 bounds { get; set; }
        public Vector3 centroid { get; set; }
        
        public int primitiveIndex { get; set; }

        public PrimitiveInfo(Bounds3 bounds, int primitiveIndex) {
            this.bounds = bounds;
            this.centroid = bounds.getCentroid();
            this.primitiveIndex = primitiveIndex;
        }
    }
}