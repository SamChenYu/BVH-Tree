using System;

namespace BVH_Tree.Utils {

    public static class MathUtils {

        public static Vector3[] ComputeMinMaxBounds(Vector3 v1, Vector3 v2) {
            Vector3[] bounds = new Vector3[2]; // [0] = Min, [1] = Max
            float minBoundsX = Math.Min(v1.X, v2.X);
            float minBoundsY = Math.Min(v1.Y, v2.Y);
            float minBoundsZ = Math.Min(v1.Z, v2.Z);
            bounds[0] = new Vector3(minBoundsX, minBoundsY, minBoundsZ);
            float maxBoundsX = Math.Max(v1.X, v2.X);
            float maxBoundsY = Math.Max(v1.Y, v2.Y);
            float maxBoundsZ = Math.Max(v1.Z, v2.Z);
            bounds[1] = new Vector3(maxBoundsX, maxBoundsY, maxBoundsZ);
            return bounds;
        }


    }
}