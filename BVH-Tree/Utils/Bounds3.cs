using System;

namespace BVH_Tree.Utils {
    public class Bounds3 {
        
        public Vector3 min { get; set; }
        public Vector3 max { get; set; }
        
        
        public Bounds3(Vector3 min, Vector3 max) {
            this.min = min;
            this.max = max;
        }

        public Bounds3() {
            min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        }

        public void initVectors(Vector3 v1, Vector3 v2) {
            min= new Vector3(
                Math.Min(v1.X, v2.X),
                Math.Min(v1.Y, v2.Y),
                Math.Min(v1.Z, v2.Z)
            );
            
            max = new Vector3(
                Math.Max(v1.X, v2.X),
                Math.Max(v1.Y, v2.Y),
                Math.Max(v1.Z, v2.Z)
            );
        }

        public void initBounds(Bounds3 b1, Bounds3 b2) {
            
            min.X = Math.Min(b1.min.X, b2.min.X);
            min.Y = Math.Min(b1.min.Y, b2.min.Y);
            min.Z = Math.Min(b1.min.Z, b2.min.Z);
            
            max.X = Math.Max(b1.max.X, b2.max.X);
            max.Y = Math.Max(b1.max.Y, b2.max.Y);
            max.Z = Math.Max(b1.max.Z, b2.max.Z);
            
        }


        public void union(Vector3 v) {
            min.X = Math.Min(min.X, v.X);
            min.Y = Math.Min(min.Y, v.Y);
            min.Z = Math.Min(min.Z, v.Z);
            max.X = Math.Max(max.X, v.X);
            max.Y = Math.Max(max.Y, v.Y);
            max.Z = Math.Max(max.Z, v.Z);
        }

        public Vector3 getCentroid() {
            return new Vector3(
                (min.X + max.X) / 2,
                (min.Y + max.Y) / 2,
                (min.Z + max.Z) / 2
            );
        }


        public string toString() {
            return $"Min: ({min.X}, {min.Y}, {min.Z}), Max: ({max.X}, {max.Y}, {max.Z})";
        }
        
        
    }
}