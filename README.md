# geo-RTreeCS
An implementation of the R-Tree data structure in C#

The R-Tree is a data structure for querying multidimensional geographic data. It groups nearby nodes together and represents them with their minimum bounding rectangle in the next higher level of the tree, allowing efficient nearest neighbor and bounding box searches.

Main method is rtree.cs

This project is in progress, and currently INCOMPLETE.
It will include methods for creating an RTree from CSV or GeoJSON data, inserting new features into that tree, removing features from the tree, and running bounding box queries.

It will additionally include all nessecary data classes to handle geographic data as native .NET objects.
