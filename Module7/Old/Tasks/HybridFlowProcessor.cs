using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> items = new DoublyLinkedList<T>();
        public void Push(T item)
        {
            items.AddAt(0, item);
        }

        public T Pop()
        {
            if (items.Length == 0) throw new InvalidOperationException();
            return items.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            items.Add(item);
        }

        public T Dequeue()
        {
            if (items.Length == 0) throw new InvalidOperationException();
            return items.RemoveAt(0);
        }
    }
}
