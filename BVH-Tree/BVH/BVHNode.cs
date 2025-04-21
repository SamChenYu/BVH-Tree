namespace BVH_Tree.BVH {

    public class BVHNode {
        
        
        
        public BVHNode left { get; set; }
        public BVHNode right { get; set; }

        public int value { get; set; }


        public BVHNode(int value) {
            this.value = value;
        }
        
        public BVHNode() {
            left = null;
            right = null;
        }

    }
}