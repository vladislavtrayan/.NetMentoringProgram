using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SetStructure.CustomExceptions;

namespace SetStructure
{
    public class Set<T> : ISet<T> where T : IComparable
    {
        private readonly List<T> _items = new List<T>();

        public int Count { get; set; }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (Contains(item)) return;
            
            _items.Add(item);
            Count++;
        }

        public void Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            
            if (!Contains(item)) throw new ElementNotFoundException("item not found" + item);

            _items.Remove(item);
            Count--;
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public bool IsSubsetOf(ISet<T> set)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            
            return _items.All(set.Contains);
        }

        public ISet<T> UnionWith(ISet<T> set)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            var result = new Set<T>();
            foreach (var item in this)
            {
                result.Add(item);
            }

            foreach (var item in set)
            {
                result.Add(item);
            }

            return result;
        }

        public ISet<T> IntersectWith(ISet<T> set)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            var result = new Set<T>();
            if (Count >= set.Count)
            {
                foreach (var item in this)
                {
                    if(set.Contains(item))
                        result.Add(item);
                }
            }
            else
            {
                foreach (var item in set)
                {
                    if(Contains(item))
                        result.Add(item);   
                }
            }

            return result;
        }

        public ISet<T> ExceptWith(ISet<T> set)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            var result = new Set<T>();
            foreach (var item in this)
            {
                if(!set.Contains(item))
                    result.Add(item);
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}