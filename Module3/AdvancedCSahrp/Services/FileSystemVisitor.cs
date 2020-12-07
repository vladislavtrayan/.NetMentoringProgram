using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Services.Delegates;

namespace Services
{
    public class FileSystemVisitor
    {
        public event Action SearchStarted;
        public event Action SearchFinished;

        public event ItemFoundHandler ItemFound;
        public event ItemFoundHandler FilteredItemFound;

        private readonly List<Predicate<string>> filteringRules = new List<Predicate<string>>();

        public FileSystemVisitor()
        {
            filteringRules.Add( x => false);
        }
        
        public FileSystemVisitor(params Predicate<string>[] filteringRules)
        {
            this.filteringRules.AddRange(filteringRules);
        }

        public List<string> Search(string path)
        {
            if(string.IsNullOrEmpty(path))
                throw new ArgumentException("path to directory is not valid");
            SearchStarted?.Invoke();
            var items = new List<string>();
            try
            {
                foreach(var item in GetFiles(path))
                {
                    var itemFoundArgs = new ItemFoundArgs(item);
                    if (Filter(item))
                    {
                        FilteredItemFound?.Invoke(this, itemFoundArgs);
                    }
                    else
                    {
                        ItemFound?.Invoke(this, itemFoundArgs);
                    }
                    if (itemFoundArgs.RemoveItemFromResult)
                    {
                        continue;
                    }
                    items.Add(item);
                    if (itemFoundArgs.EndSearch)
                    {
                        break;
                    }
                }
                return items;
            }
            finally
            {
                SearchFinished?.Invoke();
            }
        }

        private IEnumerable<string> GetFiles(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                yield return file;
            }
            
            foreach (var subdirectory in Directory.GetDirectories(path))
            {
                foreach (var file in GetFiles(subdirectory))
                {
                    yield return file;
                }
            }
        }

        private bool Filter(string item)
        {
            foreach (var filter in filteringRules)
            {
                if (filter(item))
                    return true;
            }
            return false;
        }
    }
}