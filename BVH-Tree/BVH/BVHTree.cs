using System;
using System.Collections.Generic;
using BVH_Tree.Scene;
using BVH_Tree.Utils;

namespace BVH_Tree.BVH {
    public static class BVHTree {
        
        private static string SPLIT_METHOD = "MIDDLE"; // EQUALCOUNTS, MIDDLE, SAH
        
        public static void SetSplitMethod(string splitMethod) {
            SPLIT_METHOD = splitMethod;
        }
        
        public static BVHNode BuildTree(List<Triangle> primitives) {
            List<PrimitiveInfo> primitivesInfo = GetPrimitveInfo(primitives);
            List<Triangle> orderedPrimitives = new List<Triangle>();
            int totalNodes = 0;
            BVHNode root = BuildRecursive(primitives, primitivesInfo, 0, primitivesInfo.Count, ref totalNodes, orderedPrimitives);
            return root;
        }

        private static List<PrimitiveInfo> GetPrimitveInfo(List<Triangle> primitives) {
            // Compute complete bounding box and centroid of bounding box
            List<PrimitiveInfo> primitivesInfo = new List<PrimitiveInfo>();
            for(int i=0; i<primitives.Count; i++) {
                Triangle triangle = primitives[i];
                /*
                 * AABB - Axis Aligned Bounding Box
                 * Find min and max for each axis x,y,z
                 * Bounding box has:
                 *   MinCorner(MinX, MinY, MinZ)
                 *   MaxCorner (MaxX, MaxY, MaxZ)
                 */
                float MinX = Math.Min(triangle.V1.X, Math.Min(triangle.V2.X, triangle.V3.X));
                float MinY = Math.Min(triangle.V1.Y, Math.Min(triangle.V2.Y, triangle.V3.Y));
                float MinZ = Math.Min(triangle.V1.Z, Math.Min(triangle.V2.Z, triangle.V3.Z));
                Vector3 MinCorner = new Vector3(MinX, MinY, MinZ);
                float MaxX = Math.Max(triangle.V1.X, Math.Max(triangle.V2.X, triangle.V3.X));
                float MaxY = Math.Max(triangle.V1.Y, Math.Max(triangle.V2.Y, triangle.V3.Y));
                float MaxZ = Math.Max(triangle.V1.Z, Math.Max(triangle.V2.Z, triangle.V3.Z));
                Vector3 MaxCorner = new Vector3(MaxX, MaxY, MaxZ);
                
                Bounds3 PrimitiveBounds = new Bounds3(MinCorner, MaxCorner);
                primitivesInfo.Add(new PrimitiveInfo(PrimitiveBounds, i));
            }
            return primitivesInfo;
        }

