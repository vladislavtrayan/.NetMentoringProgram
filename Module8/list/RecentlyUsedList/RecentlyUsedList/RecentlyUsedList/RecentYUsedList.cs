using System;
using System.Collections;
using System.Collections.Generic;

namespace RecentlyUsedList
{
    public class RecentYUsedList
    {
        public RecentYUsedList(int capacity = 5)
        {
            if(capacity <= 0)
                throw new ArgumentOutOfRangeException("Capacity should be greater than zero");
            Capacity = capacity;
        }
        
        private readonly List<string> _storage = new List<string>();
        public int Count => _storage.Count;
        public int Capacity { get;  }

        public void Add(string element)
        {
            if(element == null)
                throw new ArgumentNullException();
            if (_storage.Contains(element))
            {
                _storage.Remove(element);
            }
            _storage.Add(element);
        }
        
        public string this[int index]
        {
            get
            {
                if(index < 0)
                    throw new IndexOutOfRangeException();
                return _storage[Count - 1 - index];
            }
            set
            {
                if(index < 0)
                    throw new IndexOutOfRangeException();

                _storage[Count - 1 - index] = value;
            }
        }
    }
}