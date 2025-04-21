using BVH_Tree.Utils;

namespace BVH_Tree.BVH {
    public class PrimitiveInfo {
        
        /* BOUNDING BOX */
        public Vector3 MinCorner { get; set; }
        public Vector3 MaxCorner { get; set; }

        public Vector3 Centroid { get; set; }

        public PrimitiveInfo(Vector3 MinCorner, Vector3 MaxCorner) {
            this.MinCorner = MinCorner;
            this.MaxCorner = MaxCorner;
            this.Centroid = new Vector3(
                (MinCorner.X + MaxCorner.X) / 2,
                (MinCorner.Y + MaxCorner.Y) / 2,
                (MinCorner.Z + MaxCorner.Z) / 2
            );
        }
        
        
        
        
        
    }
}