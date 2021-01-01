using System.Collections.Generic;

namespace Tasks.LinkedList
{
    public interface ILinkedList<T> : IEnumerable<T>
    {
        public int Length { get; set; }
        public void Add(T element);
        public void AddAt(T element, int index);
        public void Remove(T element);
        public void RemoveAt(int index);
        public T ElementAt(int index);
    }
}