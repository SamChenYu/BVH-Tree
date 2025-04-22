using System;
using System.Collections.Generic;
using BVH_Tree.Scene;
using BVH_Tree.Utils;

namespace BVH_Tree.BVH {
    public static class BVHTree {
        
        public static string SPLIT_METHOD = "EQUALCOUNTS"; // EQUALCOUNTS, MIDDLE, SAH
        
        public static BVHNode BuildTree(List<Triangle> primitives) {
            List<PrimitiveInfo> primitivesInfo = GetPrimitveInfo(primitives); // Indices in primitivesInfo correspond to primitives

            BVHNode root = BuildRecursive(primitives, primitivesInfo, 0, primitivesInfo.Count, 0, primitives);
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
                primitivesInfo.Add(new PrimitiveInfo(MinCorner, MaxCorner, i));
            }
            return primitivesInfo;
        }

        private static BVHNode BuildRecursive(List<Triangle> primitives, List<PrimitiveInfo> primitiveInfo, int start, int end, 
                                                int totalNodes, List<Triangle> orderedPrimitives ) {
            
            BVHNode node = new BVHNode();
            totalNodes++; // Check if it is pass by reference or pass by address
            
            // Compute bounds of all primitives in BVH Node
            Vector3 minBounds = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 maxBounds = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < primitiveInfo.Count; i++) {
                minBounds.X = Math.Min(minBounds.X, primitiveInfo[i].MinCorner.X);
                minBounds.Y = Math.Min(minBounds.Y, primitiveInfo[i].MinCorner.Y);
                minBounds.Z = Math.Min(minBounds.Z, primitiveInfo[i].MinCorner.Z);
                maxBounds.X = Math.Max(maxBounds.X, primitiveInfo[i].MaxCorner.X);
                maxBounds.Y = Math.Max(maxBounds.Y, primitiveInfo[i].MaxCorner.Y);
                maxBounds.Z = Math.Max(maxBounds.Z, primitiveInfo[i].MaxCorner.Z);
            }
            

            int nPrimitives = end - start;
            if (nPrimitives == 1) {
                // Create leaf BVH Node
                int firstPrimOffset = orderedPrimitives.Count;
                for (int i = start; i < end; i++) {
                    int primNumber = primitiveInfo[i].PrimitiveIndex;
                    orderedPrimitives.Add(primitives[primNumber]);
                }
                
                node.InitLeaf(firstPrimOffset, nPrimitives, maxBounds, minBounds);
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
                Vector3 diff = maxBounds - minBounds;
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
                    node.InitLeaf(firstPrimOffset, nPrimitives, maxBounds, minBounds);
                    return node;
                }
                else {
                    // Partition primitives based on splitMethod
                    switch (SPLIT_METHOD) {
                        case "MIDDLE":
                            // Partition primitives through nodes midpoint
                            // Cut at the middle of the bounding box and split at the axis
                            float midpoint = (centroidMaxBounds.getAxis(axis) + centroidMinBounds.getAxis(axis)) / 2;
                            
                            // Reorder primitives based on midpoint


                            break;
                        case "EQUALCOUNTS":
                            
                            
                            break;
                        case "SAH":
                            
                            break;
                    }
                }
                
                node.InitInterior(axis, BuildRecursive(primitives, primitiveInfo, start, mid, totalNodes, orderedPrimitives), 
                            BuildRecursive(primitives, primitiveInfo, mid, end, totalNodes, orderedPrimitives));;   
                
                
            }
            return node;
        }
        
        
        
        
        
        // UTILS
        public static void PrintTree(BVHNode Root) {
            // Breadth first search for print ordering
            if (Root == null) return;
            
            Queue<BVHNode> queue = new Queue<BVHNode>();
            queue.Enqueue(Root);
            
            while (queue.Count > 0) {
                int levelSize = queue.Count;
                string currentLevel = "";

                for (int i = 0; i < levelSize; i++) {
                    BVHNode node = queue.Dequeue();
                    currentLevel += node.Value + " " ;
                    if(node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }
                
                Console.WriteLine($"Level: {levelSize} : {currentLevel}");
            }
        }

    }
}