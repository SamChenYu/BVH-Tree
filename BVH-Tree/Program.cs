using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BVH_Tree.BVH;
using BVH_Tree.Scene;
using BVH_Tree.Utils;

namespace BVH_Tree {

    internal class Program {
    
        public static void Main(string[] args) {
            
            // Init triangles / primitives
            List<Triangle> primitives = new List<Triangle>();
            primitives.Add(new Triangle(1, new Vector3(0,1,0), new Vector3(3, 2, 0), new Vector3(1,4,0)));
            primitives.Add(new Triangle(2, new Vector3(2,5,0), new Vector3(4,6,0), new Vector3(5,7,0)));
            primitives.Add(new Triangle(3, new Vector3(5,7,0), new Vector3(6,7,0), new Vector3(7,6,7)));
            primitives.Add(new Triangle(4, new Vector3(4,6,0), new Vector3(5,8,0), new Vector3(6,9,0)));
            primitives.Add(new Triangle(5, new Vector3(10,15,1), new Vector3(11,14,0), new Vector3(12,13,0)));;
            foreach (Triangle triangle in primitives) {
                triangle.Print();
            }
            
            BVHTree.setSplitMethod("MIDDLE");
            //BVHTree.SetSplitMethod("EQUALCOUNTS");
            //BVHTree.SetSplitMethod("SAH");
            
            BVHNode root = BVHTree.buildTree(primitives);
            BVHTree.printTree(root);
            
            // Test the BVH with a Ray Intersection
            Ray ray = new Ray(
                    new Vector3(11,15,1),
                    new Vector3(0,0,0)
                    );

            List<int> hits = BVHTree.rayCast(root, ray);
            for (int i = 0; i < hits.Count; i++) {
                Triangle triangle = primitives[hits[i]];
                Console.WriteLine($"Ray Hit with Triangle {triangle.ID}");
            }


        }
    }
}