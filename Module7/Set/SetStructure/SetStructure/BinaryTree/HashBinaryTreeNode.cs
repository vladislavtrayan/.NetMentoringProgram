using System.Collections.Generic;

namespace SetStructure
{
    public enum Side
    {
        Left,
        Right
    }
    
    public class HashBinaryTreeNode<T>
    {
        public int ItemsHashCode { get; }
        public List<T> Data { get; } = new List<T>();
        
        public HashBinaryTreeNode(int itemsHashCode,T data)
        {
            ItemsHashCode = itemsHashCode;
            Data.Add(data);
        }
        
        public HashBinaryTreeNode<T> LeftNode { get; set; }
        public HashBinaryTreeNode<T> RightNode { get; set; }
        public HashBinaryTreeNode<T> ParentNode { get; set; }
        public Side? NodeSide =>
            ParentNode == null
                ? (Side?)null
                : ParentNode.LeftNode == this
                    ? Side.Left
                    : Side.Right;
    }
}