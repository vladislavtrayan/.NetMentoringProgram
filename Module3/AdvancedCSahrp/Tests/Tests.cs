using System;
using System.Linq;
using NUnit.Framework;
using Services;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Directory = AppDomain.CurrentDomain.BaseDirectory;
        }

        public string Directory { get; set; }

        [Test]
        public void ItemIsExcludedFromResult()
        {
            var fileToExclude =  new FileSystemVisitor().Search(Directory).Last();
            var fileVisitor = new FileSystemVisitor(x => x.Equals(fileToExclude));
            fileVisitor.FilteredItemFound += RemoveItemFromResult;
            Assert.False(fileVisitor.Search(Directory).Contains(fileToExclude));
        }
        
        [Test]
        public void SearchIsStopped()
        {
            var files =  new FileSystemVisitor().Search(Directory);
            var expectedLastItem = files.ToArray()[files.Count() / 2];
            var fileVisitor = new FileSystemVisitor(x => x.Equals(expectedLastItem));
            fileVisitor.FilteredItemFound += StopSearching;
            var actualItems = fileVisitor.Search(Directory);
            Assert.Less( actualItems.Count(), files.Count());
        }

        public void StopSearching(object sender, ItemFoundArgs args)
        {
            args.EndSearch = true;
        }
        
        public void RemoveItemFromResult(object sender, ItemFoundArgs args)
        {
            args.RemoveItemFromResult = true;
        }
        
        public void AddItemToResult(object sender, ItemFoundArgs args)
        {
            args.RemoveItemFromResult = false;
        }
    }
}