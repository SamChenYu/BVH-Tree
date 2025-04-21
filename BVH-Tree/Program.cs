using System;
using System.Collections.Generic;
using BVH_Tree.BVH;
using BVH_Tree.Scene;
using BVH_Tree.Utils;

namespace BVH_Tree {

    internal class Program {
    
        public static void Main(string[] args) {
            
            // Init triangles
            List<Triangle> primitives = new List<Triangle>();
            for (int i = 0; i < 10; i++) { 
                primitives.Add(new Triangle(
                    new Vector3(i, i, i),
                    new Vector3(i, i, i),
                    new Vector3(i, i, i)
                ));
                
            }
            
            foreach (Triangle triangle in primitives) {
                triangle.Print();
            }



            BVHNode root = new BVHNode(1);
            root.left = (new BVHNode(2));
            root.right = (new BVHNode(3));
            root.left.left = (new BVHNode(4));
            root.left.right = (new BVHNode(5));
            root.right.left = (new BVHNode(6));
            root.right.right = (new BVHNode(7));
            
            PrintTree(root);

        }

        
        public static BVHNode BuildTree(List<Triangle> primitives) {
            BVHNode root = new BVHNode();
            return root;
        }


        public static void PrintTree(BVHNode root) {
            // Breadth first search for print ordering
            if (root == null) return;
            
            Queue<BVHNode> queue = new Queue<BVHNode>();
            queue.Enqueue(root);
            
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