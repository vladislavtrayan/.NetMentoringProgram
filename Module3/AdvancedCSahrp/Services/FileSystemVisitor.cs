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
        private bool searchStopped;

        public FileSystemVisitor()
        {
            filteringRules.Add( x => false);
        }
        
        public FileSystemVisitor(params Predicate<string>[] filteringRules)
        {
            this.filteringRules.AddRange(filteringRules);
        }

        public IEnumerable<string> Search(string path)
        {
            if(string.IsNullOrEmpty(path))
                throw new ArgumentException("path to directory is not valid");
            SearchStarted?.Invoke();
            try
            {
                return GetFiles(path);
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
                var itemFoundArgs = new ItemFoundArgs(file);
                if (Filter(file))
                {
                    FilteredItemFound?.Invoke(this, itemFoundArgs);
                }
                else
                {
                    ItemFound?.Invoke(this, itemFoundArgs);
                }

                if (itemFoundArgs.EndSearch)
                {
                    searchStopped = true;
                }
                if (itemFoundArgs.RemoveItemFromResult)
                {
                    continue;
                }
                yield return file;
            }
            
            foreach (var subdirectory in Directory.GetDirectories(path))
            {
                if(searchStopped)
                    yield break;
                
                var dirFoundArgs = new ItemFoundArgs(subdirectory);
                if (Filter(subdirectory))
                {
                    FilteredItemFound?.Invoke(this, dirFoundArgs);
                }
                else
                {
                    ItemFound?.Invoke(this, dirFoundArgs);
                }
                if (dirFoundArgs.EndSearch)
                {
                    searchStopped = true;
                }
                if (!dirFoundArgs.RemoveItemFromResult)
                {
                    yield return subdirectory;
                }

                
                foreach (var file in GetFiles(subdirectory))
                {
                    var itemFoundArgs = new ItemFoundArgs(file);
                    if (Filter(file))
                    {
                        FilteredItemFound?.Invoke(this, new ItemFoundArgs(file));
                    }
                    else
                    {
                        ItemFound?.Invoke(this,new ItemFoundArgs(file));
                    }
                    if (itemFoundArgs.EndSearch)
                    {
                        searchStopped = true;
                    }
                    if (itemFoundArgs.RemoveItemFromResult)
                    {
                        continue;
                    }
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