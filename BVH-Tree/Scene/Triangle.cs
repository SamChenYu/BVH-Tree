using System;
using System.Collections.Specialized;
using BVH_Tree.Utils;

namespace BVH_Tree.Scene {

    public class Triangle {
    
        public Vector3 V1 { get; set; }
        public Vector3 V2 { get; set; }
        public Vector3 V3 { get; set; }
        
        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3) {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public void Print() {
            Console.WriteLine($"Triangle at ({V1.X}, {V1.Y}, {V1.Z}), ({V2.X}, {V2.Y}, {V2.Z}), ({V3.X}, {V3.Y}, {V3.Z})" );
        }
        
        
    }
}