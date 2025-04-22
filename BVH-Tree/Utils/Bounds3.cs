using System;

namespace BVH_Tree.Utils {
    public class Bounds3 {
        
        public Vector3 Min { get; set; }
        public Vector3 Max { get; set; }
        
        
        public Bounds3(Vector3 min, Vector3 max) {
            this.Min = min;
            this.Max = max;
        }

        public Bounds3() {
            Min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        }

        public void InitVectors(Vector3 v1, Vector3 v2) {
            Min= new Vector3(
                Math.Min(v1.X, v2.X),
                Math.Min(v1.Y, v2.Y),
                Math.Min(v1.Z, v2.Z)
            );
            
            Max = new Vector3(
                Math.Max(v1.X, v2.X),
                Math.Max(v1.Y, v2.Y),
                Math.Max(v1.Z, v2.Z)
            );
        }

        public void InitBounds(Bounds3 b1, Bounds3 b2) {
            
            Min.X = Math.Min(b1.Min.X, b2.Min.X);
            Min.Y = Math.Min(b1.Min.Y, b2.Min.Y);
            Min.Z = Math.Min(b1.Min.Z, b2.Min.Z);
            
            Max.X = Math.Max(b1.Max.X, b2.Max.X);
            Max.Y = Math.Max(b1.Max.Y, b2.Max.Y);
            Max.Z = Math.Max(b1.Max.Z, b2.Max.Z);
            
        }


        public void Union(Vector3 v) {
            Min.X = Math.Min(Min.X, v.X);
            Min.Y = Math.Min(Min.Y, v.Y);
            Min.Z = Math.Min(Min.Z, v.Z);
            Max.X = Math.Max(Max.X, v.X);
            Max.Y = Math.Max(Max.Y, v.Y);
            Max.Z = Math.Max(Max.Z, v.Z);
        }

        public Vector3 GetCentroid() {
            return new Vector3(
                (Min.X + Max.X) / 2,
                (Min.Y + Max.Y) / 2,
                (Min.Z + Max.Z) / 2
            );
        }


        public string ToString() {
            return $"Min: ({Min.X}, {Min.Y}, {Min.Z}), Max: ({Max.X}, {Max.Y}, {Max.Z})";
        }
        
        
    }
}