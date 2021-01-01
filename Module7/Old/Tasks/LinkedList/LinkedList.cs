using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks.LinkedList
{
    public class LinkedList<T> : ILinkedList<T>
    {
        private LinkedListNode<T> _sentinel;
        public int Length { get; set; } = 0;
        
        public LinkedList()
        {
            _sentinel = new LinkedListNode<T>();
            _sentinel.Next = _sentinel;
        }

        public void Add(T element)
        {
            AddAt(element,Length);
        }

        public void AddAt(T element, int index)
        {
            var currentItem = index == Length ? _sentinel : NodeAt(index - 1);
            var node = new LinkedListNode<T>(element, currentItem.Next);
            currentItem.Next = node;
            Length++;
        }

        public void Remove(T element)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            Length--;
            throw new System.NotImplementedException();
        }

        public T ElementAt(int index)
        {
            return NodeAt(index).Value;
        }

        private LinkedListNode<T> NodeAt(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var node = _sentinel;
            for (var i = 0; i <= index; i++)
            {
                node = node.Next;
            }
            return node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_sentinel);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}