# BVH-Tree

A Bounding Volume Hierarchy Tree is a data structure used to accelerate ray casting intersections in graphics. Instead of looping through each geometry in linear time, a BVH Tree groups each primitive via their bounding boxes. As the BVH Tree is traversed, the bounding volumes get tighter allowing groups of objects to be culled from the intersection tests.


<div align="center">
  <img src="https://github.com/user-attachments/assets/7c15511b-ba37-4412-9c80-5400afcffc62" />
  <p>https://pbr-book.org/3ed-2018/Primitives_and_Intersection_Acceleration/Bounding_Volume_Hierarchies </p>
</div>




## BVH Building Process

1. ```List<Triangle> primitives ``` is initialised, containing all geometry data.
2. ```List<PrimitiveInfo> primitiveInfo ``` is computed => calculating the bounding boxes, centroid of the bounding box and recording the indices of each entry in```primitives```.
3. ```BVHNode BuildRecursive() ``` Partitioning is done on ```primitiveInfo``` - which Triangles go left or right in the split. Once a Leaf Node is created, you copy triangles from ```primitives``` into ```orderedPrimitives``` list in the order dictated by ```primitiveInfo```. ```orderedPrimitives``` becomes the new reordered list in the BVH => each leaf node only stores an offset and count in the array so it knows which triangles exactly to check during traversal. 
4. BVH Tree computation can now begin, with 3 SplitMethods - EqualCounts, Middle and SAH (Surface Area Heuristics)

### Equal Counts (Todo)
1. Pick the axis with the largest centroid extent, then sort all primitives by their centroid's coordinate on that axis, then split the list in half by number of primitives


### Middle

1. Compute the midpoint of the centroid's bounding box along the split axis (X, Y, Z)
2. Loop through ```primitiveInfo``` and move all the primitives with centroids less than the midpoint to one side

### SAH (Todo)

## Tree Traversal
Once the BVH Tree has been computed, a depth first search + ray intersection is used for traversal.
The currently implemented ray intersection only tests for the primitive's bounding box, not the actual geometry itself.


## Sample Output
```
Triangle 1 at (0, 1, 0), (3, 2, 0), (1, 4, 0)
Triangle 2 at (2, 5, 0), (4, 6, 0), (5, 7, 0)
Triangle 3 at (5, 7, 0), (6, 7, 0), (7, 6, 7)
Triangle 4 at (4, 6, 0), (5, 8, 0), (6, 9, 0)
Triangle 5 at (10, 15, 1), (11, 14, 0), (12, 13, 0)

Ray cast at Origin(11,15,1) Direction (0,0,1)


Depth: 0 :  <<(Interior [Axis: 1, Bounds: Min: (0, 1, 0), Max: (12, 15, 7)])>> 
Depth: 1 :  <<(Interior [Axis: 1, Bounds: Min: (0, 1, 0), Max: (5, 7, 0)])>>  <<(Interior [Axis: 1, Bounds: Min: (4, 6, 0), Max: (12, 15, 7)])>> 
Depth: 2 :  <<(Leaf [0 - 1] [Bounds: Min: (0, 1, 0), Max: (3, 4, 0)]])>>  <<(Leaf [1 - 2] [Bounds: Min: (2, 5, 0), Max: (5, 7, 0)]])>>  <<(Leaf [2 - 3] [Bounds: Min: (5, 6, 0), Max: (7, 7, 7)]])>>  <<(Interior [Axis: 1, Bounds: Min: (4, 6, 0), Max: (12, 15, 1)])>> 
Depth: 3 :  <<NULL>>  <<NULL>>  <<NULL>>  <<NULL>>  <<NULL>>  <<NULL>>  <<(Leaf [3 - 4] [Bounds: Min: (4, 6, 0), Max: (6, 9, 0)]])>>  <<(Leaf [4 - 5] [Bounds: Min: (10, 13, 0), Max: (12, 15, 1)]])>> 

Hits:
Ray Hit with Triangle 5

```

