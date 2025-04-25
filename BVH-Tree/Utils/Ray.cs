using System;

namespace BVH_Tree.Utils {
    public class Ray {
        
        
        public Vector3 origin { get; set; }
        public Vector3 direction { get; set; }


        public Ray(Vector3 origin, Vector3 direction) {
            this.origin = origin;
            this.direction = direction;
        }

        public static bool intersectsAABB(Ray ray, Bounds3 bounds) {

            float tEntry = float.MinValue;
            float tExit = float.MaxValue;

            for (int axis = 0; axis < 3; axis++) {
                float inverseDirection = 1.0f / ray.direction.getAxis(axis);
                float tNear = (bounds.min.getAxis(axis) - ray.origin.getAxis(axis)) * inverseDirection;
                float tFar = (bounds.max.getAxis(axis) - ray.origin.getAxis(axis)) * inverseDirection;

                if (inverseDirection < 0.0f) {
                    // Swap if the ray is going in the negative direction
                    float temp = tNear;
                    tNear = tFar;
                    tFar = temp;
                }
                
                tEntry = Math.Max(tEntry, tNear);
                tExit = Math.Min(tExit, tFar);

                if (tEntry > tExit) {
                    return false;
                }
                
            }

            return true;
        }
    }
}