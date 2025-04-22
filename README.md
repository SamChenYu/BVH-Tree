# BVH-Tree
Bounding Volume Hierarchy Implementation



# BVH Building Process

1. ```List<Triangle> primitives ``` is initialised, containing all geometry data.
2. ```List<PrimitiveInfo> primitiveInfo ``` is computed -> calculating the bounding boxes, centroid of the bounding box and recording the index within ```primitives```.
3. ```private static BVHNode BuildRecursive(List<Triangle> primitives, List<PrimitiveInfo> primitiveInfo, int start, int end, int totalNodes, List<Triangle> orderedPrimitives ) ``` - Partitioning is done on ```primitiveInfo```, which Triangles go left or right in the split. Once a Leaf Node is created, you copy triangles from ```primitives``` into ```orderedPrimitives``` list in the order dictated by ```primitiveInfo```. ```orderedPrimitives``` becomes the new reordered list in the BVH -> each leaf node only stores an offset and count in the array so it knows which triangles exactly to check during traversal. 
4. BVH Tree computation can now begin, with 3 SplitMethods - EqualCounts, Middle and SAH (Surface Area Heuristics)

### Equal Counts
1. Pick the axis with the largest centroid extent, then sort all primitives by their centroid's coordinate on that axis, then split the list in half by number of primitives


### Middle

1. Compute the midpoint of the centroid's bounding box along the split axis (X, Y, Z)
2. Loop through ```primitiveInfo``` and move all the primitives with centroids less than the midpoint to one side

### SAH

