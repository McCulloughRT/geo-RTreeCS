using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTreeCS
{
    public class rtree
    {
        private int minWidth = 3; // Min width of any node before a merge
        private int maxWidth = 6; // Max width of any node before a split
        public Node Root { get; }

        public rtree()
        {
            // Start with an empty root-tree
            Root = new Node(new Rect(0, 0, 0, 0), NodeType.root, new List<Node>());
        }
        public rtree(int width)
        {
            minWidth = Convert.ToInt32( Math.Floor(width / 2.0) );
            maxWidth = width;
        }
        public IList<Node> insert(Rect bbox, Leaf leaf)
        {
            throw new NotImplementedException();
            insertSubtree(new Node(bbox, NodeType.leaf, leaf), Root);
        }

        private void insertSubtree(Node node, Node root)
        {
            Node bestCurrentNode = null;
            if(root.Nodes.Count == 0)
            {
                root.Rectangle.X = node.Rectangle.X;
                root.Rectangle.Y = node.Rectangle.Y;
                root.Rectangle.W = node.Rectangle.W;
                root.Rectangle.H = node.Rectangle.H;
                root.Nodes.Add(node);
                return;
            }

            IList<Node> treeStack = chooseLeafSubtree(node, root);
            Node retObj = node;
            Node pbc;

            while(treeStack.Count > 0)
            {
                if((bestCurrentNode != null) && (bestCurrentNode.Id != NodeType.leaf) && bestCurrentNode.Nodes.Count == 0)
                {
                    pbc = bestCurrentNode; // Past best current node
                    bestCurrentNode = treeStack.Pop();
                    for (int i = 0; i < bestCurrentNode.Nodes.Count; i++)
                    {
                        if(bestCurrentNode.Nodes[i] == pbc || bestCurrentNode.Nodes[i].Nodes.Count == 0)
                        {
                            //bestCurrentNode.Nodes.Splice(i, 1); //TODO: Verify this is appropriate index
                            bestCurrentNode.Nodes.RemoveAt(i);
                            break;
                        }
                    }
                }
                else
                {
                    bestCurrentNode = treeStack.Pop();
                }

                if(retObj.Id == NodeType.leaf || retObj.Id == NodeType.node)
                {

                }
            }
        }

        private IList<Node> chooseLeafSubtree(Node leafNode, Node rootNode)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Placeholder class for data object
    /// </summary>
    public class Leaf
    {
        public string Object { get; set; }
        public Leaf()
        {

        }
        public Leaf(string obj)
        {
            Object = obj;
        }
    }
    /// <summary>
    /// Bounding box rectangle
    /// </summary>
    public class Rect
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double W { get; set; }
        public double H { get; set; }
        public Rect()
        {
            // null init
        }
        /// <summary>
        /// Bounding box for features
        /// </summary>
        /// <param name="x">Top left X coordinate</param>
        /// <param name="y">Top left Y coordinate</param>
        /// <param name="w">BBox width</param>
        /// <param name="h">BBox height</param>
        public Rect(double x, double y, double w, double h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }
    }
    /// <summary>
    /// Data nodes in RTree structure
    /// </summary>
    public class Node
    {
        public Rect Rectangle { get; set; }
        public NodeType Id { get; set; }
        public IList<Node> Nodes { get; set; }
        public Leaf Leaf { get; set; }
        public Node()
        {
            // null init
        }
        /// <summary>
        /// Creates a new directory node
        /// </summary>
        /// <param name="rectangle">Min bbox of node features</param>
        /// <param name="id">Type of node</param>
        /// <param name="nodes">Children nodes</param>
        public Node(Rect rectangle, NodeType id, IList<Node> nodes)
        {
            Rectangle = rectangle;
            Id = id;
            Nodes = nodes;
        }
        /// <summary>
        /// Creates a new leaf node
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="leaf"></param>
        public Node(Rect rectangle, NodeType id, Leaf leaf)
        {
            Rectangle = rectangle;
            Id = id;
            Leaf = leaf;
        }
    }
    public enum NodeType
    {
        node,
        leaf,
        root
    }
    static class ListExtension
    {
        public static T Pop<T>(this IList<T> list)
        {
            T r = list[list.Count];
            list.RemoveAt(list.Count);
            return r;
        }
        public static IEnumerable<T> Splice<T>(this IEnumerable<T> list, int offset, int count)
        {
            IEnumerable<T> range = list.Skip(offset).Take(count);
            list = list.Take(offset).Concat(list.Skip(offset + count));
            return range;
        }
    }
}
