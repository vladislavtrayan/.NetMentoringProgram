using System.Collections.Generic;

namespace SetStructure
{
    public interface ISet<T> : IEnumerable<T>
    {
        public int Count { get; set; }
        public void Add(T item);
        public void Remove(T item);
        public bool Contains(T item);
        public bool IsSubsetOf(ISet<T> set);
        public ISet<T> UnionWith(ISet<T> set);
        public ISet<T> IntersectWith(ISet<T> set);
        public ISet<T> ExceptWith(ISet<T> set);
    }
}