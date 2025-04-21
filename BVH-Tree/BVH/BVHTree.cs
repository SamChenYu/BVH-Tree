using System;
using System.Collections.Generic;
using BVH_Tree.Scene;
using BVH_Tree.Utils;

namespace BVH_Tree.BVH {
    public class BVHTree {

        public BVHNode Root { get; set; }
        private List<Triangle> primitives;

        public BVHTree(List<Triangle> primitives) {
            this.primitives = primitives;
        }

        public BVHNode BuildTree() {
            List<PrimitiveInfo> primitivesInfo = GetPrimitveInfo(); // Indices in primitivesInfo correspond to primitives
            
            BVHNode root = new BVHNode();
            return root;
        }

        private List<PrimitiveInfo> GetPrimitveInfo() {
            // Compute complete bounding box and centroid of bounding box
            List<PrimitiveInfo> primitivesInfo = new List<PrimitiveInfo>();
            foreach (Triangle triangle in primitives) {
                /*
                 * AABB - Axis Aligned Bounding Box
                 * Find min and max for each axis x,y,z
                 * Bounding box has:
                 *   MinCorner( MinX, MinY, MinZ)
                 *   MaxCorner ( MaxX, MaxY, MaxZ)
                 */
                float MinX = Math.Min(triangle.V1.X, Math.Min(triangle.V2.X, triangle.V3.X));
                float MinY = Math.Min(triangle.V1.Y, Math.Min(triangle.V2.Y, triangle.V3.Y));
                float MinZ = Math.Min(triangle.V1.Z, Math.Min(triangle.V2.Z, triangle.V3.Z));
                Vector3 MinCorner = new Vector3(MinX, MinY, MinZ);
                float MaxX = Math.Max(triangle.V1.X, Math.Max(triangle.V2.X, triangle.V3.X));
                float MaxY = Math.Max(triangle.V1.Y, Math.Max(triangle.V2.Y, triangle.V3.Y));
                float MaxZ = Math.Max(triangle.V1.Z, Math.Max(triangle.V2.Z, triangle.V3.Z));
                Vector3 MaxCorner = new Vector3(MaxX, MaxY, MaxZ);
                primitivesInfo.Add(new PrimitiveInfo(MinCorner, MaxCorner));
            }
            return primitivesInfo;
        }

        
        
        
        
        
        
        
        // UTILS
        
        public void PrintTree() {
            // Breadth first search for print ordering
            if (Root == null) return;
            
            Queue<BVHNode> queue = new Queue<BVHNode>();
            queue.Enqueue(Root);
            
            while (queue.Count > 0) {
                int levelSize = queue.Count;
                string currentLevel = "";

                for (int i = 0; i < levelSize; i++) {
                    BVHNode node = queue.Dequeue();
                    currentLevel += node.value + " " ;
                    if(node.left != null) queue.Enqueue(node.left);
                    if (node.right != null) queue.Enqueue(node.right);
                }
                
                Console.WriteLine($"Level: {levelSize} : {currentLevel}");
            }
        }

    }
}