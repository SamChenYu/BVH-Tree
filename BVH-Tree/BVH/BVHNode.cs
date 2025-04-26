using System;
using BVH_Tree.Utils;

namespace BVH_Tree.BVH {

    public class BVHNode {
        
        
        /* If left and right are null, then it is a LEAF node */
        public BVHNode left { get; set; }
        public BVHNode right { get; set; }
        
        /* The Bounds of ALL children */
        public Bounds3 bounds { get; set; }
        
        /* Coordinate axis which primitive children are split */
        public int splitAxis { get; set; }
        
        /* Which primitives are stored in the leaf (primitives[FirstPrimeOffset] => primitives[FirstPrimOffset + NPrimitives] */
        public int firstPrimOffset { get; set; }
        
        public int nPrimitives { get; set; }
        
        
        public bool isLeaf { get; set; }
        public String value { get; set; }
        

        public BVHNode() {
            left = null;
            right = null;
        }

        public void UpdateValue() {
            if (left == null && right == null) {
                value = $"Leaf [{firstPrimOffset} - {firstPrimOffset + nPrimitives} [Bounds: {bounds.toString()}]]";
            }
            else {
                value = $"Interior [Axis: {splitAxis}, Bounds: {bounds.toString()}]";
            }
        }

        public void InitLeaf(int first, int n, Bounds3 bounds) {
            firstPrimOffset = first;
            nPrimitives = n;
            this.bounds = bounds;
            left = null;
            right = null;
            UpdateValue();
            isLeaf = true;
        }

        public void InitInterior(int axis, BVHNode left, BVHNode right) {
            splitAxis = axis;
            this.right = right;
            this.left = left;
            nPrimitives = 0;
            bounds = new Bounds3();
            bounds.initBounds(left.bounds, right.bounds);
            UpdateValue();
            isLeaf = false;
        }
        
        

    }
}