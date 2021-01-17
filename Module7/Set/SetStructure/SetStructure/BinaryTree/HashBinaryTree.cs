using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SetStructure.CustomExceptions;

namespace SetStructure
{
    public class HashBinaryTree<T> : IEnumerable<T>
    {
        private HashBinaryTreeNode<T> RootNode { get; set; }

        
        public void Add(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            Add(new HashBinaryTreeNode<T>(data.GetHashCode(), data));
        }

        public void Remove(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            var node = FindNode(data);
            if (node == null)
            {
                throw new ElementNotFoundException("item not found" + data);
            }
            node.Data.Remove(data);
            
            if(!node.Data.Any())
                Remove(node);
        }

        public bool Contains(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            return FindNode(data) != null;
        }

        private HashBinaryTreeNode<T> Add(HashBinaryTreeNode<T> node, HashBinaryTreeNode<T> currentNode = null)
        {
            if (RootNode == null)
            {
                node.ParentNode = null;
                return RootNode = node;
            }

            currentNode ??= RootNode;
            node.ParentNode = currentNode;
            int result = node.ItemsHashCode.CompareTo(currentNode.ItemsHashCode);
            if (result == 0)
            {
                currentNode.Data.Add(node.Data.First());
                return currentNode;
            }

            if (result < 0)
            {
                if (currentNode.LeftNode != null) return Add(node, currentNode.LeftNode);
                currentNode.LeftNode = node;
                return currentNode;

            }

            if (currentNode.RightNode != null) return Add(node, currentNode.RightNode);
            currentNode.RightNode = node;
            return currentNode;
        }
        
        private HashBinaryTreeNode<T> FindNode(T data, HashBinaryTreeNode<T> startWithNode = null)
        {
            if (RootNode == null)
                return null;
            startWithNode ??= RootNode;
            int result = data.GetHashCode().CompareTo(startWithNode.ItemsHashCode);
            return (result == 0 & startWithNode.Data.Contains(data) ? startWithNode :
                result < 0 ? startWithNode.LeftNode == null ? null : FindNode(data, startWithNode.LeftNode) :
                startWithNode.RightNode == null ? null : FindNode(data, startWithNode.RightNode));
        }
        
        private void Remove(HashBinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node == RootNode)
            {
                var leftBranch = node.LeftNode;
                RootNode = node.RightNode;
                if(leftBranch != null)
                    Add(leftBranch);
            }
            else
            {
                var leftBranch = node.LeftNode;
                var rightBranch = node.RightNode;
                switch (node.NodeSide)
                {
                    case Side.Left:
                        node.ParentNode.LeftNode = null;
                        break;
                    case Side.Right:
                        node.ParentNode.RightNode = null;
                        break;
                }
                if(leftBranch != null)
                    Add(leftBranch);
                if(rightBranch != null)
                    Add(rightBranch);
            }
        }
        
        private IEnumerable<T> GetTreeElements(HashBinaryTreeNode<T> root)
        {
            if (root == null)
                yield break;
            foreach (var item in root.Data)
            {
                yield return item;
            }

            foreach (var value in GetTreeElements(root.LeftNode)) yield return value;
            foreach (var value in GetTreeElements(root.RightNode)) yield return value;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in GetTreeElements(RootNode))
                yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}