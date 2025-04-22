using System;
using BVH_Tree.Utils;

namespace BVH_Tree.BVH {

    public class BVHNode {
        
        
        /* If left and right are null, then it is a LEAF node */
        public BVHNode Left { get; set; }
        public BVHNode Right { get; set; }
        
        /* The Bounds of ALL children */
        public Vector3 MaxBounds { get; set; }
        public Vector3 MinBounds { get; set; }
        
        /* Coordinate axis which primitive children are split */
        public int SplitAxis { get; set; }
        
        /* Which primitives are stored in the leaf (primitives[FirstPrimeOffset] => primitives[FirstPrimOffset + NPrimitives] */
        public int FirstPrimOffset { get; set; }
        
        public int NPrimitives { get; set; }
        
        
        public int Value { get; set; }
        
        public BVHNode(int value) {
            this.Value = value;
        }


        public void InitLeaf(int first, int n, Vector3 maxBounds, Vector3 minBounds) {
            FirstPrimOffset = first;
            NPrimitives = n;
            MaxBounds = maxBounds;
            MinBounds = minBounds;
            Left = null;
            Right = null;
        }

        public void InitInterior(int axis, BVHNode left, BVHNode right) {
            SplitAxis = axis;
            Right = right;
            Left = left;
            NPrimitives = 0;
            MinBounds= new Vector3(
                Math.Min(left.MinBounds.X, right.MinBounds.X),
                Math.Min(left.MinBounds.Y, right.MinBounds.Y),
                Math.Min(left.MinBounds.Z, right.MinBounds.Z)
            );

            MaxBounds = new Vector3(
                Math.Max(left.MaxBounds.X, right.MaxBounds.X),
                Math.Max(left.MaxBounds.Y, right.MaxBounds.Y),
                Math.Max(left.MaxBounds.Z, right.MaxBounds.Z)
            );
        }
        
        
        public BVHNode() {
            Left = null;
            Right = null;
        }

    }
}