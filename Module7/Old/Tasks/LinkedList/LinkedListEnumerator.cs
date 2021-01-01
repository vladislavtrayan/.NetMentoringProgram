using System.Collections;
using System.Collections.Generic;

namespace Tasks.LinkedList
{
    public class LinkedListEnumerator<T> : IEnumerator<T>
    {
        private readonly LinkedListNode<T> _sentinel;
        private LinkedListNode<T> _currentNode;
        
        public LinkedListEnumerator(LinkedListNode<T> node)
        {
            _sentinel = node;
            _currentNode = node;
        }
        public bool MoveNext()
        {
            _currentNode = _currentNode.Next;
            return _currentNode != null;
        }

        public void Reset()
        {
            _currentNode = _sentinel;
        }

        public T Current => _currentNode.Value;

        object? IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}