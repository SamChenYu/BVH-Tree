using System;
using BVH_Tree.Utils;

namespace BVH_Tree.BVH {

    public class BVHNode {
        
        
        /* If left and right are null, then it is a LEAF node */
        public BVHNode Left { get; set; }
        public BVHNode Right { get; set; }
        
        /* The Bounds of ALL children */
        public Bounds3 Bounds { get; set; }
        
        /* Coordinate axis which primitive children are split */
        public int SplitAxis { get; set; }
        
        /* Which primitives are stored in the leaf (primitives[FirstPrimeOffset] => primitives[FirstPrimOffset + NPrimitives] */
        public int FirstPrimOffset { get; set; }
        
        public int NPrimitives { get; set; }
        
        
        public String Value { get; set; }
        

        public BVHNode() {
            Left = null;
            Right = null;
        }

        public void UpdateValue() {
            if (Left == null && Right == null) {
                Value = $"Leaf [{FirstPrimOffset} - {FirstPrimOffset + NPrimitives}]";
            }
            else {
                Value = $"Interior [Axis: {SplitAxis}, Bounds: {Bounds.ToString()}]";
            }
        }

        public void InitLeaf(int first, int n, Bounds3 bounds) {
            FirstPrimOffset = first;
            NPrimitives = n;
            Bounds = bounds;
            Left = null;
            Right = null;
            UpdateValue();
        }

        public void InitInterior(int axis, BVHNode left, BVHNode right) {
            SplitAxis = axis;
            Right = right;
            Left = left;
            NPrimitives = 0;
            Bounds = new Bounds3();
            Bounds.InitBounds(Left.Bounds, Right.Bounds);
            UpdateValue();
        }
        
        

    }
}