        private static BVHNode BuildRecursive(List<Triangle> primitives, List<PrimitiveInfo> primitiveInfo, int start, int end, 
                                                ref int totalNodes, List<Triangle> orderedPrimitives ) {
            
            BVHNode node = new BVHNode();
            totalNodes++;
            
            // Compute bounds of all primitives in BVH Node
            Bounds3 NodeBounds = new Bounds3();
            for (int i = 0; i < primitiveInfo.Count; i++) {
                NodeBounds.Union(primitiveInfo[i].Bounds.Min);
                NodeBounds.Union(primitiveInfo[i].Bounds.Max);
            }
            int nPrimitives = end - start;

            if (nPrimitives <= 1) {
                // Create leaf BVH Node
                int firstPrimOffset = orderedPrimitives.Count;
                for (int i = start; i < end; i++) {
                    int primNumber = primitiveInfo[i].PrimitiveIndex;
                    orderedPrimitives.Add(primitives[primNumber]);
                }
                node.InitLeaf(firstPrimOffset, nPrimitives, NodeBounds);
                return node;
            }
            else {
                // Compute bounds of primitive centroids, choose split dimension dim
                Vector3 centroidMinBounds = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                Vector3 centroidMaxBounds = new Vector3(float.MinValue, float.MinValue, float.MinValue);

                for (int i = start; i < end; i++) {
                    centroidMinBounds.X = Math.Min(centroidMinBounds.X, primitiveInfo[i].Centroid.X);
                    centroidMinBounds.Y = Math.Min(centroidMinBounds.Y, primitiveInfo[i].Centroid.Y);
                    centroidMinBounds.Z = Math.Min(centroidMinBounds.Z, primitiveInfo[i].Centroid.Z);
                    centroidMaxBounds.X = Math.Max(centroidMaxBounds.X, primitiveInfo[i].Centroid.X);
                    centroidMaxBounds.Y = Math.Max(centroidMaxBounds.Y, primitiveInfo[i].Centroid.Y);
                    centroidMaxBounds.Z = Math.Max(centroidMaxBounds.Z, primitiveInfo[i].Centroid.Z);
                }
                
                // Choose the largest dimension to split amongst
                Vector3 diff = NodeBounds.Max - NodeBounds.Min;
                int axis;
                if (diff.X > diff.Y && diff.X > diff.Z) axis = 0; // X axis
                else if (diff.Y > diff.Z) axis = 1; // Y axis
                else axis = 2; // Z axis
                
                // Partition primitives into two sets and build children
                int mid = (start + end) / 2;

                float epsilon = 1e-5f; // For float equality precision
                bool centroidsEqual = Math.Abs(centroidMaxBounds.getAxis(axis) - centroidMinBounds.getAxis(axis)) < epsilon;
                if(centroidsEqual) {
                    // Create a leaf node
                    int firstPrimOffset = orderedPrimitives.Count;
                    for (int i = start; i < end; i++) {
                        int primNumber = primitiveInfo[i].PrimitiveIndex;
                        orderedPrimitives.Add(primitives[primNumber]);
                    }
                    node.InitLeaf(firstPrimOffset, nPrimitives, NodeBounds);
                    return node;
                }
                else {
                    // Partition primitives based on splitMethod
                    switch (SPLIT_METHOD) {
                        case "MIDDLE":
                            // Partition primitives through node midpoint (Edge case -> all primitives partition to one side, creating a degenerate tree)
                            int midIndex = start;
                            float midpoint = (centroidMaxBounds.getAxis(axis) + centroidMinBounds.getAxis(axis)) / 2;
                            // Reorder primitives based on midpoint
                            for (int i = start; i < end; i++) {
                                float centroid = primitiveInfo[i].Centroid.getAxis(axis);
                                if (centroid < midpoint) {
                                    PrimitiveInfo temp = primitiveInfo[i];
                                    primitiveInfo[i] = primitiveInfo[midIndex];
                                    primitiveInfo[midIndex] = temp;
                                    midIndex++;
                                }
                            }
                            break;
                        case "EQUALCOUNTS":
                            // TODO
                            break;
                        case "SAH":
                            // TODO
                            break;
                    }
                }
                
                node.InitInterior(axis, BuildRecursive(primitives, primitiveInfo, start, mid,ref totalNodes, orderedPrimitives), 
                            BuildRecursive(primitives, primitiveInfo, mid, end, ref totalNodes, orderedPrimitives));;   
                
                
            }
            return node;
        }
        
        
        
        
        
        // UTILS
        public static void PrintTree(BVHNode root) {
            // Breadth first search for print ordering
            if (root == null) return;
            
            Queue<BVHNode> queue = new Queue<BVHNode>();
            queue.Enqueue(root);
            int treeDepth = 0;
            while (queue.Count > 0) {
                int levelCount = queue.Count;
                string currentLevel = "";

                for (int i = 0; i < levelCount; i++) {
                    BVHNode node = queue.Dequeue();
                    currentLevel += $"({node.Value})" ;
                    if(node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }
                
                Console.WriteLine($"Depth: {treeDepth} : {currentLevel}");
                treeDepth++;
            }
        }
        
        public static List<int> RayCast(BVHNode root, Ray ray) {
            List<int> hits = new List<int>(); // return indices of primitives hit by ray
            if(root == null) return hits;
            RayCastRecursive(root, ray, hits);
            return hits;
        }

        public static void RayCastRecursive(BVHNode node, Ray ray, List<int> hits) {

            if (node == null) return;

            if (!Ray.IntersectsAABB(ray, node.Bounds)) return;

            if (node.IsLeaf) {
                for(int i=node.FirstPrimOffset; i<node.FirstPrimOffset + node.NPrimitives; i++) {
                    hits.Add(i); // !! Technically not a primitive hit, but the bounding box hit
                }
            }
            else {
                // Intersect with left and right children
                RayCastRecursive(node.Left, ray, hits);
                RayCastRecursive(node.Right, ray, hits);
            }
        }


    }
}