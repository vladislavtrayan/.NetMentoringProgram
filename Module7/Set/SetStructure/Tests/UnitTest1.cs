using System;
using System.Linq;
using SetStructure;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ElementAddedWithIncrement()
        {
            var set = new Set<int>();
            set.Add(1);
            Assert.Equal(1 ,set.Count);
        }
        
        [Fact]
        public void SameElementsNotAdded()
        {
            var set = new Set<int>();
            set.Add(1);
            set.Add(1);
            Assert.Equal(1 ,set.Count);
        }
        
        [Fact]
        public void ElementIsRemovedWithDicrement()
        {
            var set = new Set<int>();
            set.Add(1);
            set.Add(2);
            Assert.Equal(2 ,set.Count);
            set.Remove(1);
            Assert.Equal(1 ,set.Count);
        }
        
        [Fact]
        public void SetIsSubsetOf()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            Assert.True(set1.IsSubsetOf(set2));
        }
        
        [Fact]
        public void SetUnion()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            set1.Add(4);
            
            var expectedSet = new Set<int>();
            expectedSet.Add(1);
            expectedSet.Add(2);
            expectedSet.Add(3);
            expectedSet.Add(4);
            Assert.True(set1.UnionWith(set2).All(element => expectedSet.Contains(element)));
        }
        
        [Fact]
        public void SetExcetWith()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            set1.Add(4);
            
            var expectedSet = new Set<int>();
            expectedSet.Add(4);
            Assert.True(set1.ExceptWith(set2).All(element => expectedSet.Contains(element)));
            Assert.Equal(1,set1.ExceptWith(set2).Count);
        }
        
        [Fact]
        public void SetIntersectWith()
        {
            var set1 = new Set<int>();
            var set2 = new Set<int>();
            set2.Add(1);
            set2.Add(2);
            set2.Add(3);
            
            set1.Add(2);
            set1.Add(1);
            set1.Add(4);
            
            var expectedSet = new Set<int>();
            expectedSet.Add(1);
            expectedSet.Add(2);
            Assert.True(set1.IntersectWith(set2).All(element => expectedSet.Contains(element)));
            Assert.Equal(2,set1.IntersectWith(set2).Count);
        }
        
        [Fact]
        public void ThrowsExceptionIfSet2IsNull()
        {
            var set1 = new Set<int>();
            Set<int> set2 = null;
            set1.Add(2);
            
            Assert.Throws<ArgumentNullException>(() => set1.IntersectWith(set2));
        }
    }
}