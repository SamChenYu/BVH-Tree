namespace BVH_Tree.Utils {

    public class Vector3     {

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float X, float Y, float Z) {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        public float getAxis(int axis) {
            // X = 0, Y = 1, Z = 2
            switch (axis) {
                case 0: return X;
                case 1: return Y;
                case 2: return Z;
                default: throw new System.Exception("Invalid axis");
            }
        }
        
        
        public static Vector3 operator +(Vector3 v1, Vector3 v2) {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        
        public static Vector3 operator -(Vector3 v1, Vector3 v2) {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
    }
}