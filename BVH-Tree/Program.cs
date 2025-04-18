using System;
using BVH_Tree.BVH;

namespace BVH_Tree {

    internal class Program {
    
        public static void Main(string[] args) {
            BVHTree bvhTree = new BVHTree(null);
            bvhTree.BuildTree();
            Console.WriteLine("BVH Tree built successfully.");
        }
        
        
        
        
    }
}