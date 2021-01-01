namespace Tasks.LinkedList
{
    public class LinkedListNode<T>
    {
        public LinkedListNode<T> Next { get; set; }
        public T Value { get; set; }

        internal LinkedListNode(T value, LinkedListNode<T> next)
        {
            Value = value;
            Next = next;
        }

        public LinkedListNode() : this(default(T), null)
        {
            
        }
    }
}