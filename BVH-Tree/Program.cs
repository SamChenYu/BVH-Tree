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
            primitives.Add(new Triangle(new Vector3(0,1,0), new Vector3(3, 2, 0), new Vector3(1,4,0)));
            primitives.Add(new Triangle(new Vector3(2,5,0), new Vector3(4,6,0), new Vector3(5,7,0)));
            primitives.Add(new Triangle(new Vector3(5,7,0), new Vector3(6,7,0), new Vector3(7,6,7)));
            foreach (Triangle triangle in primitives) {
                triangle.Print();
            }
            
            BVHNode root = BVHTree.BuildTree(primitives);
            BVHTree.PrintTree(root);
            
            
        }
    }
}