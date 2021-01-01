using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    internal class DoublyLinkedListNode<T> : IEquatable<T>
    {
        public DoublyLinkedListNode() : this(default(T), null, null)
        {
        }

        internal DoublyLinkedListNode(T value, DoublyLinkedListNode<T> previous, DoublyLinkedListNode<T> next)
        {
            Previous = previous;
            Next = next;
            Value = value;
        }

        internal DoublyLinkedListNode<T> Next;
        internal DoublyLinkedListNode<T> Previous;
        internal T Value;

        public bool Equals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }
    }

    internal class DoublyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private readonly DoublyLinkedListNode<T> _sentinel;
        private DoublyLinkedListNode<T> _currentNode;

        public DoublyLinkedListEnumerator(DoublyLinkedListNode<T> sentinel)
        {
            _sentinel = _currentNode = sentinel;
        }

        public bool MoveNext()
        {
            _currentNode = _currentNode.Next;
            return _currentNode != _sentinel;
        }

        public void Reset()
        {
            _currentNode = _sentinel;
        }

        public T Current => _currentNode.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private readonly DoublyLinkedListNode<T> _sentinel;
        public DoublyLinkedList()
        {
            _sentinel = new DoublyLinkedListNode<T>();
            _sentinel.Next = _sentinel;
            _sentinel.Previous = _sentinel;
            Length = 0;
        }

        public int Length { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(_sentinel);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T e)
        {
            AddAt(Length, e);
        }

        public void AddAt(int index, T e)
        {
            var currentItem = index == Length ? _sentinel : NodeAt(index);
            var node = new DoublyLinkedListNode<T>(e, currentItem.Previous, currentItem);
            currentItem.Previous.Next = node;
            currentItem.Previous = node;
            Length++;
        }

        private DoublyLinkedListNode<T> NodeAt(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            DoublyLinkedListNode<T> node = _sentinel;
            if (index < Length / 2)
            {
                for (int i = -1; i < index; i++)
                    node = node.Next;
            }
            else
            {
                for (int i = 0; i < Length - index; i++)
                    node = node.Previous;
            }

            return node;
        }

        public T ElementAt(int index)
        {
            return NodeAt(index).Value;
        }

        public void Remove(T item)
        {
            var node = FindFirstNode(item);
            if (node == null) return;

            RemoveNode(node);
        }

        private DoublyLinkedListNode<T> FindFirstNode(T item)
        {
            var node = _sentinel;
            for (int i = 0; i < Length; i++)
            {
                node = node.Next;
                if (node.Equals(item)) return node;
            }

            return null;
        }

        public T RemoveAt(int index)
        {
            var node = NodeAt(index);
            RemoveNode(node);
            return node.Value;
        }

        private void RemoveNode(DoublyLinkedListNode<T> node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            Length--;
        }
    }
}
